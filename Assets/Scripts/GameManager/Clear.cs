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
            LoadSystem.LoadScene(curSceanNum + 1);
        }
    }
}
