using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class JSON_Importer : MonoBehaviour
{
    GameObject empty;
    void Start()
    {
        string path = Application.dataPath + "/JSON/Enemies";
        List<Enemy> Enemies = new List<Enemy>();
        foreach (string file in System.IO.Directory.GetFiles(path, "*.enemy"))
        {
            string txt = File.ReadAllText(file);
            Enemy temp = JsonUtility.FromJson<Enemy>(txt);
            Enemies.Add(temp);
        }

        foreach (Enemy x in Enemies)
        {
            GameObject empty = new GameObject(x.Name);

            foreach (string y in x.Components)
            {
                switch (y)
                {
                    case "transform":
                        empty.GetComponent<Transform>().position = x.Pos;
                        break;
                    case "spriterenderer":
                        empty.AddComponent<SpriteRenderer>();
                        var bytes = System.IO.File.ReadAllBytes(Application.dataPath + x.Sprite);
                        var tex = new Texture2D(1, 1);
                        tex.LoadImage(bytes);
                        Sprite img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 32f);
                        empty.GetComponent<SpriteRenderer>().sprite = img;
                        break;
                    case "aipath":
                        empty.AddComponent<Pathfinding.AIPath>();
                        empty.AddComponent<PlayerInRange>();
                        empty.AddComponent<CircleCollider2D>();
                        empty.AddComponent<Rigidbody2D>();
                        empty.GetComponent<Rigidbody2D>().gravityScale = 0;
                        empty.AddComponent<Pathfinding.AIDestinationSetter>();
                        empty.GetComponent<CircleCollider2D>().radius = x.ColliderRadius;
                        empty.GetComponent<PlayerInRange>().targetTag = x.AITarget;
                        empty.GetComponent<PlayerInRange>().target = empty.GetComponent<Pathfinding.AIDestinationSetter>();
                        empty.GetComponent<PlayerInRange>().Range = x.AIRange;
                        empty.GetComponent<Pathfinding.AIPath>().orientation = Pathfinding.OrientationMode.YAxisForward;
                        break;
                    case "enemyhp":
                        empty.AddComponent<EnemyHP>();
                        empty.GetComponent<EnemyHP>().HP = x.MaxHealth;
                        break;
                }
            }
        }
    }
}

[Serializable]
class Enemy {
    public string AITarget;
    public string Name;
    public Vector3 Pos;
    public float AIRange;
    public int MaxHealth;
    public float RegenRate;
    public string[] Components;
    public string Sprite;
    public float Gravity;
    public float ColliderRadius;
}

