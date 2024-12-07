using Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Level
{
    public class LevelEndMenu : BaseEndMenu<IntervalWindow>
    {
        [SerializeField] private Button _continue;

        public override void Start()
        {
            base.Start();

            _continue.onClick.AddListener(OnContinueClicked);
        }

        public override void Show()
        {
            base.Show();
        }

        private void OnContinueClicked()
        {
            Window.gameObject.SetActive(false);
            LevelLoader.ContinueToNextLevel();
        }
    }
}