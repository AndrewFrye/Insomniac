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
    public HealthManagement hpsys;

    private void Start()
    {
        GameObject x = GameObject.FindGameObjectWithTag("HealthManager");
        hpsys = x.GetComponent<HealthManagement>();
        Heart.sprite = fullHeart;
        playerMaxHealth = hpsys.hp;
    }

    private void Update()
    {
        if(!CameraMovement.zoom)
        {
            Heart.enabled = true;
            if (hpsys.hp < heartNum)
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
