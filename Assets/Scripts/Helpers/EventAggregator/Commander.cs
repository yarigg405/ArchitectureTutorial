using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public delegate void EventHandler<in T>(T e);

namespace GlobalCommander
{
    public static class Commander
    {
        private static Dictionary<Type, object> _globalListeners = new();
        private static Dictionary<Type, List<ActionContainer>> _delayedInjection = new();
        private static Dictionary<Type, object> _singleInjections = new();

        static Commander()
        {
            SceneManager.sceneUnloaded += ClearGlobalListeners;
        }

        private static void ClearGlobalListeners(Scene scene)
        {
            ForceClean();
        }

        public static void ForceClean()
        {
            _globalListeners = new Dictionary<Type, object>();
            _delayedInjection = new Dictionary<Type, List<ActionContainer>>();
            _singleInjections = new Dictionary<Type, object>();
        }

        public static void AddListener<T>(object listener, Action<T> action)
        {
            var key = typeof(T);
            EventHandler<T> handler = new EventHandler<T>(action);

            if (_globalListeners.ContainsKey(key))
            {
                var lr = (EventContainer<T>)_globalListeners[key];
                if (lr.HasDuplicates(listener))
                    return;
                lr.AddToEvent(listener, handler);
                return;
            }
            _globalListeners.Add(key, new EventContainer<T>(listener, handler));
        }

        public static void Invoke<T>(T data)
        {
            var key = typeof(T);
            if (!_globalListeners.ContainsKey(key))
                return;
            var eventContainer = (EventContainer<T>)_globalListeners[key];
            eventContainer.Invoke(data);
        }

        public static void RegisterInject<T>(T obj)
        {
            var key = typeof(T);
            if (_delayedInjection.ContainsKey(key))
            {
                foreach (var delayed in _delayedInjection[key])
                    delayed.InjectFunc(obj);

                _delayedInjection[key].Clear();
                _delayedInjection.Remove(key);
            }

            _singleInjections.Add(key, obj);
        }

        public static void Inject<T>(Action<T> obj)
        {
            var key = typeof(T);

            if (_singleInjections.ContainsKey(key))
            {
                if (_singleInjections[key] == null)
                {
                    _singleInjections.Remove(key);
                    Inject(obj);
                    return;
                }

                obj.Invoke((T)_singleInjections[key]);
            }
            else
            {
                if (_delayedInjection.ContainsKey(key))
                    _delayedInjection[key].Add(new InjectAction<T>(obj));
                else
                    _delayedInjection.Add(key, new List<ActionContainer> { new InjectAction<T>(obj) });
            }
        }

        public static void RemoveListener<T>(object listener)
        {
            var key = typeof(T);
            if (!_globalListeners.ContainsKey(key)) return;
            var eventContainer = (EventContainer<T>)_globalListeners[key];
            eventContainer.RemoveFromEvent(listener);
        }

        public static void ReleaseListener(object listener)
        {
            foreach (var l in _globalListeners)
                (l.Value as ICanRemoveEvent)?.RemoveFromEvent(listener);
        }

        public static void RegisterObjectByEvent<T>(T registerObject, bool stateType)
        {
            Invoke(new Register<T> { RegisterObject = registerObject, Add = stateType });
        }

        public static void ReceiveRegisterObject<T>(object owner, List<T> collection)
        {
            AddListener(owner, (Register<T> obb) => collection.RegisterEventObjectAtList(obb));
        }

        public static void ReceiveRegisterObject<T>(object owner, HashSet<T> collection)
        {
            AddListener(owner, (Register<T> obb) => collection.RegisterEventObjectAtList(obb));
        }

        public static void AddOrRemoveElement<T>(this List<T> list, T element, bool add)
        {
            if (add)
            {
                if (list.Contains(element))
                    return;
                list.Add(element);
            }
            else
                if (list.Contains(element))
                list.Remove(element);
        }

        public static void AddOrRemoveElement<T>(this HashSet<T> list, T element, bool add)
        {
            if (add)
            {
                if (list.Contains(element))
                    return;
                list.Add(element);
            }
            else
                if (list.Contains(element))
                list.Remove(element);
        }

        public static void RegisterEventObjectAtList<T>(this List<T> registrationList, Register<T> registerEvent)
        {
            if (registerEvent.Add)
            {
                if (registrationList.Contains(registerEvent.RegisterObject))
                    return;
                registrationList.Add(registerEvent.RegisterObject);
            }
            else
                if (registrationList.Contains(registerEvent.RegisterObject))
                registrationList.Remove(registerEvent.RegisterObject);
        }
        public static void RegisterEventObjectAtList<T>(this HashSet<T> registrationList, Register<T> registerEvent)
        {
            if (registerEvent.Add)
            {
                if (registrationList.Contains(registerEvent.RegisterObject))
                    return;
                registrationList.Add(registerEvent.RegisterObject);
            }
            else
               if (registrationList.Contains(registerEvent.RegisterObject))
                registrationList.Remove(registerEvent.RegisterObject);
        }

        public static string DebugInfo()
        {
            var info = string.Empty;

            foreach (var listener in _globalListeners)
            {
                info += "Type the objects are subscribed to" + listener.Key.ToString() + "\n";
                var t = (IDebuggable)listener.Value;
                info += t.DebugInfo() + "\n";
            }

            return info;
        }
    }
}

