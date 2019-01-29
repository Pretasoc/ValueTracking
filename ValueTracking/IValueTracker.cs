using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace ValueTracking
{ 
    public interface IValueTracker
    {
        IValueStore<T> Create<T>(T actualValue, T storedValue, IEqualityComparer<T> equalityComparer, Action<T, T>? updateCallback = null);

        void UpdateChanged();

        bool AnyChanged { get;}
    }
}
