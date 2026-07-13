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
        [SerializeField] private int proofReward;
        [SerializeField] private int powerReward;
        [SerializeField] private int volunteerReward;
        [SerializeField] private int pressureReward;
        [SerializeField] private int supportReward;
        [SerializeField] private int boothReward;
        [SerializeField] private bool resolvesWardElection;
        [SerializeField] private int deliveryReward;
        [SerializeField] private int integrityReward;
        [SerializeField] private int budgetReward;
        [SerializeField] private bool resolvesHundredDayReview;
        [SerializeField] private int assemblyReachReward;
        [SerializeField] private int coalitionUnityReward;
        [SerializeField] private int assemblyReadinessReward;
        [SerializeField] private bool resolvesAssemblyNomination;
        [SerializeField] private int constituencySupportReward;
        [SerializeField] private int campaignComplianceReward;
        [SerializeField] private int electionOperationsReward;
        [SerializeField] private bool resolvesAssemblyElection;
        [SerializeField] private bool hideAfterCompletion;
        [SerializeField] private bool requiresDecision;
        [SerializeField] private string decisionKey;
        [SerializeField] private string decisionTitle;
        [SerializeField, TextArea(2, 4)] private string decisionMessage;
        [SerializeField] private string firstOption;
        [SerializeField] private string secondOption;
        [SerializeField, TextArea(2, 5)] private string secondDialogue;
        [SerializeField] private int secondTrustReward;
        [SerializeField] private int secondMoneyReward;
        [SerializeField] private int secondReputationReward;
        [SerializeField] private int secondProofReward;
        [SerializeField] private int secondPowerReward;
        [SerializeField] private int secondVolunteerReward;
        [SerializeField] private int secondPressureReward;
        [SerializeField] private int secondSupportReward;
        [SerializeField] private int secondBoothReward;
        [SerializeField] private int secondDeliveryReward;
        [SerializeField] private int secondIntegrityReward;
        [SerializeField] private int secondBudgetReward;
        [SerializeField] private int secondAssemblyReachReward;
        [SerializeField] private int secondCoalitionUnityReward;
        [SerializeField] private int secondAssemblyReadinessReward;
        [SerializeField] private int secondConstituencySupportReward;
        [SerializeField] private int secondCampaignComplianceReward;
        [SerializeField] private int secondElectionOperationsReward;

        private bool completed;
        private bool decisionPending;

        public string ObjectiveId => objectiveId;
        public string Prompt => prompt;
        public bool CanInteract => !completed && MissionController.Instance != null && MissionController.Instance.IsCurrent(this);
        public bool RequiresDecision => requiresDecision;

        public void Configure(
            string id,
            string interactionPrompt,
            string dialogueSpeaker,
            string dialogueText,
            int trust,
            int money,
            int reputation,
            bool hideWhenDone,
            int proof = 0)
        {
            objectiveId = id;
            prompt = interactionPrompt;
            speaker = dialogueSpeaker;
            dialogue = dialogueText;
            trustReward = trust;
            moneyReward = money;
            reputationReward = reputation;
            proofReward = proof;
            hideAfterCompletion = hideWhenDone;
        }

        public void ConfigureDecision(
            string key,
            string title,
            string message,
            string safeOption,
            string riskyOption,
            string riskyDialogue,
            int riskyTrust,
            int riskyMoney,
            int riskyReputation,
            int riskyProof = 0,
            int riskyPower = 0,
            int riskyVolunteers = 0,
            int riskyPressure = 0,
            int riskySupport = 0,
            int riskyBooth = 0,
            int riskyDelivery = 0,
            int riskyIntegrity = 0,
            int riskyBudget = 0,
            int riskyAssemblyReach = 0,
            int riskyCoalitionUnity = 0,
            int riskyAssemblyReadiness = 0,
            int riskyConstituencySupport = 0,
            int riskyCampaignCompliance = 0,
            int riskyElectionOperations = 0)
        {
            requiresDecision = true;
            decisionKey = key;
            decisionTitle = title;
            decisionMessage = message;
            firstOption = safeOption;
            secondOption = riskyOption;
            secondDialogue = riskyDialogue;
            secondTrustReward = riskyTrust;
            secondMoneyReward = riskyMoney;
            secondReputationReward = riskyReputation;
            secondProofReward = riskyProof;
            secondPowerReward = riskyPower;
            secondVolunteerReward = riskyVolunteers;
            secondPressureReward = riskyPressure;
            secondSupportReward = riskySupport;
            secondBoothReward = riskyBooth;
            secondDeliveryReward = riskyDelivery;
            secondIntegrityReward = riskyIntegrity;
            secondBudgetReward = riskyBudget;
            secondAssemblyReachReward = riskyAssemblyReach;
            secondCoalitionUnityReward = riskyCoalitionUnity;
            secondAssemblyReadinessReward = riskyAssemblyReadiness;
            secondConstituencySupportReward = riskyConstituencySupport;
            secondCampaignComplianceReward = riskyCampaignCompliance;
            secondElectionOperationsReward = riskyElectionOperations;
        }

        public void ConfigurePoliticalReward(int power, int team, int pressure)
        {
            powerReward = power;
            volunteerReward = team;
            pressureReward = pressure;
        }

        public void ConfigureCampaignReward(int support, int booth)
        {
            supportReward = support;
            boothReward = booth;
        }

        public void ConfigureWardElectionResult()
        {
            resolvesWardElection = true;
        }

        public void ConfigureGovernanceReward(int delivery, int integrity, int budgetLakhs)
        {
            deliveryReward = delivery;
            integrityReward = integrity;
            budgetReward = budgetLakhs;
        }

        public void ConfigureHundredDayReview()
        {
            resolvesHundredDayReview = true;
        }

        public void ConfigureAssemblyReward(int reach, int unity, int readiness)
        {
            assemblyReachReward = reach;
            coalitionUnityReward = unity;
            assemblyReadinessReward = readiness;
        }

        public void ConfigureAssemblyNomination()
        {
            resolvesAssemblyNomination = true;
        }

        public void ConfigureAssemblyCampaignReward(int support, int compliance, int operations)
        {
            constituencySupportReward = support;
            campaignComplianceReward = compliance;
            electionOperationsReward = operations;
        }

        public void ConfigureAssemblyElection()
        {
            resolvesAssemblyElection = true;
        }

        public void Interact(AzadController player)
        {
            if (!CanInteract)
            {
                return;
            }

            if (requiresDecision)
            {
                if (decisionPending)
                {
                    return;
                }
                decisionPending = true;
                PrototypeHud.Instance?.ShowDecision(
                    decisionTitle,
                    decisionMessage,
                    firstOption,
                    secondOption,
                    ResolveDecision);
                return;
            }

            CompleteInteraction(speaker, dialogue, trustReward, moneyReward, reputationReward, proofReward,
                powerReward, volunteerReward, pressureReward, supportReward, boothReward,
                deliveryReward, integrityReward, budgetReward,
                assemblyReachReward, coalitionUnityReward, assemblyReadinessReward,
                constituencySupportReward, campaignComplianceReward, electionOperationsReward);
        }

        public void ResolveDecisionForAutomation(int option)
        {
            if (CanInteract && requiresDecision)
            {
                if (PrototypeHud.Instance != null && PrototypeHud.Instance.IsDecisionOpen)
                {
                    PrototypeHud.Instance.SelectDecisionForAutomation(option);
                }
                else
                {
                    ResolveDecision(Mathf.Clamp(option, 1, 2));
                }
            }
        }

        private void ResolveDecision(int option)
        {
            if (!CanInteract)
            {
                return;
            }

            decisionPending = false;
            GameSession.Instance?.SetStoryDecision(decisionKey, option);
            if (option == 2)
            {
                CompleteInteraction(speaker, secondDialogue, secondTrustReward, secondMoneyReward, secondReputationReward, secondProofReward,
                    secondPowerReward, secondVolunteerReward, secondPressureReward, secondSupportReward, secondBoothReward,
                    secondDeliveryReward, secondIntegrityReward, secondBudgetReward,
                    secondAssemblyReachReward, secondCoalitionUnityReward, secondAssemblyReadinessReward,
                    secondConstituencySupportReward, secondCampaignComplianceReward, secondElectionOperationsReward);
            }
            else
            {
                CompleteInteraction(speaker, dialogue, trustReward, moneyReward, reputationReward, proofReward,
                    powerReward, volunteerReward, pressureReward, supportReward, boothReward,
                    deliveryReward, integrityReward, budgetReward,
                    assemblyReachReward, coalitionUnityReward, assemblyReadinessReward,
                    constituencySupportReward, campaignComplianceReward, electionOperationsReward);
            }
        }

        private void CompleteInteraction(
            string dialogueSpeaker,
            string dialogueText,
            int trust,
            int money,
            int reputation,
            int proof,
            int power,
            int team,
            int pressure,
            int support,
            int booth,
            int delivery,
            int integrity,
            int budget,
            int assemblyReach,
            int coalitionUnity,
            int assemblyReadiness,
            int constituencySupport,
            int campaignCompliance,
            int electionOperations)
        {
            completed = true;
            PrototypeAudio.Instance?.PlayInteraction();
            GameSession.Instance?.ApplyReward(trust, money, reputation, proof);
            GameSession.Instance?.ApplyPoliticalReward(power, team, pressure);
            GameSession.Instance?.ApplyCampaignReward(support, booth);
            GameSession.Instance?.ApplyGovernanceReward(delivery, integrity, budget);
            GameSession.Instance?.ApplyAssemblyReward(assemblyReach, coalitionUnity, assemblyReadiness);
            GameSession.Instance?.ApplyAssemblyCampaignReward(constituencySupport, campaignCompliance, electionOperations);
            if (resolvesWardElection && GameSession.Instance != null)
            {
                bool won = GameSession.Instance.ResolveWardElection();
                int share = GameSession.Instance.Progress.wardVoteShare;
                dialogueText = won
                    ? $"Counting complete: India Helping Party {share}% vote share ke saath ward election jeet gayi."
                    : $"Counting complete: {share}% vote share. Is baar haar hui; organization aur public work jaari rahega.";
            }
            if (resolvesHundredDayReview && GameSession.Instance != null)
            {
                bool passed = GameSession.Instance.ResolveHundredDayReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = passed
                    ? $"100-day public review passed: {progress.governanceScore}/100. Delivery {progress.serviceDelivery}, integrity {progress.fiscalIntegrity}, balance Rs {progress.wardBudgetLakhs} lakh."
                    : $"100-day review: {progress.governanceScore}/100. Audit pass nahi hua; delivery {progress.serviceDelivery}, integrity {progress.fiscalIntegrity}, balance Rs {progress.wardBudgetLakhs} lakh. Corrective plan ab mandatory hai.";
            }
            if (resolvesAssemblyNomination && GameSession.Instance != null)
            {
                bool nominated = GameSession.Instance.ResolveAssemblyNomination();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = nominated
                    ? $"Constituency panel score {progress.nominationScore}/100. Public record, coalition unity aur booth readiness ke basis par Azad ko MLA candidate nominate kiya gaya."
                    : $"Constituency panel score {progress.nominationScore}/100. Nomination hold par hai; 100-day audit pass aur 75+ readiness dono mandatory hain.";
            }
            if (resolvesAssemblyElection && GameSession.Instance != null)
            {
                bool won = GameSession.Instance.ResolveAssemblyElection();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = won
                    ? $"Counting complete: India Helping Party {progress.assemblyVoteShare}% vote share ke saath Vidhansabha seat jeet gayi. Azad ab MLA-elect hai."
                    : $"Counting complete: {progress.assemblyVoteShare}% vote share. Seat nahi mili; public work aur lawful organization jaari rahega.";
            }
            PrototypeHud.Instance?.ShowDialogue(dialogueSpeaker, dialogueText);
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
            decisionPending = false;
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
            decisionPending = false;
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
