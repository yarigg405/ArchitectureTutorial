using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PatrolConfig", menuName = "Configs/ Enemy Patrol Movement config")]
    public class EnemyPatrolConfig : ScriptableObject
    {
        [SerializeField] private float patrolRange = 5;
        [SerializeField] private Vector2 waitBeforeNextPatrol = new Vector2(1, 5);
        [SerializeField] private bool patrolRandom = false;

        public float PatrolRange => patrolRange;
        public float WaitBeforeNextPatrol => Random.Range(waitBeforeNextPatrol.x, waitBeforeNextPatrol.y);
        public bool PatrolRandom => patrolRandom;
    }
}