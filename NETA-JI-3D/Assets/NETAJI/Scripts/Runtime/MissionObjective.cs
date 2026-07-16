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
        [SerializeField] private int statePolicyCredibilityReward;
        [SerializeField] private int stateCaucusUnityReward;
        [SerializeField] private int publicLeadershipReward;
        [SerializeField] private bool resolvesStateLeadership;
        [SerializeField] private int statewideSupportReward;
        [SerializeField] private int statewideCampaignComplianceReward;
        [SerializeField] private int statewideElectionOperationsReward;
        [SerializeField] private bool resolvesStateElection;
        [SerializeField] private int chiefMinisterDeliveryReward;
        [SerializeField] private int cabinetIntegrityReward;
        [SerializeField] private int stateFiscalDisciplineReward;
        [SerializeField] private bool resolvesChiefMinisterHundredDayReview;
        [SerializeField] private int stateHealthOutcomeReward;
        [SerializeField] private int stateLearningOutcomeReward;
        [SerializeField] private int stateSafetyOutcomeReward;
        [SerializeField] private int stateLivelihoodOutcomeReward;
        [SerializeField] private bool resolvesStateTermReview;
        [SerializeField] private int nationalOrganizationReachReward;
        [SerializeField] private int federalAllianceTrustReward;
        [SerializeField] private int nationalPolicyCredibilityReward;
        [SerializeField] private bool resolvesNationalExpansion;
        [SerializeField] private int nationalCampaignSupportReward;
        [SerializeField] private int nationalCampaignComplianceReward;
        [SerializeField] private int nationalElectionOperationsReward;
        [SerializeField] private bool resolvesFirstNationalElection;
        [SerializeField] private int oppositionServiceReward;
        [SerializeField] private int nationalAllianceRenewalReward;
        [SerializeField] private int nationalPolicyCorrectionReward;
        [SerializeField] private bool resolvesNationalComeback;
        [SerializeField] private int comebackCampaignSupportReward;
        [SerializeField] private int comebackCampaignComplianceReward;
        [SerializeField] private int comebackElectionOperationsReward;
        [SerializeField] private bool resolvesSecondNationalElection;
        [SerializeField] private int primeMinisterDeliveryReward;
        [SerializeField] private int unionCabinetIntegrityReward;
        [SerializeField] private int nationalFiscalDisciplineReward;
        [SerializeField] private int institutionalTrustReward;
        [SerializeField] private bool resolvesPrimeMinisterHundredDayReview;
        [SerializeField] private int nationalHealthIndexReward;
        [SerializeField] private int nationalLearningIndexReward;
        [SerializeField] private int nationalSafetyJusticeIndexReward;
        [SerializeField] private int nationalLivelihoodIndexReward;
        [SerializeField] private bool resolvesNationalDevelopmentReview;
        [SerializeField] private int globalTradeTrustReward;
        [SerializeField] private int scienceInnovationLeadershipReward;
        [SerializeField] private int peaceDefenseReadinessReward;
        [SerializeField] private int humanitarianClimateLeadershipReward;
        [SerializeField] private bool resolvesGlobalLeadershipReview;
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
        [SerializeField] private int secondStatePolicyCredibilityReward;
        [SerializeField] private int secondStateCaucusUnityReward;
        [SerializeField] private int secondPublicLeadershipReward;
        [SerializeField] private int secondStatewideSupportReward;
        [SerializeField] private int secondStatewideCampaignComplianceReward;
        [SerializeField] private int secondStatewideElectionOperationsReward;
        [SerializeField] private int secondChiefMinisterDeliveryReward;
        [SerializeField] private int secondCabinetIntegrityReward;
        [SerializeField] private int secondStateFiscalDisciplineReward;
        [SerializeField] private int secondStateHealthOutcomeReward;
        [SerializeField] private int secondStateLearningOutcomeReward;
        [SerializeField] private int secondStateSafetyOutcomeReward;
        [SerializeField] private int secondStateLivelihoodOutcomeReward;
        [SerializeField] private int secondNationalOrganizationReachReward;
        [SerializeField] private int secondFederalAllianceTrustReward;
        [SerializeField] private int secondNationalPolicyCredibilityReward;
        [SerializeField] private int secondNationalCampaignSupportReward;
        [SerializeField] private int secondNationalCampaignComplianceReward;
        [SerializeField] private int secondNationalElectionOperationsReward;
        [SerializeField] private int secondOppositionServiceReward;
        [SerializeField] private int secondNationalAllianceRenewalReward;
        [SerializeField] private int secondNationalPolicyCorrectionReward;
        [SerializeField] private int secondComebackCampaignSupportReward;
        [SerializeField] private int secondComebackCampaignComplianceReward;
        [SerializeField] private int secondComebackElectionOperationsReward;
        [SerializeField] private int secondPrimeMinisterDeliveryReward;
        [SerializeField] private int secondUnionCabinetIntegrityReward;
        [SerializeField] private int secondNationalFiscalDisciplineReward;
        [SerializeField] private int secondInstitutionalTrustReward;
        [SerializeField] private int secondNationalHealthIndexReward;
        [SerializeField] private int secondNationalLearningIndexReward;
        [SerializeField] private int secondNationalSafetyJusticeIndexReward;
        [SerializeField] private int secondNationalLivelihoodIndexReward;
        [SerializeField] private int secondGlobalTradeTrustReward;
        [SerializeField] private int secondScienceInnovationLeadershipReward;
        [SerializeField] private int secondPeaceDefenseReadinessReward;
        [SerializeField] private int secondHumanitarianClimateLeadershipReward;

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
            int riskyStateElectionOperations = 0,
            int riskyStatePolicyCredibility = 0,
            int riskyStateCaucusUnity = 0,
            int riskyPublicLeadership = 0,
            int riskyStatewideSupport = 0,
            int riskyStatewideCampaignCompliance = 0,
            int riskyStatewideElectionOperations = 0,
            int riskyChiefMinisterDelivery = 0,
            int riskyCabinetIntegrity = 0,
            int riskyStateFiscalDiscipline = 0,
            int riskyStateHealthOutcome = 0,
            int riskyStateLearningOutcome = 0,
            int riskyStateSafetyOutcome = 0,
            int riskyStateLivelihoodOutcome = 0,
            int riskyNationalOrganizationReach = 0,
            int riskyFederalAllianceTrust = 0,
            int riskyNationalPolicyCredibility = 0,
            int riskyNationalCampaignSupport = 0,
            int riskyNationalCampaignCompliance = 0,
            int riskyNationalElectionOperations = 0,
            int riskyOppositionService = 0,
            int riskyNationalAllianceRenewal = 0,
            int riskyNationalPolicyCorrection = 0,
            int riskySecondNationalCampaignSupport = 0,
            int riskySecondNationalCampaignCompliance = 0,
            int riskySecondNationalElectionOperations = 0)
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
            secondStatePolicyCredibilityReward = riskyStatePolicyCredibility;
            secondStateCaucusUnityReward = riskyStateCaucusUnity;
            secondPublicLeadershipReward = riskyPublicLeadership;
            secondStatewideSupportReward = riskyStatewideSupport;
            secondStatewideCampaignComplianceReward = riskyStatewideCampaignCompliance;
            secondStatewideElectionOperationsReward = riskyStatewideElectionOperations;
            secondChiefMinisterDeliveryReward = riskyChiefMinisterDelivery;
            secondCabinetIntegrityReward = riskyCabinetIntegrity;
            secondStateFiscalDisciplineReward = riskyStateFiscalDiscipline;
            secondStateHealthOutcomeReward = riskyStateHealthOutcome;
            secondStateLearningOutcomeReward = riskyStateLearningOutcome;
            secondStateSafetyOutcomeReward = riskyStateSafetyOutcome;
            secondStateLivelihoodOutcomeReward = riskyStateLivelihoodOutcome;
            secondNationalOrganizationReachReward = riskyNationalOrganizationReach;
            secondFederalAllianceTrustReward = riskyFederalAllianceTrust;
            secondNationalPolicyCredibilityReward = riskyNationalPolicyCredibility;
            secondNationalCampaignSupportReward = riskyNationalCampaignSupport;
            secondNationalCampaignComplianceReward = riskyNationalCampaignCompliance;
            secondNationalElectionOperationsReward = riskyNationalElectionOperations;
            secondOppositionServiceReward = riskyOppositionService;
            secondNationalAllianceRenewalReward = riskyNationalAllianceRenewal;
            secondNationalPolicyCorrectionReward = riskyNationalPolicyCorrection;
            secondComebackCampaignSupportReward = riskySecondNationalCampaignSupport;
            secondComebackCampaignComplianceReward = riskySecondNationalCampaignCompliance;
            secondComebackElectionOperationsReward = riskySecondNationalElectionOperations;
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

        public void ConfigureStateLeadershipReward(int policy, int caucusUnity, int publicLeadershipScore)
        {
            statePolicyCredibilityReward = policy;
            stateCaucusUnityReward = caucusUnity;
            publicLeadershipReward = publicLeadershipScore;
        }

        public void ConfigureStateLeadership()
        {
            resolvesStateLeadership = true;
        }

        public void ConfigureStateElectionReward(int support, int compliance, int operations)
        {
            statewideSupportReward = support;
            statewideCampaignComplianceReward = compliance;
            statewideElectionOperationsReward = operations;
        }

        public void ConfigureStateElection()
        {
            resolvesStateElection = true;
        }

        public void ConfigureChiefMinisterGovernanceReward(int delivery, int integrity, int fiscalDiscipline)
        {
            chiefMinisterDeliveryReward = delivery;
            cabinetIntegrityReward = integrity;
            stateFiscalDisciplineReward = fiscalDiscipline;
        }

        public void ConfigureChiefMinisterHundredDayReview()
        {
            resolvesChiefMinisterHundredDayReview = true;
        }

        public void ConfigureStateTermReward(int health, int learning, int safety, int livelihood)
        {
            stateHealthOutcomeReward = health;
            stateLearningOutcomeReward = learning;
            stateSafetyOutcomeReward = safety;
            stateLivelihoodOutcomeReward = livelihood;
        }

        public void ConfigureStateTermReview()
        {
            resolvesStateTermReview = true;
        }

        public void ConfigureNationalExpansionReward(int reach, int allianceTrust, int policyCredibility)
        {
            nationalOrganizationReachReward = reach;
            federalAllianceTrustReward = allianceTrust;
            nationalPolicyCredibilityReward = policyCredibility;
        }

        public void ConfigureNationalExpansion()
        {
            resolvesNationalExpansion = true;
        }

        public void ConfigureFirstNationalCampaignReward(int support, int compliance, int operations)
        {
            nationalCampaignSupportReward = support;
            nationalCampaignComplianceReward = compliance;
            nationalElectionOperationsReward = operations;
        }

        public void ConfigureFirstNationalElection()
        {
            resolvesFirstNationalElection = true;
        }

        public void ConfigureOppositionTermReward(int service, int allianceRenewal, int policyCorrection)
        {
            oppositionServiceReward = service;
            nationalAllianceRenewalReward = allianceRenewal;
            nationalPolicyCorrectionReward = policyCorrection;
        }

        public void ConfigureNationalComeback()
        {
            resolvesNationalComeback = true;
        }

        public void ConfigureSecondNationalCampaignReward(int support, int compliance, int operations)
        {
            comebackCampaignSupportReward = support;
            comebackCampaignComplianceReward = compliance;
            comebackElectionOperationsReward = operations;
        }

        public void ConfigureSecondNationalElection()
        {
            resolvesSecondNationalElection = true;
        }

        public void ConfigurePrimeMinisterGovernanceReward(int delivery, int cabinetIntegrity, int fiscalDiscipline, int institutions)
        {
            primeMinisterDeliveryReward = delivery;
            unionCabinetIntegrityReward = cabinetIntegrity;
            nationalFiscalDisciplineReward = fiscalDiscipline;
            institutionalTrustReward = institutions;
        }

        public void ConfigurePrimeMinisterDecisionRewards(int delivery, int cabinetIntegrity, int fiscalDiscipline, int institutions)
        {
            secondPrimeMinisterDeliveryReward = delivery;
            secondUnionCabinetIntegrityReward = cabinetIntegrity;
            secondNationalFiscalDisciplineReward = fiscalDiscipline;
            secondInstitutionalTrustReward = institutions;
        }

        public void ConfigurePrimeMinisterHundredDayReview()
        {
            resolvesPrimeMinisterHundredDayReview = true;
        }

        public void ConfigureNationalDevelopmentReward(int health, int learning, int safetyJustice, int livelihood)
        {
            nationalHealthIndexReward = health;
            nationalLearningIndexReward = learning;
            nationalSafetyJusticeIndexReward = safetyJustice;
            nationalLivelihoodIndexReward = livelihood;
        }

        public void ConfigureNationalDevelopmentDecisionRewards(int health, int learning, int safetyJustice, int livelihood)
        {
            secondNationalHealthIndexReward = health;
            secondNationalLearningIndexReward = learning;
            secondNationalSafetyJusticeIndexReward = safetyJustice;
            secondNationalLivelihoodIndexReward = livelihood;
        }

        public void ConfigureNationalDevelopmentReview()
        {
            resolvesNationalDevelopmentReview = true;
        }

        public void ConfigureGlobalLeadershipReward(int trade, int science, int peaceDefense, int humanitarianClimate)
        {
            globalTradeTrustReward = trade;
            scienceInnovationLeadershipReward = science;
            peaceDefenseReadinessReward = peaceDefense;
            humanitarianClimateLeadershipReward = humanitarianClimate;
        }

        public void ConfigureGlobalLeadershipDecisionRewards(int trade, int science, int peaceDefense, int humanitarianClimate)
        {
            secondGlobalTradeTrustReward = trade;
            secondScienceInnovationLeadershipReward = science;
            secondPeaceDefenseReadinessReward = peaceDefense;
            secondHumanitarianClimateLeadershipReward = humanitarianClimate;
        }

        public void ConfigureGlobalLeadershipReview()
        {
            resolvesGlobalLeadershipReview = true;
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
                MissionPresentation.ShowDecision(
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
                stateCampaignReachReward, candidateSlateIntegrityReward, stateElectionOperationsReward,
                statePolicyCredibilityReward, stateCaucusUnityReward, publicLeadershipReward,
                statewideSupportReward, statewideCampaignComplianceReward, statewideElectionOperationsReward,
                chiefMinisterDeliveryReward, cabinetIntegrityReward, stateFiscalDisciplineReward,
                stateHealthOutcomeReward, stateLearningOutcomeReward, stateSafetyOutcomeReward, stateLivelihoodOutcomeReward,
                nationalOrganizationReachReward, federalAllianceTrustReward, nationalPolicyCredibilityReward,
                nationalCampaignSupportReward, nationalCampaignComplianceReward, nationalElectionOperationsReward,
                oppositionServiceReward, nationalAllianceRenewalReward, nationalPolicyCorrectionReward,
                comebackCampaignSupportReward, comebackCampaignComplianceReward, comebackElectionOperationsReward,
                primeMinisterDeliveryReward, unionCabinetIntegrityReward, nationalFiscalDisciplineReward, institutionalTrustReward,
                nationalHealthIndexReward, nationalLearningIndexReward, nationalSafetyJusticeIndexReward, nationalLivelihoodIndexReward,
                globalTradeTrustReward, scienceInnovationLeadershipReward, peaceDefenseReadinessReward, humanitarianClimateLeadershipReward);
        }

        public void ResolveDecisionForAutomation(int option)
        {
            if (CanInteract && requiresDecision)
            {
                if (MissionPresentation.IsDecisionOpen)
                {
                    MissionPresentation.SelectDecisionForAutomation(option);
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
                    secondStateCampaignReachReward, secondCandidateSlateIntegrityReward, secondStateElectionOperationsReward,
                    secondStatePolicyCredibilityReward, secondStateCaucusUnityReward, secondPublicLeadershipReward,
                    secondStatewideSupportReward, secondStatewideCampaignComplianceReward, secondStatewideElectionOperationsReward,
                    secondChiefMinisterDeliveryReward, secondCabinetIntegrityReward, secondStateFiscalDisciplineReward,
                    secondStateHealthOutcomeReward, secondStateLearningOutcomeReward, secondStateSafetyOutcomeReward, secondStateLivelihoodOutcomeReward,
                    secondNationalOrganizationReachReward, secondFederalAllianceTrustReward, secondNationalPolicyCredibilityReward,
                    secondNationalCampaignSupportReward, secondNationalCampaignComplianceReward, secondNationalElectionOperationsReward,
                    secondOppositionServiceReward, secondNationalAllianceRenewalReward, secondNationalPolicyCorrectionReward,
                    secondComebackCampaignSupportReward, secondComebackCampaignComplianceReward, secondComebackElectionOperationsReward,
                    secondPrimeMinisterDeliveryReward, secondUnionCabinetIntegrityReward, secondNationalFiscalDisciplineReward, secondInstitutionalTrustReward,
                    secondNationalHealthIndexReward, secondNationalLearningIndexReward, secondNationalSafetyJusticeIndexReward, secondNationalLivelihoodIndexReward,
                    secondGlobalTradeTrustReward, secondScienceInnovationLeadershipReward, secondPeaceDefenseReadinessReward, secondHumanitarianClimateLeadershipReward);
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
                    stateCampaignReachReward, candidateSlateIntegrityReward, stateElectionOperationsReward,
                    statePolicyCredibilityReward, stateCaucusUnityReward, publicLeadershipReward,
                    statewideSupportReward, statewideCampaignComplianceReward, statewideElectionOperationsReward,
                    chiefMinisterDeliveryReward, cabinetIntegrityReward, stateFiscalDisciplineReward,
                    stateHealthOutcomeReward, stateLearningOutcomeReward, stateSafetyOutcomeReward, stateLivelihoodOutcomeReward,
                    nationalOrganizationReachReward, federalAllianceTrustReward, nationalPolicyCredibilityReward,
                    nationalCampaignSupportReward, nationalCampaignComplianceReward, nationalElectionOperationsReward,
                    oppositionServiceReward, nationalAllianceRenewalReward, nationalPolicyCorrectionReward,
                    comebackCampaignSupportReward, comebackCampaignComplianceReward, comebackElectionOperationsReward,
                    primeMinisterDeliveryReward, unionCabinetIntegrityReward, nationalFiscalDisciplineReward, institutionalTrustReward,
                    nationalHealthIndexReward, nationalLearningIndexReward, nationalSafetyJusticeIndexReward, nationalLivelihoodIndexReward,
                    globalTradeTrustReward, scienceInnovationLeadershipReward, peaceDefenseReadinessReward, humanitarianClimateLeadershipReward);
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
            int stateElectionOperations,
            int statePolicyCredibility,
            int stateCaucusUnity,
            int publicLeadershipScore,
            int statewideSupport,
            int statewideCampaignCompliance,
            int statewideElectionOperations,
            int chiefMinisterDelivery,
            int cabinetIntegrity,
            int stateFiscalDiscipline,
            int stateHealthOutcome,
            int stateLearningOutcome,
            int stateSafetyOutcome,
            int stateLivelihoodOutcome,
            int nationalOrganizationReach,
            int federalAllianceTrust,
            int nationalPolicyCredibility,
            int nationalCampaignSupport,
            int nationalCampaignCompliance,
            int nationalElectionOperations,
            int oppositionService,
            int nationalAllianceRenewal,
            int nationalPolicyCorrection,
            int secondCampaignSupport,
            int secondCampaignCompliance,
            int secondElectionOperations,
            int primeMinisterDelivery,
            int unionCabinetIntegrity,
            int nationalFiscalDiscipline,
            int institutionalTrust,
            int nationalHealth,
            int nationalLearning,
            int nationalSafetyJustice,
            int nationalLivelihood,
            int globalTrade,
            int globalScience,
            int globalPeaceDefense,
            int globalHumanitarianClimate)
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
            GameSession.Instance?.ApplyStateLeadershipReward(statePolicyCredibility, stateCaucusUnity, publicLeadershipScore);
            GameSession.Instance?.ApplyStateElectionReward(statewideSupport, statewideCampaignCompliance, statewideElectionOperations);
            GameSession.Instance?.ApplyChiefMinisterGovernanceReward(chiefMinisterDelivery, cabinetIntegrity, stateFiscalDiscipline);
            GameSession.Instance?.ApplyStateTermReward(stateHealthOutcome, stateLearningOutcome, stateSafetyOutcome, stateLivelihoodOutcome);
            GameSession.Instance?.ApplyNationalExpansionReward(nationalOrganizationReach, federalAllianceTrust, nationalPolicyCredibility);
            GameSession.Instance?.ApplyFirstNationalCampaignReward(
                nationalCampaignSupport, nationalCampaignCompliance, nationalElectionOperations);
            GameSession.Instance?.ApplyOppositionTermReward(
                oppositionService, nationalAllianceRenewal, nationalPolicyCorrection);
            GameSession.Instance?.ApplySecondNationalCampaignReward(
                secondCampaignSupport, secondCampaignCompliance, secondElectionOperations);
            GameSession.Instance?.ApplyPrimeMinisterGovernanceReward(
                primeMinisterDelivery, unionCabinetIntegrity, nationalFiscalDiscipline, institutionalTrust);
            GameSession.Instance?.ApplyNationalDevelopmentReward(
                nationalHealth, nationalLearning, nationalSafetyJustice, nationalLivelihood);
            GameSession.Instance?.ApplyGlobalLeadershipReward(
                globalTrade, globalScience, globalPeaceDefense, globalHumanitarianClimate);
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
            if (resolvesStateLeadership && GameSession.Instance != null)
            {
                bool selected = GameSession.Instance.ResolveStateLeadership();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = selected
                    ? $"State leadership review {progress.stateLeadershipScore}/100. Policy {progress.statePolicyCredibility}, caucus unity {progress.stateCaucusUnity}, public leadership {progress.publicLeadership}. Open record ke basis par Azad state legislative leader selected hai."
                    : $"State leadership review {progress.stateLeadershipScore}/100. Selection hold par hai; policy and caucus unity 60+, state foothold aur 73+ score mandatory hain.";
            }
            if (resolvesStateElection && GameSession.Instance != null)
            {
                bool elected = GameSession.Instance.ResolveStateElection();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = elected
                    ? $"State counting complete: {progress.statewideVoteShare}% vote share aur {progress.stateAssemblySeatsWon}/40 fictional seats. Majority earned; caucus ne elected leader Azad ko CM-designate confirm kiya."
                    : $"State counting complete: {progress.statewideVoteShare}% vote share aur {progress.stateAssemblySeatsWon}/40 seats. CM mandate hold par hai; clean campaign, 50% vote and 21-seat majority mandatory hain.";
            }
            if (resolvesChiefMinisterHundredDayReview && GameSession.Instance != null)
            {
                bool passed = GameSession.Instance.ResolveChiefMinisterHundredDayReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = passed
                    ? $"Independent CM 100-day review {progress.chiefMinisterGovernanceScore}/100. Delivery {progress.chiefMinisterDelivery}, cabinet integrity {progress.cabinetIntegrity}, fiscal discipline {progress.stateFiscalDiscipline}. State reform programme approved for the full term."
                    : $"Independent CM 100-day review {progress.chiefMinisterGovernanceScore}/100. Review hold par hai; delivery, integrity and fiscal discipline 60+ aur overall score 75+ mandatory hain.";
            }
            if (resolvesStateTermReview && GameSession.Instance != null)
            {
                bool passed = GameSession.Instance.ResolveStateTermReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = passed
                    ? $"Independent five-year state review {progress.stateTermScore}/100. Health {progress.stateHealthOutcome}, learning {progress.stateLearningOutcome}, safety {progress.stateSafetyOutcome}, livelihood {progress.stateLivelihoodOutcome}. Full term public audit passed."
                    : $"Independent five-year state review {progress.stateTermScore}/100. Review hold par hai; health, learning, safety and livelihood 60+ aur total score 78+ mandatory hain.";
            }
            if (resolvesNationalExpansion && GameSession.Instance != null)
            {
                bool ready = GameSession.Instance.ResolveNationalExpansion();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = ready
                    ? $"National federation review {progress.nationalReadinessScore}/100. Reach {progress.nationalOrganizationReach}, alliance trust {progress.federalAllianceTrust}, policy {progress.nationalPolicyCredibility}; {progress.nationalRegionsAligned} fictional regional chapters aligned. National convention approved."
                    : $"National federation review {progress.nationalReadinessScore}/100 with {progress.nationalRegionsAligned} aligned chapters. Expansion hold par hai; alliance and policy 60+, score 76+ aur fourteen chapters mandatory hain.";
            }
            if (resolvesFirstNationalElection && GameSession.Instance != null)
            {
                bool won = GameSession.Instance.ResolveFirstNationalElection();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = won
                    ? $"Fictional national count: {progress.firstNationalVoteShare}% vote aur {progress.firstNationalSeatsWon}/100 seats. 51-seat majority earned."
                    : $"Fictional national count: {progress.firstNationalVoteShare}% vote aur {progress.firstNationalSeatsWon}/100 seats. Pehla national mandate nahi mila. Azad result sweekar karta hai; public service aur accountable opposition jaari rahegi.";
            }
            if (resolvesNationalComeback && GameSession.Instance != null)
            {
                bool ready = GameSession.Instance.ResolveNationalComeback();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = ready
                    ? $"Five-year opposition review {progress.nationalComebackScore}/100. Service {progress.oppositionServiceRecord}, alliance renewal {progress.nationalAllianceRenewal}, policy correction {progress.nationalPolicyCorrection}. Second national campaign approved."
                    : $"Five-year opposition review {progress.nationalComebackScore}/100. Comeback hold par hai; alliance and policy 60+ aur score 74+ mandatory hain.";
            }
            if (resolvesSecondNationalElection && GameSession.Instance != null)
            {
                bool elected = GameSession.Instance.ResolveSecondNationalElection();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = elected
                    ? $"Second fictional national count: {progress.secondNationalVoteShare}% vote aur {progress.secondNationalSeatsWon}/100 seats. 51-seat majority earned; elected alliance ne Azad ko Prime Minister-designate chuna."
                    : $"Second fictional national count: {progress.secondNationalVoteShare}% vote aur {progress.secondNationalSeatsWon}/100 seats. PM mandate hold par hai; comeback readiness, clean rules, 50% vote and 51-seat majority mandatory hain.";
            }
            if (resolvesPrimeMinisterHundredDayReview && GameSession.Instance != null)
            {
                bool passed = GameSession.Instance.ResolvePrimeMinisterHundredDayReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = passed
                    ? $"Independent PM 100-day review {progress.primeMinisterHundredDayScore}/100. Delivery {progress.primeMinisterDelivery}, cabinet integrity {progress.unionCabinetIntegrity}, fiscal discipline {progress.nationalFiscalDiscipline}, institutional trust {progress.institutionalTrust}. National reform term approved."
                    : $"Independent PM 100-day review {progress.primeMinisterHundredDayScore}/100. Review hold par hai; delivery, cabinet, fiscal discipline and institutions 65+ aur overall score 75+ mandatory hain.";
            }
            if (resolvesNationalDevelopmentReview && GameSession.Instance != null)
            {
                bool passed = GameSession.Instance.ResolveNationalDevelopmentReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = passed
                    ? $"Independent national development review {progress.nationalDevelopmentScore}/100. Health {progress.nationalHealthIndex}, learning {progress.nationalLearningIndex}, safety and justice {progress.nationalSafetyJusticeIndex}, livelihood {progress.nationalLivelihoodIndex}. Measurable transformation term passed."
                    : $"Independent national development review {progress.nationalDevelopmentScore}/100. Review hold par hai; all four outcomes 68+ aur evidence-led total score 82+ mandatory hain.";
            }
            if (resolvesGlobalLeadershipReview && GameSession.Instance != null)
            {
                bool earned = GameSession.Instance.ResolveGlobalLeadershipReview();
                PlayerProgress progress = GameSession.Instance.Progress;
                dialogueText = earned
                    ? $"Independent global leadership review {progress.globalLeadershipScore}/100. Trade trust {progress.globalTradeTrust}, science {progress.scienceInnovationLeadership}, peace-defense readiness {progress.peaceDefenseReadiness}, humanitarian-climate leadership {progress.humanitarianClimateLeadership}. India ne seva aur saath se Vishwa Guru outcome earn kiya."
                    : $"Independent global leadership review {progress.globalLeadershipScore}/100. Global power visible hai, lekin Vishwa Guru outcome hold par hai; trade, science, defensive peace and humanitarian leadership sab 75+ with score 88+ mandatory hain.";
            }
            MissionPresentation.ShowDialogue(dialogueSpeaker, dialogueText);
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
