using System;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public int saveVersion = 4;
        public int publicTrust = 12;
        public int money = 850;
        public int reputation = 4;
        public int missionStep;
        public int chapterOneStep;
        public int chapterTwoStep;
        public int chapterThreeStep;
        public int chapterFourStep;
        public bool chapterOneComplete;
        public bool chapterTwoComplete;
        public bool chapterThreeComplete;
        public bool chapterFourComplete;
        public int rescueApproach;
        public int highestUnlockedChapter = 1;
        public int lastPlayedChapter = 1;
    }

    public sealed class GameSession : MonoBehaviour
    {
        private const string SaveFileName = "neta-ji-prototype-save.json";
        public static GameSession Instance { get; private set; }
        public event Action ProgressChanged;

        [SerializeField] private PlayerProgress progress = new PlayerProgress();
        private string SavePath => GetSavePath();

        public PlayerProgress Progress => progress;
        public static bool HasSave => File.Exists(GetSavePath());
        public static int HighestUnlockedChapter => GetSavedChapterState().highestUnlockedChapter;
        public static int LastPlayedChapter => Mathf.Clamp(GetSavedChapterState().lastPlayedChapter, 1, 4);

        public static void DeleteSave()
        {
            try
            {
                string path = GetSavePath();
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Save reset failed: {exception.Message}");
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Load();
        }

        public void ApplyReward(int trust, int money, int reputation)
        {
            progress.publicTrust = Mathf.Clamp(progress.publicTrust + trust, 0, 100);
            progress.money = Mathf.Max(0, progress.money + money);
            progress.reputation = Mathf.Clamp(progress.reputation + reputation, 0, 100);
            Save();
        }

        public int GetMissionStep(int chapterNumber)
        {
            switch (chapterNumber)
            {
                case 2:
                    return progress.chapterTwoStep;
                case 3:
                    return progress.chapterThreeStep;
                case 4:
                    return progress.chapterFourStep;
                default:
                    return progress.chapterOneStep;
            }
        }

        public void SetMissionStep(int chapterNumber, int step)
        {
            int safeStep = Mathf.Max(0, step);
            if (chapterNumber == 4)
            {
                progress.chapterFourStep = safeStep;
            }
            else if (chapterNumber == 3)
            {
                progress.chapterThreeStep = safeStep;
            }
            else if (chapterNumber == 2)
            {
                progress.chapterTwoStep = safeStep;
            }
            else
            {
                progress.chapterOneStep = safeStep;
                progress.missionStep = safeStep;
            }
            progress.lastPlayedChapter = Mathf.Clamp(chapterNumber, 1, 4);
            Save();
        }

        public void ResetChapter(int chapterNumber)
        {
            if (chapterNumber == 4)
            {
                progress.rescueApproach = 0;
            }
            SetMissionStep(chapterNumber, 0);
        }

        public void CompleteChapter(int chapterNumber)
        {
            if (chapterNumber == 4)
            {
                progress.chapterFourComplete = true;
                progress.lastPlayedChapter = 4;
            }
            else if (chapterNumber == 3)
            {
                progress.chapterThreeComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 4);
                progress.lastPlayedChapter = 4;
            }
            else if (chapterNumber == 2)
            {
                progress.chapterTwoComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 3);
                progress.lastPlayedChapter = 3;
            }
            else
            {
                progress.chapterOneComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 2);
                progress.lastPlayedChapter = 2;
            }
            Save();
        }

        public void SetStoryDecision(string key, int option)
        {
            if (key == "rescue-approach")
            {
                progress.rescueApproach = Mathf.Clamp(option, 1, 2);
                Save();
            }
        }

        public void SetLastPlayedChapter(int chapterNumber)
        {
            int safeChapter = Mathf.Clamp(chapterNumber, 1, Mathf.Max(1, progress.highestUnlockedChapter));
            if (progress.lastPlayedChapter != safeChapter)
            {
                progress.lastPlayedChapter = safeChapter;
                Save();
            }
        }

        public void ResetProgress()
        {
            progress = new PlayerProgress();
            Save();
        }

        private void Load()
        {
            try
            {
                if (File.Exists(SavePath))
                {
                    progress = JsonUtility.FromJson<PlayerProgress>(File.ReadAllText(SavePath)) ?? new PlayerProgress();
                    MigrateProgress();
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Save load failed: {exception.Message}");
                progress = new PlayerProgress();
            }

            ProgressChanged?.Invoke();
        }

        private void Save()
        {
            try
            {
                File.WriteAllText(SavePath, JsonUtility.ToJson(progress, true));
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Save write failed: {exception.Message}");
            }

            ProgressChanged?.Invoke();
        }

        private static string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        private void MigrateProgress()
        {
            bool changed = false;
            if (progress.chapterOneStep == 0 && progress.missionStep > 0)
            {
                progress.chapterOneStep = progress.missionStep;
                changed = true;
            }
            if (progress.chapterOneStep >= 9 && !progress.chapterOneComplete)
            {
                progress.chapterOneComplete = true;
                changed = true;
            }
            if (progress.highestUnlockedChapter < 1)
            {
                progress.highestUnlockedChapter = progress.chapterOneComplete ? 2 : 1;
                changed = true;
            }
            if (progress.chapterOneComplete && progress.highestUnlockedChapter < 2)
            {
                progress.highestUnlockedChapter = 2;
                changed = true;
            }
            if (progress.chapterTwoStep >= 9 && !progress.chapterTwoComplete)
            {
                progress.chapterTwoComplete = true;
                changed = true;
            }
            if (progress.chapterTwoComplete && progress.highestUnlockedChapter < 3)
            {
                progress.highestUnlockedChapter = 3;
                changed = true;
            }
            if (progress.chapterThreeStep >= 11 && !progress.chapterThreeComplete)
            {
                progress.chapterThreeComplete = true;
                changed = true;
            }
            if (progress.chapterThreeComplete && progress.highestUnlockedChapter < 4)
            {
                progress.highestUnlockedChapter = 4;
                changed = true;
            }
            if (progress.lastPlayedChapter < 1)
            {
                progress.lastPlayedChapter = progress.chapterOneComplete ? 2 : 1;
                changed = true;
            }
            if (progress.saveVersion < 4)
            {
                progress.saveVersion = 4;
                changed = true;
            }

            if (changed)
            {
                Save();
            }
        }

        private static PlayerProgress GetSavedChapterState()
        {
            try
            {
                string path = GetSavePath();
                if (File.Exists(path))
                {
                    PlayerProgress saved = JsonUtility.FromJson<PlayerProgress>(File.ReadAllText(path));
                    if (saved != null)
                    {
                        if (saved.highestUnlockedChapter < 1)
                        {
                            saved.highestUnlockedChapter = saved.missionStep >= 9 ? 2 : 1;
                        }
                        if (saved.chapterTwoComplete || saved.chapterTwoStep >= 9)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 3);
                        }
                        if (saved.chapterThreeComplete || saved.chapterThreeStep >= 11)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 4);
                        }
                        if (saved.lastPlayedChapter < 1)
                        {
                            saved.lastPlayedChapter = saved.highestUnlockedChapter;
                        }
                        return saved;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Save summary failed: {exception.Message}");
            }

            return new PlayerProgress();
        }
    }
}
