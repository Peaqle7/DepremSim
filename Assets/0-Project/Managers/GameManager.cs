using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;




    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.B))
            Debug.Break();

        if (Input.GetKeyDown(KeyCode.M))
            UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);



#endif
    }
}
