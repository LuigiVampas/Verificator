using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    public class DataIsNotValidReason : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DataIsNotValidReason()
        {
        }

        public DataIsNotValidReason(string message) : base(message)
        {
        }

        public DataIsNotValidReason(string message, Exception inner) : base(message, inner)
        {
        }

        protected DataIsNotValidReason(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}