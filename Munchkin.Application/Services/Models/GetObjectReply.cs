using Minio.DataModel;

namespace Munchkin.Application.Services.Models
{
    public class GetObjectReply
    {
        public byte[]? Data { get; set; }
        public ObjectStat? ObjectStat { get; set; }
    }
}
