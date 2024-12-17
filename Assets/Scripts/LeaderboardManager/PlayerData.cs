using UnityEngine;
using UnityEngine.UI;

namespace LeaderboardManager
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private Text _rankText;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Image _marker;

        private string _rank;
        private string _name;
        private string _score;
        private bool _thisPlayer;

        public void SetData(string rank, string name, string score, bool isThisPlayer)
        {
            _rank = rank;
            _name = name;
            _score = score;
            _thisPlayer = isThisPlayer;
        }

        public void UpdateEntries()
        {
            if (_rankText && _rank != null || _nameText && _name != null || _scoreText && _score != null)
            {
                _rankText.text = _rank.ToString();
                _nameText.text = _name;
                _scoreText.text = _score.ToString();
            }

            if (_marker != null)
            {
                _marker.gameObject.SetActive(_thisPlayer);
            }
        }
    }
}