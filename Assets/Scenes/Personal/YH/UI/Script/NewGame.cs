using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NweGameStart : MonoBehaviour
{
    public void NewGameStart()
    {
        DataManager.instance.nowMap.isFirst = false;
        DataManager.instance.SaveData(0);
        SceneManager.LoadScene("tutorial");
    }
}
