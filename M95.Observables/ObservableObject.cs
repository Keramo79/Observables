using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace M95.Observables
{
    /// <summary>
    /// Base class for objects used in ObservableList. Inherit this class for the objects stored in ObservableList to get notifications about property changes.
    /// To ensure proper notification, use Get(value) and Set(value) for property setting and getting values.
    /// </summary>
    /// <example>
    /// Example:
    /// <code>
    /// public class Info : ObservableObject
    /// {
    ///    public string Name { get { return Get(); } set { Set(value); } }
    /// }
    /// </code>
    /// </example>
    public class ObservableObject : INotifyPropertyChanged
    {
        public LoadingSource LoadingSource { get; set; }

        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        /// <summary>
        /// Get value of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Property name</param>
        /// <returns>string</returns>
        protected T Get<T>([CallerMemberName] string name = null)
        {
            object value = null;
            if (_properties.TryGetValue(name, out value))
                return value == null ? default(T) : (T)value;
            return default(T);
        }
        /// <summary>
        /// Set value of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Property value</param>
        /// <param name="name">String</param>
        protected void Set<T>(T value, [CallerMemberName] string name = null)
        {
            if (Equals(value, Get<T>(name)))
                return;
            _properties[name] = value;
            OnPropertyChanged(new PropertyChangedEventArgs(name));
        }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) { PropertyChanged?.Invoke(this, e); }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableObject()
        {
            foreach (PropertyInfo pi in GetType().GetProperties())
            {



            }
        }

    }
    public enum LoadingSource
    {
        NewObject = 0,
        Database = 1,
        Other = 2
    }
}
