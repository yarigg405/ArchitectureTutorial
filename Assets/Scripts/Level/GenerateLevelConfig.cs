using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "LevelPrfbConfig", menuName = "Configs / Prefabs for level generation")]
public class GenerateLevelConfig : ScriptableObject
{
    [Header("Game field size")]
    [SerializeField] private Vector2 fieldSize = new Vector2(14, 10);

    [Header("Vertical symmetry")]
    [SerializeField] private bool IsHaveCentralVertical = true;

    [Header("References for level parts prefabs")]
    [SerializeField] private LevelPrefabsReference[] levelPrefabsReferences = default;

    [Header("Ster for grid building")]
    [SerializeField] private float step = 1f;

    [Header("Height by Y for mobs spawning")]
    [SerializeField] private float spawnActorsHeight = 1.5f;

    public float Step => step;
    public float SpawnActorsHeight => spawnActorsHeight;


    public void GetLevelPrfb(LevelPrefabType levelPrefabType, Vector3 coords, Transform parent)
    {
        var prfb = PrefabUtility.InstantiatePrefab(levelPrefabsReferences.FirstOrDefault(x => x.LevelPrefabType == levelPrefabType).AssetReference, parent);
        (prfb as GameObject).transform.position = coords;
    }


    public Vector2 GetLevelSize()
    {
        if (IsHaveCentralVertical)
        {
            if (fieldSize.x % 2 == 0)
                return new Vector2(fieldSize.x + 1, fieldSize.y);
        }

        return fieldSize;
    }
}

[System.Serializable]
public struct LevelPrefabsReference
{
    public string Name;
    public LevelPrefabType LevelPrefabType;
    public GameObject AssetReference;
}

public enum LevelPrefabType
{
    DEFAULT = 0,
    FLOOR = 1,
    OBSTACLE = 2,
    WALL = 3,
    EXIT = 4,
}