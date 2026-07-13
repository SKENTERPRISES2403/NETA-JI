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
        [SerializeField] private int legislativeEffectivenessReward;
        [SerializeField] private int constituencyServiceReward;
        [SerializeField] private int ethicsReward;
        [SerializeField] private int mlaAllocationReward;
        [SerializeField] private bool resolvesMlaPerformance;
        [SerializeField] private int districtReachReward;
        [SerializeField] private int candidateQualityReward;
        [SerializeField] private int organizationDisciplineReward;
        [SerializeField] private bool resolvesDistrictExpansion;
        [SerializeField] private int stateCampaignReachReward;
        [SerializeField] private int candidateSlateIntegrityReward;
        [SerializeField] private int stateElectionOperationsReward;
        [SerializeField] private bool resolvesStateFoothold;
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
        [SerializeField] private int secondLegislativeEffectivenessReward;
        [SerializeField] private int secondConstituencyServiceReward;
        [SerializeField] private int secondEthicsReward;
        [SerializeField] private int secondMlaAllocationReward;
        [SerializeField] private int secondDistrictReachReward;
        [SerializeField] private int secondCandidateQualityReward;
        [SerializeField] private int secondOrganizationDisciplineReward;
        [SerializeField] private int secondStateCampaignReachReward;
        [SerializeField] private int secondCandidateSlateIntegrityReward;
        [SerializeField] private int secondStateElectionOperationsReward;

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
            int riskyElectionOperations = 0,
            int riskyLegislativeEffectiveness = 0,
            int riskyConstituencyService = 0,
            int riskyEthics = 0,
            int riskyMlaAllocation = 0,
            int riskyDistrictReach = 0,
            int riskyCandidateQuality = 0,
            int riskyOrganizationDiscipline = 0,
            int riskyStateCampaignReach = 0,
            int riskyCandidateSlateIntegrity = 0,
            int riskyStateElectionOperations = 0)
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
            secondLegislativeEffectivenessReward = riskyLegislativeEffectiveness;
            secondConstituencyServiceReward = riskyConstituencyService;
            secondEthicsReward = riskyEthics;
            secondMlaAllocationReward = riskyMlaAllocation;
            secondDistrictReachReward = riskyDistrictReach;
            secondCandidateQualityReward = riskyCandidateQuality;
            secondOrganizationDisciplineReward = riskyOrganizationDiscipline;
            secondStateCampaignReachReward = riskyStateCampaignReach;
            secondCandidateSlateIntegrityReward = riskyCandidateSlateIntegrity;
            secondStateElectionOperationsReward = riskyStateElectionOperations;
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

        public void ConfigureLegislativeReward(int effectiveness, int service, int ethics, int allocationLakhs)
        {
            legislativeEffectivenessReward = effectiveness;
            constituencyServiceReward = service;
            ethicsReward = ethics;
            mlaAllocationReward = allocationLakhs;
        }

        public void ConfigureMlaPerformance()
        {
            resolvesMlaPerformance = true;
        }

        public void ConfigureDistrictReward(int reach, int quality, int discipline)
        {
            districtReachReward = reach;
            candidateQualityReward = quality;
            organizationDisciplineReward = discipline;
        }

        public void ConfigureDistrictExpansion()
        {
            resolvesDistrictExpansion = true;
        }

        public void ConfigureStateExpansionReward(int reach, int slateIntegrity, int operations)
        {
            stateCampaignReachReward = reach;
            candidateSlateIntegrityReward = slateIntegrity;
            stateElectionOperationsReward = operations;
        }

        public void ConfigureStateFoothold()
        {
            resolvesStateFoothold = true;
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
                constituencySupportReward, campaignComplianceReward, electionOperationsReward,
                legislativeEffectivenessReward, constituencyServiceReward, ethicsReward, mlaAllocationReward,
                districtReachReward, candidateQualityReward, organizationDisciplineReward,
                stateCampaignReachReward, candidateSlateIntegrityReward, stateElectionOperationsReward);
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
                    secondConstituencySupportReward, secondCampaignComplianceReward, secondElectionOperationsReward,
                    secondLegislativeEffectivenessReward, secondConstituencyServiceReward, secondEthicsReward, secondMlaAllocationReward,
                    secondDistrictReachReward, secondCandidateQualityReward, secondOrganizationDisciplineReward,
                    secondStateCampaignReachReward, secondCandidateSlateIntegrityReward, secondStateElectionOperationsReward);
            }
            else
            {
                CompleteInteraction(speaker, dialogue, trustReward, moneyReward, reputationReward, proofReward,
                    powerReward, volunteerReward, pressureReward, supportReward, boothReward,
                    deliveryReward, integrityReward, budgetReward,
                    assemblyReachReward, coalitionUnityReward, assemblyReadinessReward,
                    constituencySupportReward, campaignComplianceReward, electionOperationsReward,
                    legislativeEffectivenessReward, constituencyServiceReward, ethicsReward, mlaAllocationReward,
                    districtReachReward, candidateQualityReward, organizationDisciplineReward,
                    stateCampaignReachReward, candidateSlateIntegrityReward, stateElectionOperationsReward);
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
            int electionOperations,
            int legislativeEffectiveness,
            int constituencyService,
            int ethics,
            int mlaAllocation,
            int districtReach,
            int candidateQuality,
            int organizationDiscipline,
            int stateCampaignReach,
            int candidateSlateIntegrity,
            int stateElectionOperations)
        {
            completed = true;
            PrototypeAudio.Instance?.PlayInteraction();
            GameSession.Instance?.ApplyReward(trust, money, reputation, proof);
            GameSession.Instance?.ApplyPoliticalReward(power, team, pressure);
            GameSession.Instance?.ApplyCampaignReward(support, booth);
            GameSession.Instance?.ApplyGovernanceReward(delivery, integrity, budget);
            GameSession.Instance?.ApplyAssemblyReward(assemblyReach, coalitionUnity, assemblyReadiness);
            GameSession.Instance?.ApplyAssemblyCampaignReward(constituencySupport, campaignCompliance, electionOperations);
            GameSession.Instance?.ApplyLegislativeReward(legislativeEffectiveness, constituencyService, ethics, mlaAllocation);
            GameSession.Instance?.ApplyDistrictReward(districtReach, candidateQuality, organizationDiscipline);
            GameSession.Instance?.ApplyStateExpansionReward(stateCampaignReach, candidateSlateIntegrity, stateElectionOperations);
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
            if (resolvesMlaPerformance && GameSession.Instance != null)
            {
                bool onTrack = GameSession.Instance.ResolveMlaPerformance();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = onTrack
                    ? $"Public MLA review {progress.mlaPerformanceScore}/100. Legislative work {progress.legislativeEffectiveness}, constituency service {progress.constituencyService}, ethics {progress.ethicsRecord}, balance Rs {progress.mlaAllocationLakhs} lakh. Term on track hai."
                    : $"Public MLA review {progress.mlaPerformanceScore}/100. Term review hold par hai; ethics 60+, positive fund balance aur 70+ performance mandatory hain.";
            }
            if (resolvesDistrictExpansion && GameSession.Instance != null)
            {
                bool ready = GameSession.Instance.ResolveDistrictExpansion();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = ready
                    ? $"District network review {progress.districtExpansionScore}/100. Reach {progress.districtReach}, candidate quality {progress.candidateQuality}, discipline {progress.organizationDiscipline}. Multi-seat expansion approved."
                    : $"District review {progress.districtExpansionScore}/100. Expansion hold par hai; candidate quality, discipline aur score ke minimum gates complete nahi hue.";
            }
            if (resolvesStateFoothold && GameSession.Instance != null)
            {
                bool secured = GameSession.Instance.ResolveStateFoothold();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = secured
                    ? $"Multi-seat result: {progress.stateSeatsWon}/8 fictional seats jeeti. State expansion score {progress.stateExpansionScore}/100; reach {progress.stateCampaignReach}, slate integrity {progress.candidateSlateIntegrity}, operations {progress.stateElectionOperations}. Pradesh foothold secured."
                    : $"Multi-seat result: {progress.stateSeatsWon}/8 seats, score {progress.stateExpansionScore}/100. State foothold hold par hai; clean slate, operations, 72+ score aur kam se kam five seats mandatory hain.";
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
