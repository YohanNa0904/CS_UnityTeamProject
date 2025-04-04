using System;
using UnityEngine;

public class WorldMapPlayerMove : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float Speed;
    public bool onStage;
    public int connectSceanNum;
    [SerializeField] Transform[] stageList;

    void Start()
    {
        onStage = false;
        for (int i = 0; i < stageList.Length; i++)
        {
            stageList[i].name = $"{i + 2}";
        }
        print(DataManager.instance.nowMap.clear);
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
    
        if(DataManager.instance.nowMap.clear >= connectSceanNum && onStage)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                LoadSystem.LoadScene(connectSceanNum);
                print("hi");
            }
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        connectSceanNum = Convert.ToInt32(other.transform.name);
        onStage = true;
        print(connectSceanNum);
        print(onStage);
    }
    
    void OnTriggerExit(Collider other)
    {
        onStage = false;     
        print(onStage);
    }
}
