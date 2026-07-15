using System;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public int saveVersion = 18;
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
        public int serviceDelivery;
        public int fiscalIntegrity;
        public int wardBudgetLakhs;
        public int governanceScore;
        public bool hundredDayReviewPassed;
        public int assemblyReach;
        public int coalitionUnity;
        public int assemblyReadiness;
        public int nominationScore;
        public bool assemblyCandidateNominated;
        public int constituencySupport;
        public int campaignCompliance;
        public int electionOperations;
        public int assemblyVoteShare;
        public bool assemblyElectionWon;
        public int legislativeEffectiveness;
        public int constituencyService;
        public int ethicsRecord;
        public int mlaAllocationLakhs;
        public int mlaPerformanceScore;
        public bool mlaTermOnTrack;
        public int districtReach;
        public int candidateQuality;
        public int organizationDiscipline;
        public int districtExpansionScore;
        public bool districtNetworkReady;
        public int stateCampaignReach;
        public int candidateSlateIntegrity;
        public int stateElectionOperations;
        public int stateExpansionScore;
        public int stateSeatsWon;
        public bool stateFootholdSecured;
        public int statePolicyCredibility;
        public int stateCaucusUnity;
        public int publicLeadership;
        public int stateLeadershipScore;
        public bool stateLeaderSelected;
        public int statewideSupport;
        public int statewideCampaignCompliance;
        public int statewideElectionOperations;
        public int statewideVoteShare;
        public int stateAssemblySeatsWon;
        public bool chiefMinisterElected;
        public int chiefMinisterDelivery;
        public int cabinetIntegrity;
        public int stateFiscalDiscipline;
        public int chiefMinisterGovernanceScore;
        public bool chiefMinisterHundredDayReviewPassed;
        public int stateHealthOutcome;
        public int stateLearningOutcome;
        public int stateSafetyOutcome;
        public int stateLivelihoodOutcome;
        public int stateTermScore;
        public bool stateTermReviewPassed;
        public int nationalOrganizationReach;
        public int federalAllianceTrust;
        public int nationalPolicyCredibility;
        public int nationalReadinessScore;
        public int nationalRegionsAligned;
        public bool nationalExpansionReady;
        public int missionStep;
        public int chapterOneStep;
        public int chapterTwoStep;
        public int chapterThreeStep;
        public int chapterFourStep;
        public int chapterFiveStep;
        public int chapterSixStep;
        public int chapterSevenStep;
        public int chapterEightStep;
        public int chapterNineStep;
        public int chapterTenStep;
        public int chapterElevenStep;
        public int chapterTwelveStep;
        public int chapterThirteenStep;
        public int chapterFourteenStep;
        public int chapterFifteenStep;
        public int chapterSixteenStep;
        public int chapterSeventeenStep;
        public int chapterEighteenStep;
        public bool chapterOneComplete;
        public bool chapterTwoComplete;
        public bool chapterThreeComplete;
        public bool chapterFourComplete;
        public bool chapterFiveComplete;
        public bool chapterSixComplete;
        public bool chapterSevenComplete;
        public bool chapterEightComplete;
        public bool chapterNineComplete;
        public bool chapterTenComplete;
        public bool chapterElevenComplete;
        public bool chapterTwelveComplete;
        public bool chapterThirteenComplete;
        public bool chapterFourteenComplete;
        public bool chapterFifteenComplete;
        public bool chapterSixteenComplete;
        public bool chapterSeventeenComplete;
        public bool chapterEighteenComplete;
        public int rescueApproach;
        public int hospitalApproach;
        public int oppositionResponse;
        public int campaignStrategy;
        public int governanceApproach;
        public int expansionStrategy;
        public int assemblyCampaignStrategy;
        public int legislativeStrategy;
        public int districtStrategy;
        public int stateExpansionStrategy;
        public int stateLeadershipStrategy;
        public int stateElectionStrategy;
        public int chiefMinisterGovernanceApproach;
        public int stateReformApproach;
        public int nationalExpansionApproach;
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
        public static int LastPlayedChapter => Mathf.Clamp(GetSavedChapterState().lastPlayedChapter, 1, 18);

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

        public void ApplyGovernanceReward(int delivery, int integrity, int budgetLakhs)
        {
            progress.serviceDelivery = Mathf.Clamp(progress.serviceDelivery + delivery, 0, 100);
            progress.fiscalIntegrity = Mathf.Clamp(progress.fiscalIntegrity + integrity, 0, 100);
            progress.wardBudgetLakhs = Mathf.Clamp(progress.wardBudgetLakhs + budgetLakhs, -99, 999);
            Save();
        }

        public bool ResolveHundredDayReview()
        {
            float score = 20f
                + progress.serviceDelivery * 0.45f
                + progress.fiscalIntegrity * 0.35f
                + progress.reputation * 0.05f
                + progress.caseProof * 0.04f
                - progress.oppositionPressure * 0.08f;
            int roundedScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            if (progress.fiscalIntegrity < 60)
            {
                roundedScore = Mathf.Min(roundedScore, 64);
            }
            if (progress.wardBudgetLakhs < 0)
            {
                roundedScore = Mathf.Min(roundedScore, 55);
            }

            progress.governanceScore = roundedScore;
            progress.hundredDayReviewPassed = roundedScore >= 70;
            Save();
            return progress.hundredDayReviewPassed;
        }

        public void ApplyAssemblyReward(int reach, int unity, int readiness)
        {
            progress.assemblyReach = Mathf.Clamp(progress.assemblyReach + reach, 0, 100);
            progress.coalitionUnity = Mathf.Clamp(progress.coalitionUnity + unity, 0, 100);
            progress.assemblyReadiness = Mathf.Clamp(progress.assemblyReadiness + readiness, 0, 100);
            Save();
        }

        public bool ResolveAssemblyNomination()
        {
            float score = 20f
                + progress.assemblyReach * 0.32f
                + progress.coalitionUnity * 0.25f
                + progress.assemblyReadiness * 0.25f
                + progress.governanceScore * 0.10f
                + progress.reputation * 0.05f
                - progress.oppositionPressure * 0.08f;
            progress.nominationScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.assemblyCandidateNominated = progress.nominationScore >= 75
                && progress.hundredDayReviewPassed;
            Save();
            return progress.assemblyCandidateNominated;
        }

        public void ApplyAssemblyCampaignReward(int support, int compliance, int operations)
        {
            progress.constituencySupport = Mathf.Clamp(progress.constituencySupport + support, 0, 100);
            progress.campaignCompliance = Mathf.Clamp(progress.campaignCompliance + compliance, 0, 100);
            progress.electionOperations = Mathf.Clamp(progress.electionOperations + operations, 0, 100);
            Save();
        }

        public bool ResolveAssemblyElection()
        {
            float voteShare = 12f
                + progress.assemblyReach * 0.08f
                + progress.coalitionUnity * 0.07f
                + progress.assemblyReadiness * 0.07f
                + progress.constituencySupport * 0.20f
                + progress.campaignCompliance * 0.10f
                + progress.electionOperations * 0.08f
                + progress.reputation * 0.03f
                + progress.caseProof * 0.02f
                - progress.oppositionPressure * 0.09f;
            progress.assemblyVoteShare = Mathf.Clamp(Mathf.RoundToInt(voteShare), 25, 75);
            progress.assemblyElectionWon = progress.assemblyCandidateNominated
                && progress.assemblyVoteShare >= 50;
            Save();
            return progress.assemblyElectionWon;
        }

        public void ApplyLegislativeReward(int effectiveness, int service, int ethics, int allocationLakhs)
        {
            progress.legislativeEffectiveness = Mathf.Clamp(progress.legislativeEffectiveness + effectiveness, 0, 100);
            progress.constituencyService = Mathf.Clamp(progress.constituencyService + service, 0, 100);
            progress.ethicsRecord = Mathf.Clamp(progress.ethicsRecord + ethics, 0, 100);
            progress.mlaAllocationLakhs = Mathf.Clamp(progress.mlaAllocationLakhs + allocationLakhs, -999, 9999);
            Save();
        }

        public bool ResolveMlaPerformance()
        {
            float score = 10f
                + progress.legislativeEffectiveness * 0.25f
                + progress.constituencyService * 0.25f
                + progress.ethicsRecord * 0.25f
                + progress.governanceScore * 0.10f
                + progress.reputation * 0.04f
                - progress.oppositionPressure * 0.08f;
            int roundedScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            if (progress.mlaAllocationLakhs < 0)
            {
                roundedScore = Mathf.Min(roundedScore, 65);
            }

            progress.mlaPerformanceScore = roundedScore;
            progress.mlaTermOnTrack = progress.assemblyElectionWon
                && progress.ethicsRecord >= 60
                && roundedScore >= 70;
            Save();
            return progress.mlaTermOnTrack;
        }

        public void ApplyDistrictReward(int reach, int quality, int discipline)
        {
            progress.districtReach = Mathf.Clamp(progress.districtReach + reach, 0, 100);
            progress.candidateQuality = Mathf.Clamp(progress.candidateQuality + quality, 0, 100);
            progress.organizationDiscipline = Mathf.Clamp(progress.organizationDiscipline + discipline, 0, 100);
            Save();
        }

        public bool ResolveDistrictExpansion()
        {
            float score = 10f
                + progress.districtReach * 0.28f
                + progress.candidateQuality * 0.30f
                + progress.organizationDiscipline * 0.28f
                + progress.mlaPerformanceScore * 0.10f
                + progress.reputation * 0.04f
                - progress.oppositionPressure * 0.08f;
            progress.districtExpansionScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.districtNetworkReady = progress.mlaTermOnTrack
                && progress.candidateQuality >= 60
                && progress.organizationDiscipline >= 60
                && progress.districtExpansionScore >= 75;
            Save();
            return progress.districtNetworkReady;
        }

        public void ApplyStateExpansionReward(int reach, int slateIntegrity, int operations)
        {
            progress.stateCampaignReach = Mathf.Clamp(progress.stateCampaignReach + reach, 0, 100);
            progress.candidateSlateIntegrity = Mathf.Clamp(progress.candidateSlateIntegrity + slateIntegrity, 0, 100);
            progress.stateElectionOperations = Mathf.Clamp(progress.stateElectionOperations + operations, 0, 100);
            Save();
        }

        public bool ResolveStateFoothold()
        {
            float score = 10f
                + progress.stateCampaignReach * 0.25f
                + progress.candidateSlateIntegrity * 0.25f
                + progress.stateElectionOperations * 0.23f
                + progress.districtExpansionScore * 0.10f
                + progress.reputation * 0.04f
                + progress.caseProof * 0.03f
                - progress.oppositionPressure * 0.09f;
            progress.stateExpansionScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.stateSeatsWon = Mathf.Clamp(
                Mathf.RoundToInt((progress.stateExpansionScore - 42f) * 0.15f), 0, 8);
            progress.stateFootholdSecured = progress.districtNetworkReady
                && progress.candidateSlateIntegrity >= 60
                && progress.stateElectionOperations >= 60
                && progress.stateExpansionScore >= 72
                && progress.stateSeatsWon >= 5;
            Save();
            return progress.stateFootholdSecured;
        }

        public void ApplyStateLeadershipReward(int policy, int caucusUnity, int publicLeadership)
        {
            progress.statePolicyCredibility = Mathf.Clamp(progress.statePolicyCredibility + policy, 0, 100);
            progress.stateCaucusUnity = Mathf.Clamp(progress.stateCaucusUnity + caucusUnity, 0, 100);
            progress.publicLeadership = Mathf.Clamp(progress.publicLeadership + publicLeadership, 0, 100);
            Save();
        }

        public bool ResolveStateLeadership()
        {
            float score = 10f
                + progress.statePolicyCredibility * 0.28f
                + progress.stateCaucusUnity * 0.28f
                + progress.publicLeadership * 0.24f
                + progress.stateExpansionScore * 0.10f
                + progress.reputation * 0.04f
                + progress.caseProof * 0.03f
                - progress.oppositionPressure * 0.08f;
            progress.stateLeadershipScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.stateLeaderSelected = progress.stateFootholdSecured
                && progress.statePolicyCredibility >= 60
                && progress.stateCaucusUnity >= 60
                && progress.stateLeadershipScore >= 73;
            Save();
            return progress.stateLeaderSelected;
        }

        public void ApplyStateElectionReward(int support, int compliance, int operations)
        {
            progress.statewideSupport = Mathf.Clamp(progress.statewideSupport + support, 0, 100);
            progress.statewideCampaignCompliance = Mathf.Clamp(progress.statewideCampaignCompliance + compliance, 0, 100);
            progress.statewideElectionOperations = Mathf.Clamp(progress.statewideElectionOperations + operations, 0, 100);
            Save();
        }

        public bool ResolveStateElection()
        {
            float voteShare = 15f
                + progress.statewideSupport * 0.20f
                + progress.statewideCampaignCompliance * 0.11f
                + progress.statewideElectionOperations * 0.10f
                + progress.stateLeadershipScore * 0.08f
                + progress.reputation * 0.03f
                + progress.caseProof * 0.02f
                - progress.oppositionPressure * 0.08f;
            progress.statewideVoteShare = Mathf.Clamp(Mathf.RoundToInt(voteShare), 25, 75);
            progress.stateAssemblySeatsWon = Mathf.Clamp(
                Mathf.RoundToInt((progress.statewideVoteShare - 28f) * 0.95f), 0, 40);
            progress.chiefMinisterElected = progress.stateLeaderSelected
                && progress.statewideCampaignCompliance >= 60
                && progress.statewideElectionOperations >= 60
                && progress.statewideVoteShare >= 50
                && progress.stateAssemblySeatsWon >= 21;
            Save();
            return progress.chiefMinisterElected;
        }

        public void ApplyChiefMinisterGovernanceReward(int delivery, int integrity, int fiscalDiscipline)
        {
            progress.chiefMinisterDelivery = Mathf.Clamp(progress.chiefMinisterDelivery + delivery, 0, 100);
            progress.cabinetIntegrity = Mathf.Clamp(progress.cabinetIntegrity + integrity, 0, 100);
            progress.stateFiscalDiscipline = Mathf.Clamp(progress.stateFiscalDiscipline + fiscalDiscipline, 0, 100);
            Save();
        }

        public bool ResolveChiefMinisterHundredDayReview()
        {
            float score = progress.chiefMinisterDelivery * 0.34f
                + progress.cabinetIntegrity * 0.32f
                + progress.stateFiscalDiscipline * 0.20f
                + progress.stateLeadershipScore * 0.08f
                + progress.reputation * 0.03f
                + progress.caseProof * 0.02f
                - progress.oppositionPressure * 0.04f;
            progress.chiefMinisterGovernanceScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.chiefMinisterHundredDayReviewPassed = progress.chiefMinisterElected
                && progress.chiefMinisterDelivery >= 60
                && progress.cabinetIntegrity >= 60
                && progress.stateFiscalDiscipline >= 60
                && progress.chiefMinisterGovernanceScore >= 75;
            Save();
            return progress.chiefMinisterHundredDayReviewPassed;
        }

        public void ApplyStateTermReward(int health, int learning, int safety, int livelihood)
        {
            progress.stateHealthOutcome = Mathf.Clamp(progress.stateHealthOutcome + health, 0, 100);
            progress.stateLearningOutcome = Mathf.Clamp(progress.stateLearningOutcome + learning, 0, 100);
            progress.stateSafetyOutcome = Mathf.Clamp(progress.stateSafetyOutcome + safety, 0, 100);
            progress.stateLivelihoodOutcome = Mathf.Clamp(progress.stateLivelihoodOutcome + livelihood, 0, 100);
            Save();
        }

        public bool ResolveStateTermReview()
        {
            float score = progress.stateHealthOutcome * 0.20f
                + progress.stateLearningOutcome * 0.20f
                + progress.stateSafetyOutcome * 0.20f
                + progress.stateLivelihoodOutcome * 0.20f
                + progress.chiefMinisterGovernanceScore * 0.10f
                + progress.cabinetIntegrity * 0.05f
                + progress.reputation * 0.03f
                + progress.caseProof * 0.02f
                - progress.oppositionPressure * 0.04f;
            progress.stateTermScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.stateTermReviewPassed = progress.chiefMinisterHundredDayReviewPassed
                && progress.stateHealthOutcome >= 60
                && progress.stateLearningOutcome >= 60
                && progress.stateSafetyOutcome >= 60
                && progress.stateLivelihoodOutcome >= 60
                && progress.stateTermScore >= 78;
            Save();
            return progress.stateTermReviewPassed;
        }

        public void ApplyNationalExpansionReward(int reach, int allianceTrust, int policyCredibility)
        {
            progress.nationalOrganizationReach = Mathf.Clamp(progress.nationalOrganizationReach + reach, 0, 100);
            progress.federalAllianceTrust = Mathf.Clamp(progress.federalAllianceTrust + allianceTrust, 0, 100);
            progress.nationalPolicyCredibility = Mathf.Clamp(progress.nationalPolicyCredibility + policyCredibility, 0, 100);
            Save();
        }

        public bool ResolveNationalExpansion()
        {
            float score = progress.nationalOrganizationReach * 0.30f
                + progress.federalAllianceTrust * 0.28f
                + progress.nationalPolicyCredibility * 0.26f
                + progress.stateTermScore * 0.08f
                + progress.reputation * 0.03f
                + progress.caseProof * 0.02f
                - progress.oppositionPressure * 0.04f;
            progress.nationalReadinessScore = Mathf.Clamp(Mathf.RoundToInt(score), 0, 100);
            progress.nationalRegionsAligned = Mathf.Clamp(
                Mathf.RoundToInt((progress.nationalReadinessScore - 30f) * 0.30f), 0, 24);
            progress.nationalExpansionReady = progress.stateTermReviewPassed
                && progress.federalAllianceTrust >= 60
                && progress.nationalPolicyCredibility >= 60
                && progress.nationalReadinessScore >= 76
                && progress.nationalRegionsAligned >= 14;
            Save();
            return progress.nationalExpansionReady;
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
                case 8:
                    return progress.chapterEightStep;
                case 9:
                    return progress.chapterNineStep;
                case 10:
                    return progress.chapterTenStep;
                case 11:
                    return progress.chapterElevenStep;
                case 12:
                    return progress.chapterTwelveStep;
                case 13:
                    return progress.chapterThirteenStep;
                case 14:
                    return progress.chapterFourteenStep;
                case 15:
                    return progress.chapterFifteenStep;
                case 16:
                    return progress.chapterSixteenStep;
                case 17:
                    return progress.chapterSeventeenStep;
                case 18:
                    return progress.chapterEighteenStep;
                default:
                    return progress.chapterOneStep;
            }
        }

        public void SetMissionStep(int chapterNumber, int step)
        {
            int safeStep = Mathf.Max(0, step);
            if (chapterNumber == 18)
            {
                progress.chapterEighteenStep = safeStep;
            }
            else if (chapterNumber == 17)
            {
                progress.chapterSeventeenStep = safeStep;
            }
            else if (chapterNumber == 16)
            {
                progress.chapterSixteenStep = safeStep;
            }
            else if (chapterNumber == 15)
            {
                progress.chapterFifteenStep = safeStep;
            }
            else if (chapterNumber == 14)
            {
                progress.chapterFourteenStep = safeStep;
            }
            else if (chapterNumber == 13)
            {
                progress.chapterThirteenStep = safeStep;
            }
            else if (chapterNumber == 12)
            {
                progress.chapterTwelveStep = safeStep;
            }
            else if (chapterNumber == 11)
            {
                progress.chapterElevenStep = safeStep;
            }
            else if (chapterNumber == 10)
            {
                progress.chapterTenStep = safeStep;
            }
            else if (chapterNumber == 9)
            {
                progress.chapterNineStep = safeStep;
            }
            else if (chapterNumber == 8)
            {
                progress.chapterEightStep = safeStep;
            }
            else if (chapterNumber == 7)
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
            progress.lastPlayedChapter = Mathf.Clamp(chapterNumber, 1, 18);
            Save();
        }

        public void ResetChapter(int chapterNumber)
        {
            if (chapterNumber == 18)
            {
                progress.nationalExpansionApproach = 0;
                progress.nationalOrganizationReach = 0;
                progress.federalAllianceTrust = 0;
                progress.nationalPolicyCredibility = 0;
                progress.nationalReadinessScore = 0;
                progress.nationalRegionsAligned = 0;
                progress.nationalExpansionReady = false;
            }
            else if (chapterNumber == 17)
            {
                progress.stateReformApproach = 0;
                progress.stateHealthOutcome = 0;
                progress.stateLearningOutcome = 0;
                progress.stateSafetyOutcome = 0;
                progress.stateLivelihoodOutcome = 0;
                progress.stateTermScore = 0;
                progress.stateTermReviewPassed = false;
            }
            else if (chapterNumber == 16)
            {
                progress.chiefMinisterGovernanceApproach = 0;
                progress.chiefMinisterDelivery = 0;
                progress.cabinetIntegrity = 0;
                progress.stateFiscalDiscipline = 0;
                progress.chiefMinisterGovernanceScore = 0;
                progress.chiefMinisterHundredDayReviewPassed = false;
            }
            else if (chapterNumber == 15)
            {
                progress.stateElectionStrategy = 0;
                progress.statewideSupport = 0;
                progress.statewideCampaignCompliance = 0;
                progress.statewideElectionOperations = 0;
                progress.statewideVoteShare = 0;
                progress.stateAssemblySeatsWon = 0;
                progress.chiefMinisterElected = false;
            }
            else if (chapterNumber == 14)
            {
                progress.stateLeadershipStrategy = 0;
                progress.statePolicyCredibility = 0;
                progress.stateCaucusUnity = 0;
                progress.publicLeadership = 0;
                progress.stateLeadershipScore = 0;
                progress.stateLeaderSelected = false;
            }
            else if (chapterNumber == 13)
            {
                progress.stateExpansionStrategy = 0;
                progress.stateCampaignReach = 0;
                progress.candidateSlateIntegrity = 0;
                progress.stateElectionOperations = 0;
                progress.stateExpansionScore = 0;
                progress.stateSeatsWon = 0;
                progress.stateFootholdSecured = false;
            }
            else if (chapterNumber == 12)
            {
                progress.districtStrategy = 0;
                progress.districtReach = 0;
                progress.candidateQuality = 0;
                progress.organizationDiscipline = 0;
                progress.districtExpansionScore = 0;
                progress.districtNetworkReady = false;
            }
            else if (chapterNumber == 11)
            {
                progress.legislativeStrategy = 0;
                progress.legislativeEffectiveness = 0;
                progress.constituencyService = 0;
                progress.ethicsRecord = 0;
                progress.mlaAllocationLakhs = 0;
                progress.mlaPerformanceScore = 0;
                progress.mlaTermOnTrack = false;
            }
            else if (chapterNumber == 10)
            {
                progress.assemblyCampaignStrategy = 0;
                progress.constituencySupport = 0;
                progress.campaignCompliance = 0;
                progress.electionOperations = 0;
                progress.assemblyVoteShare = 0;
                progress.assemblyElectionWon = false;
            }
            else if (chapterNumber == 9)
            {
                progress.expansionStrategy = 0;
                progress.assemblyReach = 0;
                progress.coalitionUnity = 0;
                progress.assemblyReadiness = 0;
                progress.nominationScore = 0;
                progress.assemblyCandidateNominated = false;
            }
            else if (chapterNumber == 8)
            {
                progress.governanceApproach = 0;
                progress.serviceDelivery = 0;
                progress.fiscalIntegrity = 0;
                progress.wardBudgetLakhs = 0;
                progress.governanceScore = 0;
                progress.hundredDayReviewPassed = false;
            }
            else if (chapterNumber == 4)
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
            if (chapterNumber == 18)
            {
                progress.chapterEighteenComplete = true;
                progress.lastPlayedChapter = 18;
            }
            else if (chapterNumber == 17)
            {
                progress.chapterSeventeenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 18);
                progress.lastPlayedChapter = 18;
            }
            else if (chapterNumber == 16)
            {
                progress.chapterSixteenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 17);
                progress.lastPlayedChapter = 17;
            }
            else if (chapterNumber == 15)
            {
                progress.chapterFifteenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 16);
                progress.lastPlayedChapter = 16;
            }
            else if (chapterNumber == 14)
            {
                progress.chapterFourteenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 15);
                progress.lastPlayedChapter = 15;
            }
            else if (chapterNumber == 13)
            {
                progress.chapterThirteenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 14);
                progress.lastPlayedChapter = 14;
            }
            else if (chapterNumber == 12)
            {
                progress.chapterTwelveComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 13);
                progress.lastPlayedChapter = 13;
            }
            else if (chapterNumber == 11)
            {
                progress.chapterElevenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 12);
                progress.lastPlayedChapter = 12;
            }
            else if (chapterNumber == 10)
            {
                progress.chapterTenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 11);
                progress.lastPlayedChapter = 11;
            }
            else if (chapterNumber == 9)
            {
                progress.chapterNineComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 10);
                progress.lastPlayedChapter = 10;
            }
            else if (chapterNumber == 8)
            {
                progress.chapterEightComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 9);
                progress.lastPlayedChapter = 9;
            }
            else if (chapterNumber == 7)
            {
                progress.chapterSevenComplete = true;
                progress.highestUnlockedChapter = Mathf.Max(progress.highestUnlockedChapter, 8);
                progress.lastPlayedChapter = 8;
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
            else if (key == "governance-approach")
            {
                progress.governanceApproach = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "expansion-strategy")
            {
                progress.expansionStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "assembly-campaign-strategy")
            {
                progress.assemblyCampaignStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "legislative-strategy")
            {
                progress.legislativeStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "district-strategy")
            {
                progress.districtStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "state-expansion-strategy")
            {
                progress.stateExpansionStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "state-leadership-strategy")
            {
                progress.stateLeadershipStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "state-election-strategy")
            {
                progress.stateElectionStrategy = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "cm-governance-approach")
            {
                progress.chiefMinisterGovernanceApproach = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "state-reform-approach")
            {
                progress.stateReformApproach = Mathf.Clamp(option, 1, 2);
                Save();
            }
            else if (key == "national-expansion-approach")
            {
                progress.nationalExpansionApproach = Mathf.Clamp(option, 1, 2);
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
            if (progress.chapterSevenStep >= 12 && !progress.chapterSevenComplete)
            {
                progress.chapterSevenComplete = true;
                changed = true;
            }
            if (progress.chapterSevenComplete && progress.highestUnlockedChapter < 8)
            {
                progress.highestUnlockedChapter = 8;
                changed = true;
            }
            if (progress.chapterEightStep >= 12 && !progress.chapterEightComplete)
            {
                progress.chapterEightComplete = true;
                changed = true;
            }
            if (progress.chapterEightComplete && progress.highestUnlockedChapter < 9)
            {
                progress.highestUnlockedChapter = 9;
                changed = true;
            }
            if (progress.chapterNineStep >= 12 && !progress.chapterNineComplete)
            {
                progress.chapterNineComplete = true;
                changed = true;
            }
            if (progress.lastPlayedChapter < 1)
            {
                progress.lastPlayedChapter = progress.chapterOneComplete ? 2 : 1;
                changed = true;
            }
            if (progress.chapterNineComplete && progress.highestUnlockedChapter < 10)
            {
                progress.highestUnlockedChapter = 10;
                changed = true;
            }
            if (progress.chapterTenStep >= 12 && !progress.chapterTenComplete)
            {
                progress.chapterTenComplete = true;
                changed = true;
            }
            if (progress.chapterTenComplete && progress.highestUnlockedChapter < 11)
            {
                progress.highestUnlockedChapter = 11;
                changed = true;
            }
            if (progress.chapterElevenStep >= 12 && !progress.chapterElevenComplete)
            {
                progress.chapterElevenComplete = true;
                changed = true;
            }
            if (progress.chapterElevenComplete && progress.highestUnlockedChapter < 12)
            {
                progress.highestUnlockedChapter = 12;
                changed = true;
            }
            if (progress.chapterTwelveStep >= 12 && !progress.chapterTwelveComplete)
            {
                progress.chapterTwelveComplete = true;
                changed = true;
            }
            if (progress.chapterTwelveComplete && progress.highestUnlockedChapter < 13)
            {
                progress.highestUnlockedChapter = 13;
                changed = true;
            }
            if (progress.chapterThirteenStep >= 12 && !progress.chapterThirteenComplete)
            {
                progress.chapterThirteenComplete = true;
                changed = true;
            }
            if (progress.chapterThirteenComplete && progress.highestUnlockedChapter < 14)
            {
                progress.highestUnlockedChapter = 14;
                changed = true;
            }
            if (progress.chapterFourteenStep >= 12 && !progress.chapterFourteenComplete)
            {
                progress.chapterFourteenComplete = true;
                changed = true;
            }
            if (progress.chapterFourteenComplete && progress.highestUnlockedChapter < 15)
            {
                progress.highestUnlockedChapter = 15;
                changed = true;
            }
            if (progress.chapterFifteenStep >= 12 && !progress.chapterFifteenComplete)
            {
                progress.chapterFifteenComplete = true;
                changed = true;
            }
            if (progress.chapterFifteenComplete && progress.highestUnlockedChapter < 16)
            {
                progress.highestUnlockedChapter = 16;
                changed = true;
            }
            if (progress.chapterSixteenStep >= 12 && !progress.chapterSixteenComplete)
            {
                progress.chapterSixteenComplete = true;
                changed = true;
            }
            if (progress.chapterSixteenComplete && progress.highestUnlockedChapter < 17)
            {
                progress.highestUnlockedChapter = 17;
                changed = true;
            }
            if (progress.chapterSeventeenStep >= 12 && !progress.chapterSeventeenComplete)
            {
                progress.chapterSeventeenComplete = true;
                changed = true;
            }
            if (progress.chapterSeventeenComplete && progress.highestUnlockedChapter < 18)
            {
                progress.highestUnlockedChapter = 18;
                changed = true;
            }
            if (progress.chapterEighteenStep >= 12 && !progress.chapterEighteenComplete)
            {
                progress.chapterEighteenComplete = true;
                changed = true;
            }
            if (progress.saveVersion < 18)
            {
                progress.saveVersion = 18;
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
                        if (saved.chapterSevenComplete || saved.chapterSevenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 8);
                        }
                        if (saved.chapterEightComplete || saved.chapterEightStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 9);
                        }
                        if (saved.chapterNineComplete || saved.chapterNineStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 10);
                        }
                        if (saved.chapterTenComplete || saved.chapterTenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 11);
                        }
                        if (saved.chapterElevenComplete || saved.chapterElevenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 12);
                        }
                        if (saved.chapterTwelveComplete || saved.chapterTwelveStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 13);
                        }
                        if (saved.chapterThirteenComplete || saved.chapterThirteenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 14);
                        }
                        if (saved.chapterFourteenComplete || saved.chapterFourteenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 15);
                        }
                        if (saved.chapterFifteenComplete || saved.chapterFifteenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 16);
                        }
                        if (saved.chapterSixteenComplete || saved.chapterSixteenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 17);
                        }
                        if (saved.chapterSeventeenComplete || saved.chapterSeventeenStep >= 12)
                        {
                            saved.highestUnlockedChapter = Mathf.Max(saved.highestUnlockedChapter, 18);
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
