using Behaviours;
using Commands;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Actors
{
    public abstract class Actor : MonoBehaviour, IActor
    {
        private List<IBehaviour> _behaviours = new List<IBehaviour>(20);

        private void Start()
        {
            GlobalCommander.Commander.RegisterObjectByEvent<ICanBePaused>(this, true);
            Init();
        }

        protected abstract void Init();

        public void AddBehaviour(IBehaviour behaviour)
        {
            if (_behaviours.Contains(behaviour))
                return;
            _behaviours.Add(behaviour);
            behaviour.Actor = this;
        }

        protected virtual void Update()
        {
            for (int i = 0; i < _behaviours.Count; i++)
                _behaviours[i].Update();
        }

        public void SetPause(bool state)
        {
            if (state)
                foreach (var b in _behaviours)
                    b.Pause();
            else
                foreach (var b in _behaviours)
                    b.UnPause();
        }

        public void Command(ICommand command)
        {
            foreach(var b in _behaviours)
                b.CommandBehaviour(command);
        }

        public void Dispose()
        {
            foreach(var b in _behaviours)
                b.Dispose();

            Destroy(gameObject);
        }
    }

    public interface ICanBePaused
    {
        void SetPause(bool state);
    }

    public interface IActor : IDisposable, ICanBePaused
    {
        void Command(ICommand command);
    }
}
