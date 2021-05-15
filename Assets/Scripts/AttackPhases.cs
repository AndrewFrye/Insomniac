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
    public List<Vector2> positions;

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

    public List<Vector2> calcPointPos()
    {
        List<Vector2> pos = new List<Vector2>();
        float angle = 0;
        float singleAngle;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        int quadrant = 0;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

        singleAngle = 360f / count;
        
        for(int i = 0; i < count; i++)
        {
            while (angle > 90) angle -= 90;
            if (i == 0) pos.Add(new Vector2(0, radius));

            float sinBase = 7f/Mathf.Sin(Mathf.Deg2Rad * 90);

            float y = sinBase * Mathf.Sin(Mathf.Deg2Rad * angle);
            float x = sinBase * Mathf.Sin(Mathf.Deg2Rad * (90 - angle));

            switch (quadrant)
            {
                case 1:
                    pos.Add(new Vector2(x, y));
                    break;
                case 2:
                    pos.Add(new Vector2(-x, y));
                    break;
                case 3:
                    pos.Add(new Vector2(-x, -y));
                    break;
                case 4:
                    pos.Add(new Vector2(x, -y));
                    break;
                default:
                    break;
            }

            angle += singleAngle;
            if (angle < 90) quadrant = 1;
            else if (angle >= 270) quadrant = 4;
            else if (angle >= 180) quadrant = 3;
            else if (angle >= 90) quadrant = 2;
        }


        return pos;
    }
}