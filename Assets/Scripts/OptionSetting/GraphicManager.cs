using UnityEngine;

public class GraphicManager : Singleton<GraphicManager>
{
    int _resoulIndex;

    public int resoulIdx
    {
        get => _resoulIndex;
        set
        {
            PlayerPrefs.SetInt("RESOUL_INDEX", _resoulIndex);
        }
    }
    private void Awake()
    {
        base.Initialize();
        _resoulIndex = PlayerPrefs.GetInt("RESOUL_INDEX");
    }

}
