using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject menuUi;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject option;
    bool isMenu = false;
    bool isOption = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu) CloseMenu();
            else OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isOption) CloseMenu();
            else if (!isOption && !isMenu) OpenOption();
        }
    }

    private void OpenMenuBackGround()
    {
        menuUi.SetActive(true);
        Time.timeScale = 0f;
        isMenu = true;
    }
    public void OpenMenu()
    {
        OpenMenuBackGround();
        menuButton.SetActive(true);
        option.SetActive(false);
    }

    public void CloseMenu()
    {
        menuUi.SetActive(false);
        Time.timeScale = 1.0f;
        isMenu = false;
        isOption = false;
    }

    public void ToStartScean()
    {
        LoadSystem.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void ResetScean()
    {
        LoadSystem.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void OpenOption()
    {
        if (!isMenu) OpenMenuBackGround();
        menuButton.SetActive(false);
        option.SetActive(true);
        isOption = true;
    }
}
