using System;

namespace GlobalCommander
{
    public class InjectAction<T> : ActionContainer
    {
        public InjectAction(Action<T> inject)
        {
            this.inject = inject;
        }

        public Action<T> inject { get; }
        public override void InjectFunc(object call)
        {
            inject?.Invoke((T)call);
        }
    }
}