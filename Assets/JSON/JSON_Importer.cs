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
    
    void Start()
    {
        string path = Application.dataPath + "/JSON/CaveEntrance";
        List<Enemy> Enemies = new List<Enemy>();
        foreach (string file in System.IO.Directory.GetFiles(path, "*.enemy"))
        {
            string txt = File.ReadAllText(file);
            Enemy temp = JsonUtility.FromJson<Enemy>(txt);
            Enemies.Add(temp);
        }

        foreach (Enemy x in Enemies)
        {
            Debug.Log(x.Type);
            if (x.Type == "CaveBat")
            {
                Transform z = GetComponent<Transform>();
                z.position = x.Pos;

                GameObject Tmp = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/CaveBat.prefab", typeof(PrefabAssetType)) as GameObject;
                Instantiate(Tmp, z);

                GameObject Temp = Tmp;
                Temp.name = x.Name;

                EnemyHP tempHealth = Temp.GetComponent<EnemyHP>();
                tempHealth.HP = x.MaxHealth;

                PlayerInRange tempFinder = Temp.GetComponent<PlayerInRange>();
                tempFinder.Range = x.AIRange;
                tempFinder.targetTag = x.AITarget;

                
            }
        }
    }
}

[Serializable]
class Enemy {
    public string Type;
    public string AITarget;
    public string Name;
    public Vector3 Pos;
    public float AIRange;
    public int MaxHealth;
    public float RegenRate;
    public string Components;
}

