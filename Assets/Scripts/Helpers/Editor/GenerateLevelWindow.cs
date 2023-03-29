using UnityEditor;
using UnityEngine;

public class GenerateLevelWindow : EditorWindow
{
    private Object generateConfigRef;

    [MenuItem("TutorialProj/Generate Level")]
    public static void OpenGenerateLevelWindow()
    {
        GetWindow<GenerateLevelWindow>();
    }

    private void OnEnable()
    {
        ShowModal();
    }

    private void OnGUI()
    {
        generateConfigRef = EditorGUILayout.ObjectField(generateConfigRef, typeof(GenerateLevelConfig), true);

        if (GUILayout.Button("Generate Level"))
            GenerateLevel();
    }

    private void GenerateLevel()
    {
        if (generateConfigRef == null)
        {
            Debug.LogError("No level generator config");
            return;
        }

        var generateConfig = generateConfigRef as GenerateLevelConfig;

        float width = 0;
        float height = 0;

        var levelHolder = new GameObject();


        for (int i = 0; i < generateConfig.GetLevelSize().x; i++)
        {
            for (int j = 0; j < generateConfig.GetLevelSize().y; j++)
            {
                generateConfig.GetLevelPrfb(LevelPrefabType.FLOOR, new Vector3(width, 0, height), levelHolder.transform);
                height += generateConfig.Step;
            }

            width += generateConfig.Step;
            height = 0;
        }
    }
}
