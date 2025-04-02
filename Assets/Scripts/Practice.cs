using System;
using UnityEngine;

public class Practice : MonoBehaviour
{
    GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Physics.Raycast())
        int connectSceanNum = Convert.ToInt32(obj.name);
        if(connectSceanNum >= DataManager.instance.nowMap.clear)
        {

        }
    }
}
