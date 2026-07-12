using System;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public int publicTrust = 12;
        public int money = 850;
        public int reputation = 4;
        public int missionStep;
    }

    public sealed class GameSession : MonoBehaviour
    {
        public static GameSession Instance { get; private set; }
        public event Action ProgressChanged;

        [SerializeField] private PlayerProgress progress = new PlayerProgress();
        private string SavePath => Path.Combine(Application.persistentDataPath, "neta-ji-prototype-save.json");

        public PlayerProgress Progress => progress;

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

        public void SetMissionStep(int step)
        {
            progress.missionStep = Mathf.Max(0, step);
            Save();
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
    }
}

