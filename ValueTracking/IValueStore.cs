using System;
using System.Collections.Generic;
using System.Text;

namespace ValueTracking
{
    public interface IValueStore<T>
    { 
        T Value { get; set; }

        bool HasChanged { get;}
    }
}
