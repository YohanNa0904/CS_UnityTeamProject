using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStart : MonoBehaviour
{
    public void NewGameStart()
    {
        SceneManager.LoadScene("tutorial");
    }
}
