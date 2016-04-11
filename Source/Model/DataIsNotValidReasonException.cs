using System;
using System.Runtime.Serialization;

namespace Model
{
    [Serializable]
    public class DataIsNotValidReason : Exception
    {
        public DataIsNotValidReason(string message) : base(message)
        {
        }

    }
}