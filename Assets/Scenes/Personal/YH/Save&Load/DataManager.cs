using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class Map
{
    public int clear;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    string path;
    string filename = "saves";
    Map nowMap = new Map();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }
    void Start()
    {
        SaveData();
        print(path);
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowMap);
        File.WriteAllText(path + filename,data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowMap = JsonUtility.FromJson<Map>(data);
    }
}
