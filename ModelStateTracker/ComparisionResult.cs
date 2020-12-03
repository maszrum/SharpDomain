﻿using System.Collections.Generic;
using System.Linq;

namespace ModelStateTracker
{
    public class ComparisionResult<T>
    {
        public ComparisionResult(T model, IEnumerable<string> propertiesChanged)
        {
            Model = model;
            PropertiesChanged = propertiesChanged as IReadOnlyList<string> ?? propertiesChanged.ToList();
        }
        
        public T Model { get; }
        public IReadOnlyList<string> PropertiesChanged { get; }
    }
}