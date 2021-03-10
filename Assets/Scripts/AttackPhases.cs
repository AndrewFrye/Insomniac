using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhases : MonoBehaviour
{
    public GameObject projectile;
    bool temp = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(!temp)
        {
            Pattern pattern = new Pattern("Circle", 8, 0f, 10f);
            pattern.genPos();
            for (int i = 0; i < 8; i++)
            {
                Instantiate(projectile, new Vector3(pattern.positions[i].x, pattern.positions[i].y, 0), Quaternion.Euler(0, 0, 0));
            }
            temp = true;
        }
    }
}

public class Pattern
{
    private float center;
    private string shape;
    private int projectileCount;
    private float radius;
    public Vector2[] positions;

    public Pattern(string Shape, int Count, float Center, float Radius)
    {
        center = Center;
        shape = Shape;
        projectileCount = Count;
        radius = Radius;
    }

    public void genPos()
    {
        switch (shape)
        {
            case "Circle":
                circle Circle = new circle(radius, projectileCount);
                positions = Circle.calcPointPos();
                break;
        }
    }
}

class circle
{
    private float radius;
    private int count;
    private float angleDifference;

    public circle(float Radius, int Count)
    {
        radius = Radius;
        count = Count;
    }

    public Vector2[] calcPointPos()
    {
        float angle = 0;
        angleDifference = 360 / count;
        Vector2[] pos = new Vector2[count];
        float angleX;
        float sideX;
        float sideY;
        //int quadrant;

        for(int i = 1; i <= count; i++)
        {
            if (i == 1) pos[i - 1] = new Vector2(0, radius);
            else
            {
                angle += angleDifference;
                if (angle > 90) angle -= 90;
                angleX = 180 - 90 - angle;
                sideY = angle * (Mathf.Sin((Mathf.PI / 180) * 90));
                Debug.Log(sideY);
                sideX = angleX * (Mathf.Sin((Mathf.PI / 180) * 90));

                pos[i - 1] = new Vector2(sideX, sideY);
            }
            
        }

        return pos;
    }
}