using System;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public int saveVersion = 7;
        public int publicTrust = 12;
        public int money = 850;
        public int reputation = 4;
        public int caseProof;
        public int politicalPower;
        public int volunteers;
        public int oppositionPressure;
        public int wardSupport;
        public int boothReadiness;
        public int wardVoteShare;
        public bool wardElectionWon;
        public int missionStep;
        public int chapterOneStep;
        public int chapterTwoStep;
        public int chapterThreeStep;
        public int chapterFourStep;
        public int chapterFiveStep;
        public int chapterSixStep;
        public int chapterSevenStep;
        public bool chapterOneComplete;
        public bool chapterTwoComplete;
        public bool chapterThreeComplete;
        public bool chapterFourComplete;
        public bool chapterFiveComplete;
        public bool chapterSixComplete;
        public bool chapterSevenComplete;
        public int rescueApproach;
        public int hospitalApproach;
        public int oppositionResponse;
        public int campaignStrategy;
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
        public static int LastPlayedChapter => Mathf.Clamp(GetSavedChapterState().lastPlayedChapter, 1, 7);

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

        public void ApplyReward(int trust, int money, int reputation, int proof = 0)
        {
            progress.publicTrust = Mathf.Clamp(progress.publicTrust + trust, 0, 100);
            progress.money = Mathf.Max(0, progress.money + money);
            progress.reputation = Mathf.Clamp(progress.reputation + reputation, 0, 100);
            progress.caseProof = Mathf.Clamp(progress.caseProof + proof, 0, 100);
            Save();
        }

        public void ApplyPoliticalReward(int power, int team, int pressure)
        {
            progress.politicalPower = Mathf.Clamp(progress.politicalPower + power, 0, 100);
            progress.volunteers = Mathf.Max(0, progress.volunteers + team);
            progress.oppositionPressure = Mathf.Clamp(progress.oppositionPressure + pressure, 0, 100);
            Save();
        }

        public void ApplyCampaignReward(int support, int booth)
        {
            progress.wardSupport = Mathf.Clamp(progress.wardSupport + support, 0, 100);
            progress.boothReadiness = Mathf.Clamp(progress.boothReadiness + booth, 0, 100);
            Save();
        }

        public bool ResolveWardElection()
        {
            float voteShare = 30f
                + progress.wardSupport * 0.25f
                + progress.boothReadiness * 0.08f
                + progress.reputation * 0.06f
                + progress.caseProof * 0.03f
                + progress.politicalPower * 0.06f
                - progress.oppositionPressure * 0.08f;
            progress.wardVoteShare = Mathf.Clamp(Mathf.RoundToInt(voteShare), 25, 75);
            progress.wardElectionWon = progress.wardVoteShare >= 50;
            Save();
            return progress.wardElectionWon;
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
                case 5:
                    return progress.chapterFiveStep;
                case 6:
                    return progress.chapterSixStep;
                case 7:
                    return progress.chapterSevenStep;
                default:
                    return progress.chapterOneStep;
            }
        }

        public void SetMissionStep(int chapterNumber, int step)
        {
            int safeStep = Mathf.Max(0, step);
            if (chapterNumber == 7)
            {
                progress.chapterSevenStep = safeStep;
            }
            else if (chapterNumber == 6)
            {
                progress.chapterSixStep = safeStep;
            }
            else if (chapterNumber == 5)
            {
                progress.chapterFiveStep = safeStep;
            }
            else if (chapterNumber == 4)
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
            progress.lastPlayedChapter = Mathf.Clamp(chapterNumber, 1, 7);
            Save();
        }

        public void ResetChapter(int chapterNumber)
        {
            if (chapterNumber == 4)
            {
                progress.rescueApproach = 0;
            }
            else if (chapterNumber == 5)
            {
                progress.hospitalApproach = 0;
            }
            else if (chapterNumber == 6)
            {
                progress.oppositionResponse = 0;
            }
            else if (chapterNumber == 7)
            {
                progress.campaignStrategy = 0;
                progress.wardVoteShare = 0;
                progress.wardElectionWon = false;
            }
            SetMissionStep(chapterNumber, 0);
        }

        public void CompleteChapter(int chapterNumber)
        {
            if (chapterNumber == 7)
            {
                progress.chapterSevenComplete = true;
                progress.lastPlayedChapter = 7;
            }
            else if (chapterNumber == 6)
            {
                progress.chapterSixComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 7);
                progress.lastPlayedChapter = 7;
            }
            else if (chapterNumber == 5)
            {
                progress.chapterFiveComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 6);
                progress.lastPlayedChapter = 6;
            }
            else if (chapterNumber == 4)
            {
                progress.chapterFourComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 5);
                progress.lastPlayedChapter = 5;
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
            else if (key == "hospital-approach")
            {
                progress.hospitalApproach = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "opposition-response")
            {
                progress.oppositionResponse = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "campaign-strategy")
            {
                progress.campaignStrategy = Mathf.Clamp(option, 1, 2);
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
            if (progress.chapterFourStep >= 11 && !progress.chapterFourComplete)
            {
                progress.chapterFourComplete = true;
                changed = true;
            }
            if (progress.chapterFourComplete && progress.highestUnlockedChapter < 5)
            {
                progress.highestUnlockedChapter = 5;
                changed = true;
            }
            if (progress.chapterFiveStep >= 12 && !progress.chapterFiveComplete)
            {
                progress.chapterFiveComplete = true;
                changed = true;
            }
            if (progress.chapterFiveComplete && progress.highestUnlockedChapter < 6)
            {
                progress.highestUnlockedChapter = 6;
                changed = true;
            }
            if (progress.chapterSixStep >= 12 && !progress.chapterSixComplete)
            {
                progress.chapterSixComplete = true;
                changed = true;
            }
            if (progress.chapterSixComplete && progress.highestUnlockedChapter < 7)
            {
                progress.highestUnlockedChapter = 7;
                changed = true;
            }
            if (progress.lastPlayedChapter < 1)
            {
                progress.lastPlayedChapter = progress.chapterOneComplete ? 2 : 1;
                changed = true;
            }
            if (progress.saveVersion < 7)
            {
                progress.saveVersion = 7;
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
                        if (saved.chapterFourComplete || saved.chapterFourStep >= 11)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 5);
                        }
                        if (saved.chapterFiveComplete || saved.chapterFiveStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 6);
                        }
                        if (saved.chapterSixComplete || saved.chapterSixStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 7);
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
