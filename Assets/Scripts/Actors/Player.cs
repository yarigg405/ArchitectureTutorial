using Behaviours;
using Components;
using UnityEngine;
namespace Actors
{
    public class Player : Actor, IPlayer
    {
        protected override void Init()
        {
            GlobalCommander.Commander.RegisterInject<Player>(this);

            if (TryGetComponent<IMoveAndRotate>(out var movable))
                AddBehaviour(new PlayerStandartMoveBehaviour(movable));
            else
                Debug.LogError("Is no move component on " + gameObject.name);

            if (TryGetComponent<ICanAttack>(out var attacker))
                AddBehaviour(new PlayerStandartAttackBehaviour(attacker));
            else Debug.LogError("Is no attack component on " + gameObject.name);
        }
    }
    public interface IPlayer : IActor { }
 
        
}
