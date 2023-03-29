using Components;
using Configs;
using UnityEngine;

namespace Behaviours
{
    public class EnemyPatrolBehaviourContainer : MoveBehaviourContainer
    {
        [SerializeField] private EnemyPatrolConfig enemyPatrolConfig = default;
        private EnemyPatrolMoveBehaviour enemyPatrolMoveSystem;

        public override IMoveBehariour GetValue => enemyPatrolMoveSystem;

        protected override void Init()
        {
            if (TryGetComponent<IMoveAndRotate>(out var movable))
                enemyPatrolMoveSystem = new EnemyPatrolMoveBehaviour(movable, enemyPatrolConfig);
            else
                Debug.LogError("Is no movable component on " + gameObject.name);
        }
    }
}
