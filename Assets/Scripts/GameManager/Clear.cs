using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public LayerMask playerMask;
    Collider myCol;
    public int sceneNum;

    private void Awake()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        myCol = gameObject.GetComponent<Collider>();
        myCol.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if((1<< other.gameObject.layer & playerMask) != 0)
        {
            if(DataManager.instance.nowMap.clear <= sceneNum)
            {
                DataManager.instance.nowMap.clear = sceneNum+1;
                DataManager.instance.SaveData(0);
            }

            LoadSystem.LoadScene(sceneNum + 1);
        }
    }
}
