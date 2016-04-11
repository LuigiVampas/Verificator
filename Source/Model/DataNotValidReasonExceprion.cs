using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    public class DataNotValidReason : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DataNotValidReason()
        {
        }

        public DataNotValidReason(string message) : base(message)
        {
        }

        public DataNotValidReason(string message, Exception inner) : base(message, inner)
        {
        }

        protected DataNotValidReason(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}