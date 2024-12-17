using System.Collections.Generic;
using UnityEngine;

namespace Levels.LevelMessage
{
    public class LevelMessageDisplayer : MonoBehaviour
    {
        [SerializeField] private List<LevelMessage> _level1Messages;
        [SerializeField] private List<LevelMessage> _level5Messages;

        private int _indexLevel = 4;

        public void Show(int levelIndex)
        {
            DeactivateAll();

            if (levelIndex == 0 && _level1Messages != null)
            {
                Activate(_level1Messages);
            }
            else if (levelIndex == _indexLevel && _level5Messages != null)
            {
                Activate(_level5Messages);
            }
        }

        private void DeactivateAll()
        {
            Deactivate(_level1Messages);
            Deactivate(_level5Messages);
        }

        private void Activate(List<LevelMessage> messages)
        {
            foreach (LevelMessage message in messages)
            {
                message.gameObject.SetActive(true);
            }
        }

        private void Deactivate(List<LevelMessage> messages)
        {
            foreach (LevelMessage message in messages)
            {
                message.gameObject.SetActive(false);
            }
        }
    }
}