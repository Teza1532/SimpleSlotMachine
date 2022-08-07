using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Common.Exceptions
{
    public class NoSymbolFoundException : Exception
    {
        public NoSymbolFoundException()
        {
        }

        public NoSymbolFoundException(string? message) : base(message)
        {
        }

        protected NoSymbolFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
