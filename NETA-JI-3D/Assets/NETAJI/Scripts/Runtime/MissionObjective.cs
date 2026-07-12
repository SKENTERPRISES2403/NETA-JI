using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class MissionObjective : MonoBehaviour, IInteractable
    {
        [SerializeField] private string objectiveId;
        [SerializeField] private string prompt = "Madad karein";
        [SerializeField] private string speaker = "Azad";
        [SerializeField, TextArea(2, 5)] private string dialogue;
        [SerializeField] private int trustReward;
        [SerializeField] private int moneyReward;
        [SerializeField] private int reputationReward;
        [SerializeField] private bool hideAfterCompletion;

        private bool completed;

        public string ObjectiveId => objectiveId;
        public string Prompt => prompt;
        public bool CanInteract => !completed && MissionController.Instance != null && MissionController.Instance.IsCurrent(this);

        public void Configure(
            string id,
            string interactionPrompt,
            string dialogueSpeaker,
            string dialogueText,
            int trust,
            int money,
            int reputation,
            bool hideWhenDone)
        {
            objectiveId = id;
            prompt = interactionPrompt;
            speaker = dialogueSpeaker;
            dialogue = dialogueText;
            trustReward = trust;
            moneyReward = money;
            reputationReward = reputation;
            hideAfterCompletion = hideWhenDone;
        }

        public void Interact(AzadController player)
        {
            if (!CanInteract)
            {
                return;
            }

            completed = true;
            PrototypeHud.Instance?.ShowDialogue(speaker, dialogue);
            GameSession.Instance?.ApplyReward(trustReward, moneyReward, reputationReward);
            MissionController.Instance.Complete(this);

            if (hideAfterCompletion)
            {
                foreach (Renderer itemRenderer in GetComponentsInChildren<Renderer>())
                {
                    itemRenderer.enabled = false;
                }

                foreach (Collider itemCollider in GetComponentsInChildren<Collider>())
                {
                    itemCollider.enabled = false;
                }
            }
        }

        public void RestoreAsCompleted()
        {
            completed = true;
            if (!hideAfterCompletion)
            {
                return;
            }

            foreach (Renderer itemRenderer in GetComponentsInChildren<Renderer>())
            {
                itemRenderer.enabled = false;
            }

            foreach (Collider itemCollider in GetComponentsInChildren<Collider>())
            {
                itemCollider.enabled = false;
            }
        }

        public void ResetObjective()
        {
            completed = false;
            foreach (Renderer itemRenderer in GetComponentsInChildren<Renderer>(true))
            {
                itemRenderer.enabled = true;
            }

            foreach (Collider itemCollider in GetComponentsInChildren<Collider>(true))
            {
                itemCollider.enabled = true;
            }
        }
    }
}
