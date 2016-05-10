using MoviesLibrary;

namespace MoviesWcfService
{
    public static class MovieDataHelper
    {
        public static bool IsEqualTo(this MovieData source, MovieData toCheck)
        {
            if (toCheck == null)
                return false;

            if (toCheck.Cast.ToString() == source.Cast.ToString() && toCheck.Classification == source.Classification && toCheck.Genre == source.Genre
                && toCheck.MovieId == source.MovieId && toCheck.Rating == source.Rating && toCheck.ReleaseDate == source.ReleaseDate && toCheck.Title == source.Title)
                return true;
            return false;
        }
    }
}