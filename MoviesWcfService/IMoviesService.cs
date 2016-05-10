using System;
using System.Collections.Generic;
using System.ServiceModel;
using MoviesLibrary;

namespace MoviesWcfService
{
    [ServiceContract]
    public interface IMoviesService
    {
        /// <summary>
        ///     Searches all text fields in all movies for a search term, and returns the matching movies as a sorted list.
        /// </summary>
        /// <param name="searchTerm">The key word or phrase you are looking for.</param>
        /// <param name="sortField">Which field you would like to sort by</param>
        /// <param name="sortAscending">An optional boolean, defaults to true.  Specify false if you want a descending sort.</param>
        /// <returns>A list of movies containing your search term, sorted by the field you specified.</returns>
        [OperationContract]
        [FaultContract(typeof (MoviesServiceFault))]
        List<MovieData> FindMovies(String searchTerm, MovieSortFields sortField = MovieSortFields.Title, bool sortAscending = true);

        /// <summary>
        ///     Adds a Movie into the data store
        /// </summary>
        /// <param name="cast">A string array containing the names of cast members</param>
        /// <param name="classification">The classification of the movie, such as PG, G, MA</param>
        /// <param name="genre">Romance, Thriller, Comedy, etc</param>
        /// <param name="rating">The rating of the movie, as a number between 0 and 5</param>
        /// <param name="releaseYear">The year the movie was released</param>
        /// <param name="title">The title of the movie</param>
        /// <returns>The MovieId of the newly added movie</returns>
        [OperationContract]
        [FaultContract(typeof (MoviesServiceFault))]
        int AddMovie(string[] cast, string classification, string genre, int rating, int releaseYear, string title);

        /// <summary>
        ///     Updates a Movie in the data store
        /// </summary>
        /// <param name="movieId">
        ///     The ID number of the movie to be updated. This is the only mandatory parameter.  All others are
        ///     optional.
        /// </param>
        /// <param name="cast">A string array containing the names of cast members</param>
        /// <param name="classification">The classification of the movie, such as PG, G, MA</param>
        /// <param name="genre">Romance, Thriller, Comedy, etc</param>
        /// <param name="rating">The rating of the movie, as a number between 0 and 5</param>
        /// <param name="releaseYear">The year the movie was released</param>
        /// <param name="title">The title of the movie</param>
        /// <returns>The MovieId of the newly added movie</returns>
        [OperationContract]
        [FaultContract(typeof (MoviesServiceFault))]
        bool UpdateMovie(int movieId, string[] cast = null, string classification = "", string genre = "", int rating = 0,
            int releaseYear = 0, string title = "");
    }
}