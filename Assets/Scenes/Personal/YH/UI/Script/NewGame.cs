using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGameS : MonoBehaviour
{
    public void NewGameStart()
    {
        DataManager2.Instance.nowMap.isFirst = false;
        DataManager2.Instance.SaveData(3);
        SceneManager.LoadScene("tutorial");
    }
}
