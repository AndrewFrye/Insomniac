using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static int levelsCompleted = 0;
    int frames = 600;
    void Update()
    {
        frames--;
       if(frames == 0) SceneManager.LoadScene(levelsCompleted + 1, LoadSceneMode.Single);
    }
}
