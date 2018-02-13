using System;

namespace Aero.Infrastructure
{
    /// <summary>
    /// A lightweight wrapper around an IoC container. 
    /// </summary>
    /// <remarks>
    /// Should be used inside of factory classes only. IoC should not be used ad-hoc in non-factory classes. 
    /// </remarks>
    public interface IAccessor
    {
        T Get<T>();
        object Get(Type type);
    }

    public class Accessor : IAccessor
    {
        private readonly Func<Type, object> _getByType;

        public Accessor(Func<Type, object> getByType)
        {
            _getByType = getByType;
        }

        public object Get(Type type)
        {
            return _getByType(type);
        }

        public T Get<T>()
        {
            return (T)_getByType(typeof(T));
        }
    }
}
