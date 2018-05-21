using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;


namespace M95.Observables
{
    public class ObservableList<T> : List<T>
    {
        public delegate void ClearHandler();
        public delegate void ChangeHandler(ObservableList<T> ol, ObservableListEventArgs<T> e);
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
        public event ChangeHandler ItemChanging;
        public event ChangeHandler ItemChanged;
        public event ChangeHandler ItemAdding;
        public event ChangeHandler ItemAdded;
        public event ChangeHandler ItemRemoving;
        public event ChangeHandler ItemRemoved;
        public event ClearHandler ItemsCleared;
        public event ClearHandler ItemsLoaded;
        public bool Changing { get; set; }
        public bool Adding { get; set; }
        public bool Removing { get; set; }
        public bool Loading { get; set; }
        public bool Loaded { get; set; }
        public void OnItemChanged(ObservableListEventArgs<T> e)
        {
            ItemChanging?.Invoke(this, e);
            ItemChanged?.Invoke(this, e);
        }
        public void OnItemAdded(ObservableListEventArgs<T> e)
        {
            ItemAdding?.Invoke(this, e);
            ItemAdded?.Invoke(this, e);
        }
        public void OnItemRemoved(ObservableListEventArgs<T> e)
        {
            ItemRemoving?.Invoke(this, e);
            ItemRemoved?.Invoke(this, e);
        }
        public void OnItemsLoaded()
        {
            Loaded = true;
            ItemsLoaded?.Invoke();
        }
        public void OnItemsCleared()
        {
            ItemsCleared?.Invoke();
        }
        public new T this[int index] { get { return base[index]; } set { OnItemChanged(new ObservableListEventArgs<T>((T)value)); base[index] = (T)value; } }
        public ObservableList()
        {
        }
        public ObservableList(IEnumerable<T> values)
        {
            AddRange(values);
        }
        public new void Add(T value)
        {
            try
            {
                INotifyPropertyChanged a = (INotifyPropertyChanged)value;
                if (a != null) { a.PropertyChanged += ((s, e) => { OnItemChanged(new ObservableListEventArgs<T>(value, e.PropertyName)); }); }
            }
            catch { };
            OnItemAdded(new ObservableListEventArgs<T>(value));
            base.Add(value);
        }
        public new void AddRange(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                Add(value);
            }
        }
        public new void Clear()
        {
            base.Clear();
            OnItemsCleared();
        }
        public new void Remove(T item)
        {
            base.Remove(item);
            OnItemRemoved(new ObservableListEventArgs<T>(item));
        }
    }
}
