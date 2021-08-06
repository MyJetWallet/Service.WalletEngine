using System.Runtime.Serialization;
using Service.WalletEngine.Domain.Models;

namespace Service.WalletEngine.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}