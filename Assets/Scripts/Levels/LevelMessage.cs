using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMessage : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _messageTextComponent;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseMessage);
    }

    private void CloseMessage()
    {
        gameObject.SetActive(false);
    }
}