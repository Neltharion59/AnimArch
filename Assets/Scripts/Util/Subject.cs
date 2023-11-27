using System;
using System.Collections.Generic;

namespace Assets.Scripts.Util
{
    public class Subject<T>
    {
        private List<Action<T>> Observers;
        private T WrappedValue;

        public Subject(T Subject)
        {
            this.Observers = new List<Action<T>>();
            this.WrappedValue = Subject;
        }

        public void Register(Action<T> observer)
        {
            this.Observers.Add(observer);
        }

        public void Unregister(Action<T> observer)
        {
            this.Observers.Remove(observer);
        }

        public T GetValue()
        {
            return this.WrappedValue;
        }
        public void SetValue(T NewValue)
        {
            this.WrappedValue = NewValue;

            foreach (Action<T> observer in this.Observers)
            {
                observer(NewValue);
            }
        }
    }
}