using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerilogMsSqlSinkTrain
{
    public class MyLog
    {
        public DateTime TimeStamp { get; }
        public string Message { get; set; }

        public string Model { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string HostName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public long? ElapsedMilliseconds { get; set; }
        public string CorrelationId { get; set; }
        public Exception Exception { get; set; }
        public Dictionary<string, string> AdditionalInformation { get; set; }

        public MyLog() => TimeStamp = DateTime.UtcNow;
    }
}
