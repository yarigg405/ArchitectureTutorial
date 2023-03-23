using HECS.Controllers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components
{
    [DefaultExecutionOrder(100)]
    public class AttackComponent : MonoBehaviour, ICanAttack
    {
        [SerializeField] private float dmg = 30;
        [SerializeField] private float attackInterval = 0.3f;
        [SerializeField] private AssetReference projectile = default;
        [SerializeField] private float attackMoveSpeed = 3f;
        [SerializeField] private DmgOwner dmgOwner = DmgOwner.DEFAULT;

        public float Dmg => dmg;
        public float AttackInterval => attackInterval;


        public bool IsReadyForAttack => InputController.Instance.HorizontalMove == 0 && InputController.Instance.VerticalMove == 0;
        public float AttackMoveSpeed => attackMoveSpeed;
        public Vector3 ShootPosition => transform.position;

        public Transform Transform => transform;

        public DmgOwner Owner => dmgOwner;

        private void Awake()
        {
            projectile.LoadAssetAsync<GameObject>();
        }


        public async Task<GameObject> GetProjectile(Vector3 position)
        {
            return await projectile.InstantiateAsync(position, Quaternion.identity).Task;
        }
    }
}