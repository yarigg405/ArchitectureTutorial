using Actors;
using Commands;
using System;

namespace Behaviours
{
    public abstract class BaseBehaviour : IBehaviour
    {
        public IActor Actor { get; set; }

        public abstract void Update();
        public abstract void Pause();
        public abstract void UnPause();

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual void CommandBehaviour(ICommand command) { }
    }

    public interface IBehaviour : IDisposable
    {
        IActor Actor { get; set; }

        void CommandBehaviour(ICommand command);
        void Update();
        void Pause();
        void UnPause();
    }
}
