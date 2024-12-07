using System.Collections.Generic;
using UnityEngine;

namespace Levels.LevelMessage
{
    public class LevelMessageDisplayer : MonoBehaviour
    {
        [SerializeField] private List<LevelMessage> _level1Messages;
        [SerializeField] private List<LevelMessage> _level5Messages;

        public void ShowLevelMessages(int levelIndex)
        {
            int index = 4;

            DeactivateAllMessages();

            if (levelIndex == 0 && _level1Messages != null)
            {
                foreach (LevelMessage message in _level1Messages)
                {
                    message.gameObject.SetActive(true);
                }
            }
            else if (levelIndex == index && _level5Messages != null)
            {
                foreach (LevelMessage message in _level5Messages)
                {
                    message.gameObject.SetActive(true);
                }
            }
        }

        private void DeactivateAllMessages()
        {
            foreach (LevelMessage message in _level1Messages)
            {
                message.gameObject.SetActive(false);
            }

            foreach (LevelMessage message in _level5Messages)
            {
                message.gameObject.SetActive(false);
            }
        }
    }
}