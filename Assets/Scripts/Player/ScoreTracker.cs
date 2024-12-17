using BaseElements.FolderKey;
using ColorService;
using UnityEngine;

namespace Player
{
    public class ScoreTracker : MonoBehaviour
    {
        private int _score;

        public int CurrentScore => _score;

        public void Add(Key key)
        {
            int points = CalculatePoints(key);

            _score += points;
        }

        private int CalculatePoints(Key key)
        {
            int pointsYellowKey = 15;
            int pointsGreenKey = 15;
            int pointsBlueKey = 10;
            int pointsRedKey = 8;

            if (key.Color == BaseColors.Blue)
            {
                return pointsBlueKey;
            }
            else if (key.Color == BaseColors.Yellow)
            {
                return pointsYellowKey;
            }
            else if (key.Color == BaseColors.Green)
            {
                return pointsGreenKey;
            }
            else if (key.Color == BaseColors.Red)
            {
                return pointsRedKey;
            }
            else
            {
                return 0;
            }
        }
    }
}