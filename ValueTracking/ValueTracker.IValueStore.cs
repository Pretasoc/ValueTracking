#nullable enable

namespace ValueTracking
{
    public partial class ValueTracker
    {
        private interface IValueStore
        {
            object Value { get; }

            object StoredValue { get;}

            void Reset();
        }


    }
}


