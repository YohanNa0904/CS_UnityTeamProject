using UnityEngine;
using System.IO;

[System.Serializable]
public class MapData2
{
    [SerializeField] int clear = 0;
    [SerializeField] bool isFirst = true;

    public int GetClearNum()
    {
        return clear;
    }

    public void SetClearNum(int num)
    {
        clear = num;
    }

    public bool GetIsFirst()
    {
        return isFirst;
    }

    public void SetIsFisrt(bool tf)
    {
        isFirst = tf;
    }
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {

    }

    public void SaveData(int stageNum)
    {
        nowMap.SetClearNum(stageNum);
        string data = JsonUtility.ToJson(nowMap);

        File.WriteAllText(path + filename, data);
    }
    public void LoadData()
    {
        if (File.Exists(path + filename))
        //세이브가 없을 때 로드하여 버그가 나는 것을 막기 위해서 조건문을 작성
        {
            string data = File.ReadAllText(path + filename);
            nowMap = JsonUtility.FromJson<MapData2>(data);
        }
    }
}
