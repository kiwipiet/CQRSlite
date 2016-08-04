using System;
using System.Runtime.Serialization;

namespace CQRSlite.Domain.Exceptions
{
    [DataContract]
    public class MissingParameterLessConstructorException : Exception
    {
        public MissingParameterLessConstructorException()
        {
        }

        public MissingParameterLessConstructorException(string message) : base(message)
        {
        }

        public MissingParameterLessConstructorException(string message, Exception inner) : base(message, inner)
        {
        }

        public MissingParameterLessConstructorException(Type type, Exception inner)
            : base(
                $"{type.FullName} has no constructor without paramerters. This can be either public or private", inner)
        {
        }
    }
}