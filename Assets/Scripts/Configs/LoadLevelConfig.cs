using Components;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs
{
    [CreateAssetMenu(fileName = "Levels config", menuName = "Configs/Levels config")]
    class LoadLevelConfig : ScriptableObject   
    {
        [SerializeField] public AssetReference[] levels = default;

        public async Task<LevelComponent> GetLevelByIndex(int index)
        {
            if (levels == null || levels.Length == 0)
            {
                Debug.LogError("Levels not set");
                return default;
            }

            var loadLvl = await levels[index].InstantiateAsync().Task;

            if (loadLvl.TryGetComponent<LevelComponent>(out var levelComponent))
                return levelComponent;
            else
                Debug.LogError("Is no levelComponent on loaded level " + name);

            return default;
        }
    }
}