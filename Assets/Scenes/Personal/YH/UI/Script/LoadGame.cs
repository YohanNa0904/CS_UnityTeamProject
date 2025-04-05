using UnityEngine;

using Image = UnityEngine.UI.Image;

public class LoadGame : MonoBehaviour
{
    [SerializeField] Image image;
    MapData2 saveData;

    private void Awake()
    {
        saveData = DataManager2.Instance.nowMap;
    }
    void Start()
    {
        if(saveData.isFirst == true)
        {
            image.color = new Color(1, 1, 1, 0.3f);
        }
        else
            image.color = new Color(1, 1, 1, 1f);
        
    }
   
    public void LoadScene()
    {
        if (saveData.isFirst == false)
        {
            LoadSystem.LoadScene(DataManager2.Instance.nowMap.clear);
        }
    }
}
