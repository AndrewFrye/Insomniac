using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagement : MonoBehaviour
{
    public static int iFrames;
    public static int hp;
    public static bool dead;

    private void Awake()
    {
        hp = 3;
        iFrames = 0;
        dead = false;
        
    }

    private void Update()
    {
        if (iFrames > 0) iFrames--;
        if (EnemyCollisionDetect.hit)
        {
            hp--;
            iFrames = 30;
            Debug.Log("Player has been hit");
            if (hp <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        
    }
}
