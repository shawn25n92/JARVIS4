using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS4
{
    public class StatusObject
    {
        public enum StatusCode
        {
            SUCCESS,
            FAILURE
        }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public StatusCode Status { get; set; }
        public string ErrorStacktrace { get; set; }
        public StatusObject()
        {
            this.Status = StatusCode.SUCCESS;
        }
        public StatusObject(string ErrorMessage, string ErrorCode, StatusCode Status, string ErrorStacktrace)
        {
            this.ErrorMessage = ErrorMessage;
            this.ErrorCode = ErrorCode;
            this.Status = Status;
            this.ErrorStacktrace = ErrorStacktrace;
        }
    }
}
