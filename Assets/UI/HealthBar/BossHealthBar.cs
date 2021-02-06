using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthBar : MonoBehaviour
{
    public int heartNum;
    public SpriteRenderer Heart;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public GameObject boss;
    public BossHealth bossHealth;


    private void Start()
    {
        Heart.sprite = fullHeart;
    }
    private void Awake()
    {
        bossHealth = new BossHealth();
        bossHealth.bossHP = boss.GetComponent<EnemyHP>();
        bossHealth.setMaxHealth();
    }

    private void Update()
    {
        bossHealth.updateHealth();
        if (bossHealth.bossCurrentHealth < heartNum) Heart.sprite = emptyHeart;
        else if (Heart.sprite != fullHeart) Heart.sprite = fullHeart;
    }
}

public class BossHealth
{
    public int bossMaxHealth;
    public int bossCurrentHealth;
    public EnemyHP bossHP;
    public void updateHealth()
    {
        bossCurrentHealth = bossHP.HP;
    }

    public void setMaxHealth()
    {
        bossMaxHealth = bossHP.HP;
    }
}
