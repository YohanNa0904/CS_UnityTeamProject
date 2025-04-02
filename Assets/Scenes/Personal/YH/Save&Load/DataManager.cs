using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

[System.Serializable]
public class MapData
{
    public int clear;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    string path;
    string filename = "saves";
    public MapData nowMap = new MapData();
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
        if (nowMap.clear != 0)
        {
            LoadData();
            Debug.Log(nowMap.clear);
        }
    }
    public void SaveData(int stageNum)
    {
        nowMap.clear = stageNum;
        string data = JsonUtility.ToJson(nowMap);
        File.WriteAllText(path + filename,data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowMap = JsonUtility.FromJson<MapData>(data);
    }

}
