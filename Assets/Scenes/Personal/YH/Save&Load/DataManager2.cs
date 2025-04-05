using UnityEngine;
using System.IO;

[System.Serializable]
public class MapData2
{
    public int clear;
    public bool isFirst = true;
}

public class DataManager2 : Singleton<DataManager2>
{
    string path;
    string filename = "saves";
    public MapData2 nowMap = new MapData2();

    public void Awake()
    {
        base.Initialize();
        path = Application.persistentDataPath + "/";
        LoadData();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void SaveData(int stageNum)
    {
        nowMap.clear = stageNum;
        string data = JsonUtility.ToJson(nowMap);
        File.WriteAllText(path + filename, data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowMap = JsonUtility.FromJson<MapData2>(data);
    }
}
