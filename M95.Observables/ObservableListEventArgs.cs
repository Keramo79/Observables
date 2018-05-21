using System;
using System.Collections.Generic;
using System.Text;

namespace M95.Observables
{
    public class ObservableListEventArgs<T> : EventArgs
    {
        public object sender { get; set; }
        public T Item { get; set; }
        public string PropertyName { get; set; }
        public ObservableListEventArgs(T item, string propertyName = "")
        {
            this.PropertyName = propertyName;
            this.Item = item;
        }
    }
}
