using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public LayerMask playerMask;
    Collider myCol;
    [SerializeField]int sceneNum;

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
            if(DataManager2.Instance.nowMap.clear <= sceneNum)
            {
                DataManager2.Instance.SaveData(sceneNum + 1);
            }

            LoadSystem.LoadScene(sceneNum + 1);
        }
    }
}
