using Actors;
using UnityEngine;

namespace Behaviours
{
    public abstract class BehaviourContainer<T> : MonoBehaviour where T : IBehaviour
    {
        public abstract T GetValue { get; }

        private void Awake()
        {
            if (TryGetComponent<ICanSetBehaviour>(out var behaviourOwner))
            {
                Init();

                if (GetValue != null)
                    behaviourOwner.AddBehaviour(GetValue);
                else
                    Debug.LogError($"Is no move behaviour in container " + gameObject.name);
            }

            else
                Debug.LogError($"Is no behaviour owner for beh {gameObject.name}  {this.name}");
        }

        protected abstract void Init();
    }

    public abstract class MoveBehaviourContainer : BehaviourContainer<IMoveBehariour>
    {
    }
}
