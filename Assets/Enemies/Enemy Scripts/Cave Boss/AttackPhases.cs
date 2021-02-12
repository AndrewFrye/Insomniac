using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhases : MonoBehaviour
{
    
}

public class Pattern
{
    private float center;
    private string shape;
    private int projectileCount;
    private float radius;
    private Vector2[] positions;

    public Pattern(string Shape, int Count, float Center, float Radius)
    {
        center = Center;
        shape = Shape;
        projectileCount = Count;
        radius = Radius;
    }

    void genPos()
    {
        string equation;
        switch (shape)
        {
            case "Circle":
                equation = "x*x + y*y = " + (radius*radius);
                break;
        }
    }
}

class circle
{
    private float radius;
    private string equation;
    private int count;
    private float angleDifference;

    public circle(float Radius, string Equation, int Count)
    {
        radius = Radius;
        equation = Equation;
        count = Count;
    }

    Vector2[] calcPointPos()
    {
        float angle = 0;
        angleDifference = 360 / count;
        Vector2[] pos = new Vector2[count - 1];
        float angleX;
        float sideX;
        float sideY;
        int quadrant;

        for(int i = 1; i <= count; i++)
        {
            if (i == 1) pos[i - 1] = new Vector2(0, radius);
            else
            {
                angle += angleDifference;
                if (angle > 90) angle -= 90;
                angleX = 180 - 90 - angle;
                sideY = angle * (Mathf.Sin((Mathf.PI / 180) * 90));
                sideX = angleX * (Mathf.Sin((Mathf.PI / 180) * 90));

                pos[i - 1] = new Vector2(sideX, sideY);
            }
            
        }

        return pos;
    }
}