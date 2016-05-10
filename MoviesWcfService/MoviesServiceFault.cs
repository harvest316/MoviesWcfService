using System.Runtime.Serialization;

namespace MoviesWcfService
{
    /// <summary>
    /// A fault contract for returning errors
    /// </summary>
    [DataContract]
    public class MoviesServiceFault
    {
        public static MoviesServiceFault Default = new MoviesServiceFault();

        [DataMember]
        public string FaultInfo { get; private set; }

        public MoviesServiceFault()
        {
            FaultInfo = "An unknown error occurred.";
        }

        public MoviesServiceFault(string faultInfo)
        {
            FaultInfo = faultInfo;
        }
    }
}