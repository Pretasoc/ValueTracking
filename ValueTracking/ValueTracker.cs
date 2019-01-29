using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
#nullable enable

namespace ValueTracking
{
    public partial class ValueTracker : IValueTracker
    {
        private IDictionary<IValueStore, Action<object, object>?> _updateCallbacks = new Dictionary<IValueStore, Action<object, object>?>();
        private readonly ISet<IValueStore> _changedValues = new HashSet<IValueStore>();

        public bool AnyChanged => _changedValues.Count > 0;

        public IValueStore<T> Create<T>(T actualValue, T storedValue, IEqualityComparer<T> equalityComparer, Action<T, T>? updateCallback = null)
        {
            var store = new ValueStore<T>(actualValue, storedValue, this, equalityComparer);
#pragma warning disable CS8602 // Possible dereference of a null reference. This is checked, but not recognized by the compiler
            _updateCallbacks.Add(store, updateCallback != null ? ((object a, object b) => updateCallback((T)a, (T)b)) : (Action<object, object>?)null);
#pragma warning restore CS8602 // Possible dereference of a null reference.
            return store;
        }

        public void UpdateChanged()
        {

            var changed = _changedValues.ToArray();
            foreach (var changedValue in changed)
            {
                _updateCallbacks[changedValue]?.Invoke(changedValue.Value, changedValue.StoredValue);
            }
        }

        private void Add(IValueStore valueStore)
        {
            _changedValues.Add(valueStore);
        }


        private void Remove(IValueStore valueStore)
        {
            _changedValues.Remove(valueStore);
        }


    }
}


