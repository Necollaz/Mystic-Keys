using UnityEngine;

namespace Levels
{
    public class ProgressDataStorage
    {
        private const string CurrentLevelIndexKey = "CurrentLevelIndex";
        private const string RandomLevelPhaseKey = "IsRandomLevelPhase";
        private const string TotalLevelsCompletedKey = "TotalLevelsCompleted";

        public void Save(int currentLevelIndex, bool isRandomLevelPhase)
        {
            PlayerPrefs.SetInt(CurrentLevelIndexKey, currentLevelIndex);
            PlayerPrefs.SetInt(RandomLevelPhaseKey, isRandomLevelPhase ? 1 : 0);
        }

        public bool HasSaved()
        {
            return PlayerPrefs.HasKey(CurrentLevelIndexKey);
        }

        public int LoadLevelIndex()
        {
            return PlayerPrefs.GetInt(CurrentLevelIndexKey);
        }

        public bool IsRandomLevelPhase()
        {
            return PlayerPrefs.GetInt(RandomLevelPhaseKey) == 1;
        }

        public void Reset()
        {
            PlayerPrefs.DeleteKey(CurrentLevelIndexKey);
            PlayerPrefs.DeleteKey(RandomLevelPhaseKey);
        }

        public void SaveTotalCompleted(int totalLevels)
        {
            PlayerPrefs.SetInt(TotalLevelsCompletedKey, totalLevels);
        }

        public int LoadTotalCompleted()
        {
            return PlayerPrefs.GetInt(TotalLevelsCompletedKey, 0);
        }
    }
}