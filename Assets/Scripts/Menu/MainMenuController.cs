using UnityEngine;

public class MainMenuController : MonoBehaviour
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
        _optionsMenu.SetBackButtonAction(() =>
        {
            _optionsMenu.CloseWindow();
            ShowMainMenu();
        });
        _mainMenu.gameObject.SetActive(false);
    }
}
