using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOfAlifTech.Service.Exceptions
{
    public class AppException : Exception
    {
        public int Code { get; set; }
        public AppException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
