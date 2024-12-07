using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.Level
{
    public abstract class BaseEndMenu<T> : MonoBehaviour where T : MonoBehaviour
    {
        public T Window;
        public Button ExitButton;
        public LevelLoader LevelLoader;
        public ParticleSystem ParticleSystem;

        public virtual void Start()
        {
            ExitButton.onClick.AddListener(OnExitClicked);
        }

        public virtual void Show()
        {
            Window.gameObject.SetActive(true);
            Instantiate(ParticleSystem, Window.transform);
        }

        private void OnExitClicked()
        {
            Window.gameObject.SetActive(false);
            SceneManager.LoadScene("Menu");
        }
    }
}