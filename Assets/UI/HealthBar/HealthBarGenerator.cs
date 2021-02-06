using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarGenerator : MonoBehaviour
{
    public int playerMaxHealth;
    public int heartNum;
    public SpriteRenderer Heart;
    public Sprite emptyHeart;
    public Sprite fullHeart;

    private void Start()
    {
        Heart.sprite = fullHeart;
    }
    private void Awake()
    {
        playerMaxHealth = HealthManagement.hp;
    }

    private void Update()
    {
        if(!CameraMovement.zoom)
        {
            Heart.enabled = true;
            if (HealthManagement.hp < heartNum)
            {
                Heart.sprite = emptyHeart;
                if(heartNum == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
            else if (Heart.sprite != fullHeart) Heart.sprite = fullHeart;
        }
        else if(CameraMovement.zoom)
        {
            Heart.enabled = false;
        }
    }
}
