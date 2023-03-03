using System;

namespace GlobalCommander
{
    public struct OwnWeakReference<T>
    {
        public WeakReference Holder;
        public EventHandler<T> EventHandler;

        public OwnWeakReference(object obj, EventHandler<T> handler)
        {
            Holder = new WeakReference(obj);
            EventHandler = handler;
        }

        public bool IsDead()
        {
            return Holder.Target == null || Holder.Target.ToString() == "null" || !Holder.IsAlive;
        }
    }
}