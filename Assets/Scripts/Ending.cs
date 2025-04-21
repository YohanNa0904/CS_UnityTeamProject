using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Ending : MonoBehaviour
{
    bool hi = false;
    void Update()
    {
        StartCoroutine(Wait());
        if (Input.anyKey && hi) 
        {
            SceneManager.LoadScene("GameStartUI");
        }
    }
    IEnumerator Wait()
    {   
        yield return new WaitForSeconds(3.0f);
        hi = true;
    }
}
