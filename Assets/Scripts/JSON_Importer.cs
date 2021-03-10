using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.U2D;

public class JSON_Importer : MonoBehaviour
{
    GameObject empty;
    public GameObject master;
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
                        empty.AddComponent<SpriteLibrary>();
                        empty.AddComponent<SpriteResolver>();
                        empty.GetComponent<SpriteLibrary>().spriteLibraryAsset = master.GetComponent<SpriteLibrary>().spriteLibraryAsset;
                        empty.GetComponent<SpriteResolver>().SetCategoryAndLabel(x.SpriteCategory, x.SpriteLabel);
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
    public string SpriteLabel;
    public string SpriteCategory;
    public float Gravity;
    public float ColliderRadius;
}

