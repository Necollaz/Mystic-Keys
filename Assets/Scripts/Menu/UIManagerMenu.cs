using UnityEngine;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private OptionsMenu _optionsMenu;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _optionsMenu.CloseWindow();
    }

    public void ShowOptionsMenu()
    {
        _optionsMenu.OpenWindow();
        _mainMenu.gameObject.SetActive(false);
    }
}
