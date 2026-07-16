using System;

namespace NetaJi.Prototype
{
    public interface IMissionPresentation
    {
        bool IsDecisionOpen { get; }
        void SetInteractionPrompt(string value);
        void ShowDialogue(string speaker, string text);
        void ShowBanner(string title, string subtitle);
        void RefreshMission();
        void ShowChapterActions(string nextScene);
        void HideChapterActions();
        void ShowDecision(string title, string message, string leftOption, string rightOption, Action<int> callback);
        void SelectDecisionForAutomation(int option);
    }

    public static class MissionPresentation
    {
        private static IMissionPresentation current;

        public static bool IsActive => current != null;
        public static bool IsDecisionOpen => current?.IsDecisionOpen ?? false;

        public static void Register(IMissionPresentation presentation)
        {
            current = presentation;
        }

        public static void Unregister(IMissionPresentation presentation)
        {
            if (ReferenceEquals(current, presentation))
            {
                current = null;
            }
        }

        public static void SetInteractionPrompt(string value) => current?.SetInteractionPrompt(value);
        public static void ShowDialogue(string speaker, string text) => current?.ShowDialogue(speaker, text);
        public static void ShowBanner(string title, string subtitle) => current?.ShowBanner(title, subtitle);
        public static void RefreshMission() => current?.RefreshMission();
        public static void ShowChapterActions(string nextScene) => current?.ShowChapterActions(nextScene);
        public static void HideChapterActions() => current?.HideChapterActions();

        public static void ShowDecision(
            string title,
            string message,
            string leftOption,
            string rightOption,
            Action<int> callback)
        {
            current?.ShowDecision(title, message, leftOption, rightOption, callback);
        }

        public static void SelectDecisionForAutomation(int option)
        {
            current?.SelectDecisionForAutomation(option);
        }
    }
}
