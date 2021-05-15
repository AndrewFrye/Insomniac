using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagement : MonoBehaviour
{
    public int iFrames;
    public int hp;
    public bool dead;

    private void Start()
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
        }
        if (hp < 1) dead = true;
        if (dead) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
