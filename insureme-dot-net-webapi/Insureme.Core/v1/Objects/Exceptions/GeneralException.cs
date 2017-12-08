using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insureme.Core.v1.Objects.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string message) : base(message)
        {
        }

        public GeneralException(int code, string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}