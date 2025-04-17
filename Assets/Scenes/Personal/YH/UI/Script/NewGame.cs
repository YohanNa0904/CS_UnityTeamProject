using UnityEngine;
using UnityEngine.SceneManagement;
public class NewGameS : MonoBehaviour
{
    public void NewGameStart()
    {
        DataManager.instance.nowMap.isFirst = false;
        DataManager.instance.SaveData(3);
        SceneManager.LoadScene("tutorial MG");
    }
}
