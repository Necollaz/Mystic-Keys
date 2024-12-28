using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace LeaderboardManager
{
    public class Leaderboard : MonoBehaviour
    {
        private const string NameLeaderboard = "LeaderBoard";
        private const string Ru = "ru";
        private const string En = "en";
        private const string Tr = "tr";
        private const string AnonimDataRu = "Нет данных";
        private const string AnonimDataEn = "No data";
        private const string AnonimDataTr = "Veri yok";
        private const string AnonimNameRu = "Аноним";
        private const string AnonimNameEn = "Anonymous";
        private const string AnonimNameTr = "Anonim";
        
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private VerticalLayoutGroup _currentPlayerLayoutGroup;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PlayerData _currentPlayerDataPrefab;
        [SerializeField] private int _maxPlayer = 7;

        private PlayerData _currentPlayerEntry;
        private readonly List<PlayerData> _playerDataEntries = new List<PlayerData>();

        private void OnEnable()
        {
            YandexGame.onGetLeaderboard += OnUpdate;
            RequestLeaderboardData();
        }

        private void OnDisable()
        {
            YandexGame.onGetLeaderboard -= OnUpdate;
        }

        private void RequestLeaderboardData()
        {
            string leaderboardName = NameLeaderboard;
            int maxQuantityPlayers = _maxPlayer;
            int quantityTop = _maxPlayer;
            int quantityAround = 0;

            YandexGame.GetLeaderboard(leaderboardName, maxQuantityPlayers, quantityTop, quantityAround, null);
        }

        private void OnUpdate(LBData lbData)
        {
            if (lbData.technoName != NameLeaderboard)
            {
                return;
            }

            foreach (PlayerData entry in _playerDataEntries)
            {
                Destroy(entry.gameObject);
            }

            _playerDataEntries.Clear();

            if (_currentPlayerEntry != null)
            {
                Destroy(_currentPlayerEntry.gameObject);

                _currentPlayerEntry = null;
            }

            if (lbData.entries == AnonimDataEn)
            {
                string noDataMessage = YandexGame.savesData.language switch
                {
                    Ru => AnonimDataRu,
                    En => AnonimDataEn,
                    Tr => AnonimDataTr,
                    _ => "...",
                };

                PlayerData playerData = Instantiate(_playerData, _gridLayout.transform);
                playerData.SetData("-", noDataMessage, "-", false);
                playerData.UpdateEntries();
                _playerDataEntries.Add(playerData);
            }
            else
            {
                LBPlayerData[] players = lbData.players;

                for (int i = 0; i < players.Length; i++)
                {
                    LBPlayerData player = players[i];
                    PlayerData playerData = Instantiate(_playerData, _gridLayout.transform);

                    string playerName = string.IsNullOrEmpty(player.name) ? GetAnonimName() : player.name;
                    bool isCurrentUser = player.uniqueID == YandexGame.playerId;

                    playerName = LBMethods.AnonimName(playerName);
                    playerData.SetData(player.rank.ToString(), playerName, player.score.ToString(), isCurrentUser);
                    playerData.UpdateEntries();

                    _playerDataEntries.Add(playerData);
                }

                if (lbData.thisPlayer != null)
                {
                    string currentPlayerName = string.IsNullOrEmpty(YandexGame.playerName) ? GetAnonimName() : YandexGame.playerName;
                    string currentPlayerScore = lbData.thisPlayer.score.ToString();
                    string currentPlayerRank = lbData.thisPlayer.rank.ToString();

                    _currentPlayerEntry = Instantiate(_currentPlayerDataPrefab, _currentPlayerLayoutGroup.transform);
                    _currentPlayerEntry.SetData(currentPlayerRank, currentPlayerName, currentPlayerScore, true);
                    _currentPlayerEntry.UpdateEntries();
                }
            }
        }

        private string GetAnonimName()
        {
            return YandexGame.savesData.language switch
            {
                Ru => AnonimNameRu,
                En => AnonimNameEn,
                Tr => AnonimNameTr,
                _ => AnonimNameEn,
            };
        }
    }
}