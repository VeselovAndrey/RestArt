// -----------------------------------------------------------------------
// <copyright file="ObjectConvertor.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace RestArt.Core.Convertors
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class ObjectConvertor
    {
        public IDictionary<string, object> ToObjectDictionary(object source)
        {
            Type sourceType = source.GetType();

            IEnumerable<PropertyInfo> properties = sourceType.GetRuntimeProperties();
            Dictionary<string, object> dictionary = new Dictionary<string, object>(capacity: properties.Count());

            foreach (PropertyInfo property in properties) {
                object value = property.GetValue(source);
                dictionary.Add(property.Name, value);
            }

            return dictionary;
        }

        public IDictionary<string, string> ToStringDictionary(object source)
        {
            Type sourceType = source.GetType();

            IEnumerable<PropertyInfo> properties = sourceType.GetRuntimeProperties();
            Dictionary<string, string> dictionary = new Dictionary<string, string>(capacity: properties.Count());

            foreach (PropertyInfo property in properties) {
                object value = property.GetValue(source);
                dictionary.Add(property.Name, value.ToString());
            }

            return dictionary;
        }
    }
}
