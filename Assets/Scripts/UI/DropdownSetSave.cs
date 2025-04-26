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
        //해상도 Dropdown의 선택된 값을 PlayerPrefs에 저장된 값으로 설정
        Screen.SetResolution(GraphicManager.Instance.resoulFig[0],
            GraphicManager.Instance.resoulFig[1], true);
        //선택된 값으로 해상도를 설정함
        resoulDropdown.onValueChanged.AddListener(SaveDropdownChange);
    }

    private void SaveDropdownChange(int idx)
    {
        GraphicManager.Instance.resoulIdx = idx;
        // 선택된 해상도 목차 값을 저장함
        String resoulStr = resoulDropdown.options[resoulDropdown.value].text;
        // 목차에 적은 텍스트(1920 x 720)를 문자열로 변환함
        String[] splitResoul = resoulStr.Split(" x ");
        // 문자열의 필요없는 부분을 나눠서 숫자값만 저장함(1920 720) 
        int[] intSplit = new int[splitResoul.Length];

        for(int i = 0; i < splitResoul.Length; i++)
        {
            intSplit[i] = Convert.ToInt32(splitResoul[i]);
        }
        // 문자열을 정수값으로 변환함
        GraphicManager.Instance.SetResoulution(intSplit);
        // 정수값으로 해상도를 저장하고, 그 값으로 해상도를 설정함
    }
}
