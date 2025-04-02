using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public LayerMask playerMask;
    Collider myCol;
    
    private void Start()
    {
        myCol = gameObject.GetComponent<Collider>();
        myCol.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if((1<< other.gameObject.layer & playerMask) != 0)
        {
            int curSceanNum = SceneManager.GetActiveScene().buildIndex;
            DataManager.instance.SaveData(curSceanNum - 3);
            Debug.Log(DataManager.instance.nowMap.clear);
            LoadSystem.LoadScene(curSceanNum + 1);
        }
    }
}
