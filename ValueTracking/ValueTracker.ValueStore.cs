#nullable enable

using System.Collections.Generic;

namespace ValueTracking
{
    public partial class ValueTracker
    {
        private class ValueStore<T> : IValueStore, IValueStore<T>
        {
            private readonly IEqualityComparer<T> _equalityComparer;
            private T _storedValue;
            private T _currentValue;
            private readonly ValueTracker _valueTracker;

            public ValueStore(T currentValue, T storedValue, ValueTracker valueTracker, IEqualityComparer<T> equalityComparer)
            {
                _currentValue = currentValue;
                _storedValue = storedValue;
                _valueTracker = valueTracker;
                _equalityComparer = equalityComparer;
                CheckHasChanged();
            }

            private void CheckHasChanged()
            {
                if (HasChanged = _equalityComparer.Equals(_storedValue, _currentValue))
                {
                    _valueTracker.Add(this);
                }
                else
                {
                    _valueTracker.Remove(this);
                }
            }

            public T Value
            {
                get => _currentValue;
                set
                {
                    if (!_equalityComparer.Equals(_currentValue, value))
                    {
                        _currentValue = value;
                        CheckHasChanged();
                    }
                }
            }

            public bool HasChanged { get; private set; }

            object? IValueStore.Value => _currentValue;

            object? IValueStore.StoredValue => _storedValue;

            void IValueStore.Reset()
            {
                _storedValue = _currentValue;
                CheckHasChanged();
            }
        }


    }
}


