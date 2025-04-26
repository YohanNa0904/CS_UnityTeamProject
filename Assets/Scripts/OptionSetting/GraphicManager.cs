using UnityEngine;

public class GraphicManager : Singleton<GraphicManager>
{
    int _resoulIndex = 0;
    // 해상도 설정에서 나오는 해상도 목차의 순서를 저장함
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
    //실제로 적용할 해상도 값을 저장함
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
        //설정된 값이 없다면 해상도 목차는 두번째로, 있다면 설정된 값으로

        if (!PlayerPrefs.HasKey("RESOUL_WIDTH")) 
            _resoulFigure = new int[] { 1920, 1080 };
        else
        {
            _resoulFigure[0] = PlayerPrefs.GetInt("RESOUL_WIDTH");
            _resoulFigure[1] = PlayerPrefs.GetInt("RESOUL_HEIGHT");
        }
        //설정된 값이 없다면 해상도 1080p로, 있다면 설정된 값으로
    }

    public void SetResoulution(int[] resoul)
    {
        resoulFig = resoul;
        Screen.SetResolution(_resoulFigure[0], _resoulFigure[1], true);
        //설정된 해상도를 실제 해상도로 적용함
    }
}
