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
        //현재 씬의 씬 번호를 저장함
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
                DataManager.instance.SaveData(sceneNum + 1);
                // 현재 저장한 진행사항보다 더 진행했다면, 진행사항을 새로 변경함.
            }

            LoadSystem.LoadScene(sceneNum + 1);
            //현재 씬의 다음 번호를 가진 씬으로 이동
        }
    }
}
