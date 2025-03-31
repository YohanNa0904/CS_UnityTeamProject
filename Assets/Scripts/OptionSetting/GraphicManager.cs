using UnityEngine;

public class GraphicManager : Singleton<GraphicManager>
{
    int _resoulIndex = 0;
    public int resoulIdx
    {
        get => _resoulIndex;
        set
        {
            _resoulIndex = value;
            PlayerPrefs.SetInt("RESOUL_INDEX", _resoulIndex);
        }
    }

    int[] _resoulFigure = {0,0};
    public int[] resoulFig
    {
        get => _resoulFigure;
        set
        {
            _resoulFigure = value;
            PlayerPrefs.SetInt("RESOUL_WIDTH", _resoulFigure[0]);
            PlayerPrefs.SetInt("RESOUL_HEIGHT", _resoulFigure[1]);
        }
    }

    private void Awake()
    {
        base.Initialize();
        if (!PlayerPrefs.HasKey("RESOUL_INDEX")) _resoulIndex = 1;
        else _resoulIndex = PlayerPrefs.GetInt("RESOUL_INDEX");

        if (!PlayerPrefs.HasKey("RESOUL_INDEX")) 
            _resoulFigure = new int[] { 1920, 1080 };
        else
        {
            _resoulFigure[0] = PlayerPrefs.GetInt("RESOUL_WIDTH");
            _resoulFigure[1] = PlayerPrefs.GetInt("RESOUL_HEIGHT");
        }
    }

    public void SetResoulution(int[] resoul)
    {
        resoulFig = resoul;
        Screen.SetResolution(_resoulFigure[0], _resoulFigure[1], true);
    }
}
