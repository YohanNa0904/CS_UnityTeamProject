using UnityEngine;

using Image = UnityEngine.UI.Image;

public class LoadGame : MonoBehaviour
{
    [SerializeField] Image image;

    void Start()
    {
        
        if (DataManager.instance.nowMap.isFirst)
        {
            image.color = new Color(1, 1, 1, 0.3f);
        }
        else
            image.color = new Color(1, 1, 1, 1f);
        
    }
   
    public void LoadScene()
    {
        if (!DataManager.instance.nowMap.isFirst)
        {
            DataManager.instance.LoadData();
            LoadSystem.LoadScene(DataManager.instance.nowMap.clear);
        }
    }
}
