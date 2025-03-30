using UnityEngine;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    private TMP_Dropdown resoulDropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resoulDropdown = GetComponent<TMP_Dropdown>();
        resoulDropdown.value = GraphicManager.Instance.resoulIdx;
        resoulDropdown.onValueChanged.AddListener(idx => 
            {
            Debug.Log("Do");
            GraphicManager.Instance.resoulIdx = idx;
            }
        );
    }

    private void Update()
    {
        /*
        Debug.Log(resoulDropdown.value);
        Debug.Log(GraphicManager.Instance.resoulIdx);
        */
    }
}
