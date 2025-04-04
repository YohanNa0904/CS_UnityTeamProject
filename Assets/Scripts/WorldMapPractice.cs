using System;
using UnityEngine;

public class Practice : MonoBehaviour
{
    [SerializeField] Transform[] stageList;
    [SerializeField] LayerMask stageMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < stageList.Length; i++)
        {
            stageList[i].name = $"{i + 2}";
        }
        DataManager.instance.SaveData(1);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, stageMask))
        {
            if(Input.GetMouseButtonDown(0))
            {
                int connectSceanNum = Convert.ToInt32(hit.transform.name);
                if (connectSceanNum <= DataManager.instance.nowMap.clear)
                {
                    //Popup UI �� �ʿ��� ���
                    LoadSystem.LoadScene(connectSceanNum);
                }
            }
        }
        //����ĳ��Ʈ�� ���̾ Ȯ���ϰ� ������Ʈ �̸��� �̾����� ���������� ���ѹ��� ���� ��
    }
}
