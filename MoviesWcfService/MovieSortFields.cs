using System.Runtime.Serialization;

namespace MoviesWcfService
{
    /// <summary>
    ///     A list of fields that can be used for sorting output in FindMovies
    /// </summary>
    [DataContract]
    public enum MovieSortFields
    {
        [EnumMember] Classification = 0,
        [EnumMember] Genre = 1,
        [EnumMember] Rating = 2,
        [EnumMember] ReleaseDate = 3,
        [EnumMember] Title = 4
    }
}