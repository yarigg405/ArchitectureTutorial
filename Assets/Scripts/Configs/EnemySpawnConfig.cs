using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy config")]
    class EnemySpawnConfig : ScriptableObject
    {
        [SerializeField] private int enemyCount = 3;
        [SerializeField] private EnemyContainer[] enemyContainers = default;

        public EnemyContainer[] EnemyContainers { get => enemyContainers; }

        public int EnemiesCount => enemyCount;


        private void OnEnable()
        {

        }

        public async Task<GameObject> GetRandomEnemy(Vector3 position)
        {
            if (enemyContainers == null || enemyContainers.Length == 0)
            {
                Debug.LogError("Enemies array is not set");
                return default;
            }

            var random = Random.Range(0, enemyContainers.Length);
            return await enemyContainers[random].EnemyPrfb.InstantiateAsync(position, Quaternion.identity).Task;
        }
    }
}

[System.Serializable]
public struct EnemyContainer
{
    public string Name;
    public AssetReference EnemyPrfb;
}

