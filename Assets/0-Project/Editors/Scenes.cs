using UnityEditor;
using UnityEditor.SceneManagement;

public class Scenes
{
    /*
    [MenuItem("SCENES/Elephant Scene %e", priority = 0)]
    private static void OpenElephantScene()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Elephant/elephant_scene.unity", OpenSceneMode.Single);
        }
    }
    
    [MenuItem("SCENES/Splash Scene", priority = 0)]
    private static void OpenSplashScene()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/0-Project/Scenes/Splash.unity", OpenSceneMode.Single);
        }
    }*/




    [MenuItem("Scenes/Anket %&g")]
    public static void LoadScene1()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Anket.unity");
    }

    [MenuItem("Scenes/GameScene")]
    public static void LoadScene2()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/0-Project/Scenes/GameScene.unity");
    }
    [MenuItem("Scenes/SampleScene")]
    public static void LoadScene3()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
    }
    [MenuItem("Scenes/FreeButtonScene")]
    public static void LoadScene4()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/FreeButtonSet/Scenes/SampleScene.unity");
    }
    [MenuItem("Scenes/AnketSoruları")]
    public static void LoadScene5()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/FreeButtonSet/Scenes/AnketSoruları.unity");
    }




    [MenuItem("Scenes/Restart Scene %&r")]
    private static void RestartScene()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path, OpenSceneMode.Single);
        }
    }
}