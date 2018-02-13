using System;

namespace Aero
{ 
    /// <summary>
    /// Generic even args. 
    /// </summary>
    /// <remarks>This was created for a much earlier version of .Net and may no longer be needed in .NetStandard</remarks>
    public interface IValueEventArgs<out T>
    {
        T Value { get; }
    }

    public class ValueEventArgs<T> : EventArgs, IValueEventArgs<T>
    {

        public ValueEventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

    }
}
