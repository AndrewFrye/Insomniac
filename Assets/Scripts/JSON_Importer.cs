using System.Numerics;
using System.Net.Mime;
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
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class JSON_Importer : MonoBehaviour
{
    GameObject empty;
    public GameObject master;
    void Start()
    {
        //pMaps();
        Enemies();
    }

    /*void pMaps(){
        string path = Application.dataPath + "/JSON/PointMaps";
        string txt = File.ReadAllText(path+"/Test.map");
        Map current = JsonUtility.FromJson<Map>(txt);

        List<Vector2> pos = new List<Vector2>();

        float distX = (float)(current.maxX-current.minX) / (float)current.countX;
        float distY = (float)(current.maxY-current.minY) / (float)current.countY;

        float currentX = current.minX;
        float currentY = current.minY;

        for(int i = 0; i < current.countY; i++){
            for(int x = 0; i < current.countX; x++){
                pos.Add(new Vector2(currentX, currentY));
                currentX+=distX;
            }
            currentY+=distY;
        }

        foreach(Vector2 x in pos){
            GameObject empty = new GameObject(x.x + "," + x.y);
            empty.AddComponent<Transform>();
            empty.GetComponent<Transform>().position.x = x.x;
            empty.GetComponent<Transform>().position.y = x.y;
        }
    }*/

    public void Enemies(){
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
            empty.tag = "JSONEnemy";

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
                        empty.AddComponent<CircleCollider2D>();
                        empty.AddComponent<Rigidbody2D>();
                        empty.GetComponent<Rigidbody2D>().gravityScale = 0;
                        empty.AddComponent<Pathfinding.AIDestinationSetter>();
                        empty.GetComponent<Pathfinding.AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
                        empty.GetComponent<CircleCollider2D>().radius = x.ColliderRadius;
                        empty.GetComponent<Pathfinding.AIPath>().orientation = Pathfinding.OrientationMode.YAxisForward;
                        empty.GetComponent<Pathfinding.AIPath>().constrainInsideGraph = false;
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

[Serializable]
class Map {
    public int countX;
    public int countY;
    public int maxX;
    public int minX;
    public int maxY;
    public int minY;
}