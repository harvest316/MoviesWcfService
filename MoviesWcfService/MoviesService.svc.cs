using System;
using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;

namespace MoviesWcfService
{
    [ErrorHandler]
    public class MoviesService : IMoviesService
    {
        private readonly List<MovieData> _cache;

        public MoviesService()
        {
            var dataSource = new MovieDataSource();
            _cache = dataSource.GetAllData();
        }

        #region IMoviesService Members

        public List<MovieData> FindMovies(String searchTerm, MovieSortFields sortField = MovieSortFields.Title, bool sortAscending = true)
        {
            var results = _cache.FindAll(e => string.Join(",", e.Title, e.Classification, e.Genre, e.Rating).ContainsAnyCase(searchTerm));
            return results.OrderBy(sortField + (sortAscending ? " asc" : " desc")).ToList();
        }

        public int AddMovie(string[] cast, string classification, string genre, int rating, int releaseYear, string title)
        {
            // Build local movie object
            var movie = new MovieData
            {
                Cast = cast,
                Classification = classification,
                Genre = genre,
                Rating = rating,
                ReleaseDate = releaseYear,
                Title = title
            };

            // Send to data store
            var dataSource = new MovieDataSource();
            var movieId = dataSource.Create(movie);

            // Update cache
            movie.MovieId = movieId;
            _cache.Add(movie);

            return movieId;
        }

        public bool UpdateMovie(int movieId, string[] cast = null, string classification = "", string genre = "", int rating = 0,
            int releaseYear = 0, string title = "")
        {
            if (movieId == 0)
                throw new ArgumentNullException("movieId", "MovieId is required.");

            // Get existing movie from cache
            var i = _cache.FindIndex(x => x.MovieId == movieId);
            if (i == -1)
                throw new KeyNotFoundException("MovieId not found.");
            var sourceMovie = _cache[i];

            // Update local movie object
            if (cast != null && cast.Length > 0)
                sourceMovie.Cast = cast;
            if (!String.IsNullOrEmpty(classification))
                sourceMovie.Classification = classification;
            if (!String.IsNullOrEmpty(genre))
                sourceMovie.Genre = genre;
            if (rating != 0)
                sourceMovie.Rating = rating;
            if (releaseYear != 0)
                sourceMovie.ReleaseDate = releaseYear;
            if (!String.IsNullOrEmpty(title))
                sourceMovie.Title = title;

            // Send to data store
            var dataSource = new MovieDataSource();
            dataSource.Update(sourceMovie);

            // Since data store doesn't return a boolean success/fail,
            // retrieve the updated movie to verify by inspection
            var updatedMovie = dataSource.GetDataById(movieId);

            // Update cache
            _cache[i] = updatedMovie;

            // Indicate if update was successful
            return updatedMovie.IsEqualTo(sourceMovie);
        }

        #endregion
    }
}