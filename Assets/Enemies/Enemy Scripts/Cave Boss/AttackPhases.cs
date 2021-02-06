using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhases : MonoBehaviour
{
    public int phaseNum = 0;
    public float timer = 30;
    public GameObject Boss;
    public GameObject projectile1;
    public GameObject Player;
    public Transform BossTransform;
    public Transform PlayerTransform;

    private void Awake()
    {
        BossTransform = Boss.GetComponent<Transform>();
        PlayerTransform = Player.GetComponent<Transform>();
    }
    private void Update()
    {
        if (timer <= 0)
        {
            switch (phaseNum)
            {
                case 0:
                    phase0();
                    break;
                default:
                    break;
            }
            timer = 30;
        }
        else timer -= Time.deltaTime;
    }

    private void phase0() //Ring of projectiles
    {
        Vector3 position = BossTransform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, 180);
        int projectileNum = 0;

        for(int i = 0; i < 8; i++)
        {
            switch(projectileNum)
            {
                case 0:
                    position.y = 2;
                    position.x = 7.35f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 1:
                    position.y = 1.121f;
                    position.x = 9.471f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 2:
                    position.y = -1f;
                    position.x = 10.35f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 3:
                    position.y = -3.121f;
                    position.x = 9.471f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 4:
                    position.y = -4f;
                    position.x = 7.35f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 5:
                    position.y = -3.121f;
                    position.x = 5.229f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 6:
                    position.y = -1f;
                    position.x = 4.35f;
                    Instantiate(projectile1, position, rotation);
                    break;
                case 7:
                    position.y = 1.121f;
                    position.x = 5.229f;
                    Instantiate(projectile1, position, rotation);
                    break;

            }
            Debug.Log(projectileNum);
            projectileNum++;
            rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 45);
        }
    }
}
