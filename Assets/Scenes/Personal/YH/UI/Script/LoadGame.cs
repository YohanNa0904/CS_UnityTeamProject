using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using UnityEditor.UI;
public class LoadGame : MonoBehaviour
{
    [SerializeField] Image image;
    void Start()
    {
        if(DataManager.instance.nowMap.isFirst == true)
        {
            image.color = new Color(1, 1, 1, 0.3f);
        }
        else
            image.color = new Color(1, 1, 1, 1f);
        
    }
    void Update()
    {
        
    }

    public void LoadScene()
    {
        if (DataManager.instance.nowMap.isFirst == false)
        {
            DataManager.instance.LoadData();
            LoadSystem.LoadScene(DataManager.instance.nowMap.clear);
        }
    }
}
