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
        
        public void SetData(string rankUser, string nameUser, string score, bool isThisPlayer)
        {
            _rank = rankUser;
            _name = nameUser;
            _score = score;
            _thisPlayer = isThisPlayer;
        }
        
        public void UpdateEntries()
        {
            if (_rankText != null && _rank != null && _nameText != null && _name != null && _scoreText != null && _score != null)
            {
                _rankText.text = _rank;
                _nameText.text = _name;
                _scoreText.text = _score;
            }
            
            if (_marker != null)
            {
                _marker.gameObject.SetActive(_thisPlayer);
            }
        }
    }
}