using System;
using CQRSlite.Domain.Exceptions;

namespace CQRSlite.Domain.Factories
{
    internal static class AggregateFactory
    {
        public static T CreateAggregate<T>()
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            catch (MissingMemberException ex)
            {
                throw new MissingParameterLessConstructorException(typeof(T), ex);
            }
        }
    }
}