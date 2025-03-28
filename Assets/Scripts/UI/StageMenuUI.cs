using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMenuUI : MenuUI
{
    [field: SerializeField] GameObject MenuButtonCanvas { get; set; }
    bool isMenuUI = false;
    bool isMenuButton = false;
    bool isOption = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void UIStart()
    {
        MenuButtonCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuUI) CloseMenu();
            else OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isOption) CloseMenu();
            else if (!isMenuUI) OpenOption();
        }
    }

    void OpenMenuUI()
    {
        MenuUiCanvas.SetActive(true);
        isMenuUI = true;
        Time.timeScale = 0f;
    }
    public void OpenMenu()
    {
        OpenMenuUI();
        MenuButtonCanvas.SetActive(true);
        isMenuButton = true;
    }

    public override void CloseMenu()
    {
        MenuUiCanvas.SetActive(false);
        isMenuUI = false;
        if (isMenuButton)
        {
            MenuButtonCanvas.SetActive(false);
            isMenuButton = false;
        }
        else if (isOption)
        {
            OptionCanvas.SetActive(false);
            isOption = false;
        }
        Time.timeScale = 1.0f;
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

    public override void OpenOption()
    {
        if (!isMenuUI) OpenMenuUI();
        else if (isMenuButton)
        {
            MenuButtonCanvas.SetActive(false);
            isMenuButton = false;
        }
        OptionCanvas.SetActive(true);
        isOption = true;
    }
}
