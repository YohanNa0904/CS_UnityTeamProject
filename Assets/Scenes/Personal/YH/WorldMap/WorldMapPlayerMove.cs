using System;
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
        temp = DataManager.instance.nowMap.clear;
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
        if(Input.GetKey(KeyCode.W))
        player.Translate(player.forward * Speed *Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
        player.Translate(-player.right * Speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
        player.Translate(-player.forward * Speed *Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
        player.Translate(player.right * Speed *Time.deltaTime);
        
        if (temp <= DataManager.instance.nowMap.clear)
        {
            stageTextList[temp].enabled = true;
            stageLock[temp].SetActive(false);
        }

        if(DataManager.instance.nowMap.clear >= connectSceanNum && onStage)
        {
            if(Input.GetKey(KeyCode.Space))
            LoadSystem.LoadScene(connectSceanNum);
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
}
