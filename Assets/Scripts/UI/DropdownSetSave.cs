using UnityEngine;
using TMPro;
using System;

public class DropdownSetSave : MonoBehaviour
{
    [SerializeField]private TMP_Dropdown resoulDropdown;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resoulDropdown.value = GraphicManager.Instance.resoulIdx;
        Screen.SetResolution(GraphicManager.Instance.resoulFig[0],
            GraphicManager.Instance.resoulFig[1], true);
        resoulDropdown.onValueChanged.AddListener(SaveDropdownChange);
    }

    private void SaveDropdownChange(int idx)
    {
        GraphicManager.Instance.resoulIdx = idx;
        String resoulStr = resoulDropdown.options[resoulDropdown.value].text;
        String[] splitResoul = resoulStr.Split(" x ");
        int[] intSplit = new int[splitResoul.Length];

        for(int i = 0; i < splitResoul.Length; i++)
        {
            intSplit[i] = Convert.ToInt32(splitResoul[i]);
        }
        GraphicManager.Instance.SetResoulution(intSplit);
    }
}
