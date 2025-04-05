using UnityEngine;
using System.IO;

[System.Serializable]
public class MapData
{
    public int clear;
    public bool isFirst = true;
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
            instance = FindFirstObjectByType<DataManager>();
            DataManager[] dataArray = FindObjectsByType<DataManager>(FindObjectsSortMode.None);
            if (FindObjectsByType<DataManager>(FindObjectsSortMode.None).Length == 2)
            {
                Destroy(dataArray[1].gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
        LoadData();
    }
    void Start()
    {
        Debug.Log(nowMap.clear);
    }
    public void SaveData(int stageNum)
    {
        nowMap.clear = stageNum;
        string data = JsonUtility.ToJson(nowMap);
        File.WriteAllText(path + filename,data);
    }
    public void LoadData()
    {
        if (File.Exists(path + filename))
        {
            string data = File.ReadAllText(path + filename);
            nowMap = JsonUtility.FromJson<MapData>(data);
        }
    }

}
