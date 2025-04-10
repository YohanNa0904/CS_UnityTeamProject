using System;
using System.Collections;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPlayerMove : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float Speed;
    public bool onStage;
    public int connectSceanNum;
    [SerializeField] Transform[] stageList;
    [SerializeField] TextMeshPro[] stageTextList;
    [SerializeField] GameObject[] stageLock;

    int temp;
    void Start()
    {
        onStage = false;
        for (int i = 0; i < stageList.Length; i++)
        {
            stageList[i].name = $"{i + 3}";
            stageTextList[i].enabled = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        temp = DataManager.instance.nowMap.clear;

        if(Input.GetKey(KeyCode.W))
        player.Translate(player.forward * Speed *Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
        player.Translate(-player.right * Speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
        player.Translate(-player.forward * Speed *Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
        player.Translate(player.right * Speed *Time.deltaTime);
        
        StartCoroutine(UnLockStage());

        if(DataManager.instance.nowMap.clear >= connectSceanNum && onStage)
        {
            if(Input.GetKey(KeyCode.Space))
            LoadSystem.LoadScene(connectSceanNum);
            temp++;
            StopCoroutine(UnLockStage());
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        connectSceanNum = Convert.ToInt32(other.transform.name);
        onStage = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        onStage = false;     
    }
    IEnumerator UnLockStage()
{
    while (true)
    {
        if (temp <= DataManager.instance.nowMap.clear)
        {
                stageTextList[temp-3].enabled = true;
                stageLock[temp-3].SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
    /*IEnumerator UnLockStage()
    {
        while(true)
        {
            if (temp <= DataManager.instance.nowMap.clear)
            {
                temp ++;
                stageTextList[temp].enabled = true;
                stageLock[temp].SetActive(false);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}
