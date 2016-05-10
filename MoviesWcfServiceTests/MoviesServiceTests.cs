using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviesWcfService;

namespace MoviesWcfServiceTests
{
    [TestClass]
    public class MoviesServiceTests
    {
        // Please forgive the lack of unit tests.  These smoke tests should be sufficient to ensure code coverage for this small project.

        [TestMethod]
        public void FindMovies_ShouldFind18Results()
        {
            var svc = new MoviesService();
            var result = svc.FindMovies("the", MovieSortFields.Rating, false);
            Assert.IsTrue(result.Count >= 18, "Expected 3 results.");
        }

        [TestMethod]
        public void FindMovies_ShouldSortDescendingOnRating()
        {
            var svc = new MoviesService();
            var result = svc.FindMovies("the", MovieSortFields.Rating, false);
            Assert.IsTrue(result[0].Rating > result[result.Count - 1].Rating, "Expected Descending Sort on Rating");
        }

        [TestMethod]
        public void FindMovies_ShouldSortAscendingOnTitle()
        {
            var svc = new MoviesService();
            var result = svc.FindMovies("the", MovieSortFields.Title, false);
            Assert.IsTrue(string.CompareOrdinal(result[0].Title, result[result.Count - 1].Title) > 0, "Expected Ascending Sort on Title");
        }

        [TestMethod]
        public void AddMovie_ShouldShowUpInFindMovies()
        {
            var svc = new MoviesService();
            svc.AddMovie(new[] {"alfred jones", "julia roberts"}, "M", "Drama", 3, 2013, "The King and I");
            Assert.IsTrue(svc.FindMovies("The King and I").Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateMovie_ShouldFailIfMovieIdIsZero()
        {
            var svc = new MoviesService();
            svc.UpdateMovie(0, new[] {"alfred jones", "julia roberts"}, "M", "Drama", 3, 2013, "The King and I");
        }

        [TestMethod]
        [ExpectedException(typeof (KeyNotFoundException))]
        public void UpdateMovie_ShouldFailIfMovieIdDoesntExist()
        {
            var svc = new MoviesService();
            svc.UpdateMovie(1001, new[] {"alfred jones", "julia roberts"}, "M", "Drama", 3, 2013, "The King and I");
        }

        [TestMethod]
        public void UpdateMovie_ShouldChangeMovieRating()
        {
            var svc = new MoviesService();
            Assert.IsTrue(svc.FindMovies("The Simpsons")[0].Rating >= 3,
                "The Simpsons should already be in the database with a Rating of 3.");
            svc.UpdateMovie(1, new string[] {}, "M", "Drama", 1, 2013, "The Simpsons");
            Assert.IsTrue(svc.FindMovies("The Simpsons")[0].Rating == 1,
                "The Simpsons should have been given a new rating of 1");
        }
    }
}