using System;

/**  This script demonstrates implementation of the Observer Pattern.
 *  If you're interested in learning about Observer Pattern, 
 *  you can find a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 **/

namespace StatePattern.Events
{
    public class EventController<T>
    {
        public event Action<T> baseEvent;
        public void InvokeEvent(T type) => baseEvent?.Invoke(type);
        public void AddListener(Action<T> listener) => baseEvent += listener;
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }
    
    public class EventController
    {
        public event Action baseEvent;
        public void InvokeEvent() => baseEvent?.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;
    }
}