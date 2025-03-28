using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [field: SerializeField] protected GameObject MenuUiCanvas { get; private set; }
    [field: SerializeField] protected GameObject OptionCanvas { get; private set; }

    private void Start()
    {
        MenuUiCanvas.SetActive(false);
        OptionCanvas.SetActive(false);
        UIStart();
    }

    protected virtual void UIStart()
    {

    }
    public virtual void OpenOption()
    {
        MenuUiCanvas.SetActive(true);
        OptionCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public virtual void CloseMenu() 
    {
        MenuUiCanvas.SetActive(false);
        OptionCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void GameExit()
    {

    }
}
