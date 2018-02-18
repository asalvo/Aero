using System;
using System.Collections.Generic;

namespace Aero
{
    public sealed class DisposableManager : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                try
                {
                    disposable?.Dispose();
                }
                catch (Exception)
                {
                    //Silently continue
                }
            }

            _disposables.Clear();
        }

        public T Register<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }

        public void Register(params IDisposable[] items)
        {
            _disposables.AddRange(items);
        }
    }
}
