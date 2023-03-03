using System.Collections.Generic;
using System.Linq;

namespace GlobalCommander
{
    public class EventContainer<T>
    {
        private event EventHandler<T> EventKeeper;
        private readonly HashSet<OwnWeakReference<T>> _activeListenersOfThisType = new();


        public bool HasDuplicates(object listener)
        {
            return _activeListenersOfThisType.Any(x => x.Holder == listener);
        }

        public void AddToEvent(object listener, EventHandler<T> action)
        {
            var newAction = new OwnWeakReference<T>(listener, action);
            _activeListenersOfThisType.Add(newAction);
            EventKeeper += newAction.EventHandler;
        }

        public void RemoveFromEvent(object listener)
        {
            var currentEvent = _activeListenersOfThisType.FirstOrDefault(x => x.Holder.Target == listener);
            if (currentEvent.Holder != null)
            {
                EventKeeper -= currentEvent.EventHandler;
                _activeListenersOfThisType.Remove(currentEvent);
            }
        }

        public EventContainer(object listener, EventHandler<T> action)
        {
            EventKeeper += action;
            _activeListenersOfThisType.Add(new OwnWeakReference<T>(listener, action));
        }

        public void Invoke(T t)
        {
            if (_activeListenersOfThisType.Any(x => x.IsDead()))
            {
                var failObjList = _activeListenersOfThisType.Where(x => x.IsDead());
                foreach (var fail in failObjList)
                {
                    EventKeeper -= fail.EventHandler;
                    _activeListenersOfThisType.Remove(fail);
                }
            }

            EventKeeper?.Invoke(t);
        }

        public string DebugInfo()
        {
            var info = string.Empty;
            info += _activeListenersOfThisType.Count.ToString() + "Count \n";
            foreach (var c in _activeListenersOfThisType)
            {
                if (c.Holder.Target != null)
                    info += c.Holder.Target.ToString() + "\n";
            }
            return info;
        }
    }
}