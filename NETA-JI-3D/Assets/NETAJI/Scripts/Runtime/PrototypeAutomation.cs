using System.Collections;
using System.IO;
using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeAutomation : MonoBehaviour
    {
        [SerializeField] private int chapterNumber = 1;
        [SerializeField] private int expectedTrust = 35;
        [SerializeField] private int expectedMoney = 950;
        [SerializeField] private int expectedReputation = 16;
        [SerializeField] private int expectedProof;
        [SerializeField] private int expectedPower;
        [SerializeField] private int expectedVolunteers;
        [SerializeField] private int expectedPressure;
        [SerializeField] private int expectedSupport;
        [SerializeField] private int expectedBooth;
        [SerializeField] private int expectedVoteShare;
        [SerializeField] private bool expectedWardWin;
        [SerializeField] private int expectedDelivery;
        [SerializeField] private int expectedIntegrity;
        [SerializeField] private int expectedBudget;
        [SerializeField] private int expectedGovernanceScore;
        [SerializeField] private bool expectedReviewPassed;
        [SerializeField] private int expectedAssemblyReach;
        [SerializeField] private int expectedCoalitionUnity;
        [SerializeField] private int expectedAssemblyReadiness;
        [SerializeField] private int expectedNominationScore;
        [SerializeField] private bool expectedAssemblyNomination;
        [SerializeField] private int expectedConstituencySupport;
        [SerializeField] private int expectedCampaignCompliance;
        [SerializeField] private int expectedElectionOperations;
        [SerializeField] private int expectedAssemblyVoteShare;
        [SerializeField] private bool expectedAssemblyWin;
        [SerializeField] private int expectedLegislativeEffectiveness;
        [SerializeField] private int expectedConstituencyService;
        [SerializeField] private int expectedEthics;
        [SerializeField] private int expectedMlaAllocation;
        [SerializeField] private int expectedMlaPerformance;
        [SerializeField] private bool expectedMlaTermOnTrack;

        public void Configure(
            int chapter,
            int trust,
            int money,
            int reputation,
            int proof = 0,
            int power = 0,
            int team = 0,
            int pressure = 0,
            int support = 0,
            int booth = 0,
            int voteShare = 0,
            bool wardWin = false,
            int delivery = 0,
            int integrity = 0,
            int budget = 0,
            int governanceScore = 0,
            bool reviewPassed = false,
            int assemblyReach = 0,
            int coalitionUnity = 0,
            int assemblyReadiness = 0,
            int nominationScore = 0,
            bool assemblyNomination = false,
            int constituencySupport = 0,
            int campaignCompliance = 0,
            int electionOperations = 0,
            int assemblyVoteShare = 0,
            bool assemblyWin = false,
            int legislativeEffectiveness = 0,
            int constituencyService = 0,
            int ethics = 0,
            int mlaAllocation = 0,
            int mlaPerformance = 0,
            bool mlaTermOnTrack = false)
        {
            chapterNumber = Mathf.Max(1, chapter);
            expectedTrust = trust;
            expectedMoney = money;
            expectedReputation = reputation;
            expectedProof = proof;
            expectedPower = power;
            expectedVolunteers = team;
            expectedPressure = pressure;
            expectedSupport = support;
            expectedBooth = booth;
            expectedVoteShare = voteShare;
            expectedWardWin = wardWin;
            expectedDelivery = delivery;
            expectedIntegrity = integrity;
            expectedBudget = budget;
            expectedGovernanceScore = governanceScore;
            expectedReviewPassed = reviewPassed;
            expectedAssemblyReach = assemblyReach;
            expectedCoalitionUnity = coalitionUnity;
            expectedAssemblyReadiness = assemblyReadiness;
            expectedNominationScore = nominationScore;
            expectedAssemblyNomination = assemblyNomination;
            expectedConstituencySupport = constituencySupport;
            expectedCampaignCompliance = campaignCompliance;
            expectedElectionOperations = electionOperations;
            expectedAssemblyVoteShare = assemblyVoteShare;
            expectedAssemblyWin = assemblyWin;
            expectedLegislativeEffectiveness = legislativeEffectiveness;
            expectedConstituencyService = constituencyService;
            expectedEthics = ethics;
            expectedMlaAllocation = mlaAllocation;
            expectedMlaPerformance = mlaPerformance;
            expectedMlaTermOnTrack = mlaTermOnTrack;
        }

        private void Start()
        {
            string[] arguments = System.Environment.GetCommandLineArgs();
            string smokeArgument = chapterNumber == 1 ? "-prototypeSmoke" : $"-chapter{chapterNumber}Smoke";
            if (System.Array.IndexOf(arguments, smokeArgument) >= 0
                || (chapterNumber == 4 && System.Array.IndexOf(arguments, "-riskyDecisionSmoke") >= 0)
                || (chapterNumber == 5 && System.Array.IndexOf(arguments, "-riskyHospitalSmoke") >= 0)
                || (chapterNumber == 6 && System.Array.IndexOf(arguments, "-riskyOppositionSmoke") >= 0)
                || (chapterNumber == 7 && System.Array.IndexOf(arguments, "-riskyCampaignSmoke") >= 0)
                || (chapterNumber == 8 && System.Array.IndexOf(arguments, "-riskyGovernanceSmoke") >= 0)
                || (chapterNumber == 9 && System.Array.IndexOf(arguments, "-riskyExpansionSmoke") >= 0)
                || (chapterNumber == 10 && System.Array.IndexOf(arguments, "-riskyAssemblyCampaignSmoke") >= 0)
                || (chapterNumber == 11 && System.Array.IndexOf(arguments, "-riskyLegislativeSmoke") >= 0))
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            bool riskyDecision = (chapterNumber == 4
                    && System.Array.IndexOf(arguments, "-riskyDecisionSmoke") >= 0)
                || (chapterNumber == 5
                    && System.Array.IndexOf(arguments, "-riskyHospitalSmoke") >= 0)
                || (chapterNumber == 6
                    && System.Array.IndexOf(arguments, "-riskyOppositionSmoke") >= 0)
                || (chapterNumber == 7
                    && System.Array.IndexOf(arguments, "-riskyCampaignSmoke") >= 0)
                || (chapterNumber == 8
                    && System.Array.IndexOf(arguments, "-riskyGovernanceSmoke") >= 0)
                || (chapterNumber == 9
                    && System.Array.IndexOf(arguments, "-riskyExpansionSmoke") >= 0)
                || (chapterNumber == 10
                    && System.Array.IndexOf(arguments, "-riskyAssemblyCampaignSmoke") >= 0)
                || (chapterNumber == 11
                    && System.Array.IndexOf(arguments, "-riskyLegislativeSmoke") >= 0);

            yield return new WaitForSeconds(1.2f);
            MissionController mission = MissionController.Instance;
            AzadController player = FindFirstObjectByType<AzadController>();
            if (mission == null || player == null)
            {
                Debug.LogError("PROTOTYPE_SMOKE_FAILED: required runtime systems were not found.");
                Application.Quit(2);
                yield break;
            }

            if (chapterNumber == 1)
            {
                mission.ResetMission(true);
            }
            else if (chapterNumber == 2)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(23, 100, 12);
                GameSession.Instance.CompleteChapter(1);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 3)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(46, 200, 26);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 4)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(55, 50, 36);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 5)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(71, -200, 52);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 6)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(85, -700, 67, 17);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 7)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(88, -400, 80, 31);
                GameSession.Instance.ApplyPoliticalReward(18, 35, 8);
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 8)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(88, -500, 86, 42);
                GameSession.Instance.ApplyPoliticalReward(28, 55, 17);
                GameSession.Instance.ApplyCampaignReward(62, 85);
                GameSession.Instance.ResolveWardElection();
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                GameSession.Instance.CompleteChapter(7);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 9)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(88, -500, 86, 42);
                GameSession.Instance.ApplyPoliticalReward(28, 55, 17);
                GameSession.Instance.ApplyCampaignReward(62, 85);
                GameSession.Instance.ResolveWardElection();
                GameSession.Instance.ApplyReward(0, 0, 10, 14);
                GameSession.Instance.ApplyPoliticalReward(5, 9, 14);
                GameSession.Instance.ApplyCampaignReward(5, 0);
                GameSession.Instance.ApplyGovernanceReward(65, 75, 2);
                GameSession.Instance.ResolveHundredDayReview();
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                GameSession.Instance.CompleteChapter(7);
                GameSession.Instance.CompleteChapter(8);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 10)
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(88, -500, 86, 42);
                GameSession.Instance.ApplyPoliticalReward(28, 55, 17);
                GameSession.Instance.ApplyCampaignReward(62, 85);
                GameSession.Instance.ResolveWardElection();
                GameSession.Instance.ApplyReward(0, 0, 10, 14);
                GameSession.Instance.ApplyPoliticalReward(5, 9, 14);
                GameSession.Instance.ApplyCampaignReward(5, 0);
                GameSession.Instance.ApplyGovernanceReward(65, 75, 2);
                GameSession.Instance.ResolveHundredDayReview();
                GameSession.Instance.ApplyReward(0, 0, 0, 2);
                GameSession.Instance.ApplyGovernanceReward(0, 12, 3);
                GameSession.Instance.ApplyAssemblyReward(2, 5, 2);
                GameSession.Instance.ResolveHundredDayReview();
                GameSession.Instance.ApplyReward(0, -200, 0, 10);
                GameSession.Instance.ApplyPoliticalReward(5, 18, 11);
                GameSession.Instance.ApplyAssemblyReward(68, 70, 77);
                GameSession.Instance.ResolveAssemblyNomination();
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                GameSession.Instance.CompleteChapter(7);
                GameSession.Instance.CompleteChapter(8);
                GameSession.Instance.CompleteChapter(9);
                mission.ResetMission(false);
            }
            else
            {
                GameSession.Instance.ResetProgress();
                GameSession.Instance.ApplyReward(88, -500, 86, 42);
                GameSession.Instance.ApplyPoliticalReward(28, 55, 17);
                GameSession.Instance.ApplyCampaignReward(62, 85);
                GameSession.Instance.ResolveWardElection();
                GameSession.Instance.ApplyReward(0, 0, 10, 14);
                GameSession.Instance.ApplyPoliticalReward(5, 9, 14);
                GameSession.Instance.ApplyCampaignReward(5, 0);
                GameSession.Instance.ApplyGovernanceReward(65, 75, 2);
                GameSession.Instance.ResolveHundredDayReview();
                GameSession.Instance.ApplyReward(0, 0, 0, 2);
                GameSession.Instance.ApplyGovernanceReward(0, 12, 3);
                GameSession.Instance.ApplyAssemblyReward(2, 5, 2);
                GameSession.Instance.ResolveHundredDayReview();
                GameSession.Instance.ApplyReward(0, -200, 0, 10);
                GameSession.Instance.ApplyPoliticalReward(5, 18, 11);
                GameSession.Instance.ApplyAssemblyReward(68, 70, 77);
                GameSession.Instance.ResolveAssemblyNomination();
                GameSession.Instance.ApplyReward(0, -100, 0, 11);
                GameSession.Instance.ApplyPoliticalReward(2, 17, 14);
                GameSession.Instance.ApplyAssemblyReward(18, 18, 20);
                GameSession.Instance.ApplyAssemblyCampaignReward(58, 92, 69);
                GameSession.Instance.ResolveAssemblyElection();
                GameSession.Instance.CompleteChapter(1);
                GameSession.Instance.CompleteChapter(2);
                GameSession.Instance.CompleteChapter(3);
                GameSession.Instance.CompleteChapter(4);
                GameSession.Instance.CompleteChapter(5);
                GameSession.Instance.CompleteChapter(6);
                GameSession.Instance.CompleteChapter(7);
                GameSession.Instance.CompleteChapter(8);
                GameSession.Instance.CompleteChapter(9);
                GameSession.Instance.CompleteChapter(10);
                mission.ResetMission(false);
            }

            string filePrefix = chapterNumber == 1
                ? "prototype"
                : riskyDecision ? $"chapter-{chapterNumber}-risky" : $"chapter-{chapterNumber}";
            string startPath = Path.Combine(outputDirectory, filePrefix + "-start.png");
            ScreenCapture.CaptureScreenshot(startPath);
            yield return new WaitForSeconds(0.8f);

            int guard = 0;
            while (!mission.IsComplete && guard++ < 16)
            {
                MissionObjective objective = mission.CurrentObjectiveItem;
                if (objective == null)
                {
                    break;
                }

                player.transform.position = objective.transform.position + new Vector3(0f, 0.1f, -1.1f);
                objective.Interact(player);
                if (objective.RequiresDecision)
                {
                    yield return new WaitForSeconds(0.25f);
                    ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, filePrefix + "-decision.png"));
                    yield return new WaitForSeconds(0.55f);
                    objective.ResolveDecisionForAutomation(riskyDecision ? 2 : 1);
                }
                yield return new WaitForSeconds(0.45f);
            }

            if (chapterNumber >= 3)
            {
                player.transform.position = new Vector3(0f, 0.1f, -4f);
                player.transform.rotation = Quaternion.identity;
                yield return new WaitForSeconds(0.8f);
            }

            yield return new WaitForSeconds(0.9f);
            string finalPath = Path.Combine(outputDirectory, filePrefix + "-final.png");
            ScreenCapture.CaptureScreenshot(finalPath);
            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(6.5f);
            string actionsPath = Path.Combine(outputDirectory, filePrefix + "-actions.png");
            ScreenCapture.CaptureScreenshot(actionsPath);
            yield return new WaitForSeconds(0.6f);

            PlayerProgress progress = GameSession.Instance.Progress;
            int requiredTrust = chapterNumber == 4 && riskyDecision ? 76
                : chapterNumber == 5 && riskyDecision ? 99 : expectedTrust;
            int requiredMoney = chapterNumber == 4 && riskyDecision ? 750
                : chapterNumber == 5 && riskyDecision ? 250 : expectedMoney;
            int requiredReputation = chapterNumber == 4 && riskyDecision ? 50
                : chapterNumber == 5 && riskyDecision ? 64 : expectedReputation;
            int requiredProof = chapterNumber == 5 && riskyDecision ? 9 : expectedProof;
            if (chapterNumber == 6 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 550;
                requiredReputation = 77;
                requiredProof = 26;
            }
            else if (chapterNumber == 7 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 500;
                requiredReputation = 85;
                requiredProof = 38;
            }
            else if (chapterNumber == 8 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 350;
                requiredReputation = 93;
                requiredProof = 51;
            }
            else if (chapterNumber == 9 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 96;
                requiredProof = 63;
            }
            else if (chapterNumber == 10 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 95;
                requiredProof = 74;
            }
            else if (chapterNumber == 11 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 50;
                requiredReputation = 95;
                requiredProof = 89;
            }
            int requiredDecision = riskyDecision ? 2 : 1;
            int requiredPower = chapterNumber == 6 && riskyDecision ? 20 : expectedPower;
            int requiredVolunteers = chapterNumber == 6 && riskyDecision ? 32 : expectedVolunteers;
            int requiredPressure = chapterNumber == 6 && riskyDecision ? 17 : expectedPressure;
            if (chapterNumber == 7 && riskyDecision)
            {
                requiredPower = 30;
                requiredVolunteers = 52;
                requiredPressure = 25;
            }
            else if (chapterNumber == 8 && riskyDecision)
            {
                requiredPower = 35;
                requiredVolunteers = 64;
                requiredPressure = 40;
            }
            else if (chapterNumber == 9 && riskyDecision)
            {
                requiredPower = 40;
                requiredVolunteers = 79;
                requiredPressure = 51;
            }
            else if (chapterNumber == 10 && riskyDecision)
            {
                requiredPower = 43;
                requiredVolunteers = 96;
                requiredPressure = 65;
            }
            else if (chapterNumber == 11 && riskyDecision)
            {
                requiredPower = 47;
                requiredVolunteers = 111;
                requiredPressure = 86;
            }
            int requiredSupport = chapterNumber == 7 && riskyDecision ? 66 : expectedSupport;
            int requiredBooth = chapterNumber == 7 && riskyDecision ? 70 : expectedBooth;
            int requiredVoteShare = chapterNumber == 7 && riskyDecision ? 58 : expectedVoteShare;
            int requiredDelivery = chapterNumber == 8 && riskyDecision ? 71 : expectedDelivery;
            int requiredIntegrity = chapterNumber == 8 && riskyDecision ? 48 : expectedIntegrity;
            int requiredBudget = chapterNumber == 8 && riskyDecision ? -1 : expectedBudget;
            int requiredGovernanceScore = chapterNumber == 8 && riskyDecision ? 55 : expectedGovernanceScore;
            bool requiredReviewPassed = chapterNumber == 8 && riskyDecision ? false : expectedReviewPassed;
            int requiredAssemblyReach = chapterNumber == 9 && riskyDecision ? 78
                : chapterNumber == 10 && riskyDecision ? 93 : expectedAssemblyReach;
            int requiredCoalitionUnity = chapterNumber == 9 && riskyDecision ? 57
                : chapterNumber == 10 && riskyDecision ? 83 : expectedCoalitionUnity;
            int requiredAssemblyReadiness = chapterNumber == 9 && riskyDecision ? 62
                : chapterNumber == 10 && riskyDecision ? 90 : expectedAssemblyReadiness;
            int requiredNominationScore = chapterNumber == 9 && riskyDecision ? 84 : expectedNominationScore;
            bool requiredAssemblyNomination = chapterNumber == 9 && riskyDecision || expectedAssemblyNomination;
            int requiredConstituencySupport = chapterNumber == 10 && riskyDecision ? 66 : expectedConstituencySupport;
            int requiredCampaignCompliance = chapterNumber == 10 && riskyDecision ? 75 : expectedCampaignCompliance;
            int requiredElectionOperations = chapterNumber == 10 && riskyDecision ? 55 : expectedElectionOperations;
            int requiredAssemblyVoteShare = chapterNumber == 10 && riskyDecision ? 55 : expectedAssemblyVoteShare;
            bool requiredAssemblyWin = chapterNumber == 10 && riskyDecision || expectedAssemblyWin;
            int requiredLegislativeEffectiveness = chapterNumber == 11 && riskyDecision ? 85 : expectedLegislativeEffectiveness;
            int requiredConstituencyService = chapterNumber == 11 && riskyDecision ? 84 : expectedConstituencyService;
            int requiredEthics = chapterNumber == 11 && riskyDecision ? 60 : expectedEthics;
            int requiredMlaAllocation = chapterNumber == 11 && riskyDecision ? 15 : expectedMlaAllocation;
            int requiredMlaPerformance = chapterNumber == 11 && riskyDecision ? 73 : expectedMlaPerformance;
            bool requiredMlaTermOnTrack = chapterNumber == 11 && riskyDecision || expectedMlaTermOnTrack;
            bool passed = mission.IsComplete
                && progress.publicTrust == requiredTrust
                && progress.money == requiredMoney
                && progress.reputation == requiredReputation
                && progress.caseProof == requiredProof
                && progress.politicalPower == requiredPower
                && progress.volunteers == requiredVolunteers
                && progress.oppositionPressure == requiredPressure
                && progress.wardSupport == requiredSupport
                && progress.boothReadiness == requiredBooth
                && progress.wardVoteShare == requiredVoteShare
                && progress.wardElectionWon == expectedWardWin
                && progress.serviceDelivery == requiredDelivery
                && progress.fiscalIntegrity == requiredIntegrity
                && progress.wardBudgetLakhs == requiredBudget
                && progress.governanceScore == requiredGovernanceScore
                && progress.hundredDayReviewPassed == requiredReviewPassed
                && progress.assemblyReach == requiredAssemblyReach
                && progress.coalitionUnity == requiredCoalitionUnity
                && progress.assemblyReadiness == requiredAssemblyReadiness
                && progress.nominationScore == requiredNominationScore
                && progress.assemblyCandidateNominated == requiredAssemblyNomination
                && progress.constituencySupport == requiredConstituencySupport
                && progress.campaignCompliance == requiredCampaignCompliance
                && progress.electionOperations == requiredElectionOperations
                && progress.assemblyVoteShare == requiredAssemblyVoteShare
                && progress.assemblyElectionWon == requiredAssemblyWin
                && progress.legislativeEffectiveness == requiredLegislativeEffectiveness
                && progress.constituencyService == requiredConstituencyService
                && progress.ethicsRecord == requiredEthics
                && progress.mlaAllocationLakhs == requiredMlaAllocation
                && progress.mlaPerformanceScore == requiredMlaPerformance
                && progress.mlaTermOnTrack == requiredMlaTermOnTrack
                && (chapterNumber != 4 || progress.rescueApproach == requiredDecision)
                && (chapterNumber != 5 || progress.hospitalApproach == requiredDecision)
                && (chapterNumber != 6 || progress.oppositionResponse == requiredDecision)
                && (chapterNumber != 7 || progress.campaignStrategy == requiredDecision)
                && (chapterNumber != 8 || progress.governanceApproach == requiredDecision)
                && (chapterNumber != 9 || progress.expansionStrategy == requiredDecision)
                && (chapterNumber != 10 || progress.assemblyCampaignStrategy == requiredDecision)
                && (chapterNumber != 11 || progress.legislativeStrategy == requiredDecision)
                && (PrototypeHud.Instance == null || !PrototypeHud.Instance.IsDecisionOpen);
            string marker = chapterNumber == 1
                ? "PROTOTYPE"
                : riskyDecision ? $"CHAPTER_{chapterNumber}_RISKY" : $"CHAPTER_{chapterNumber}";
            Debug.Log(passed
                ? $"{marker}_SMOKE_PASSED: trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, review={progress.governanceScore}, nomination={progress.nominationScore}, vote={progress.assemblyVoteShare}, won={progress.assemblyElectionWon}, legislative={progress.legislativeEffectiveness}, service={progress.constituencyService}, ethics={progress.ethicsRecord}, allocation={progress.mlaAllocationLakhs}, performance={progress.mlaPerformanceScore}, onTrack={progress.mlaTermOnTrack}"
                : $"{marker}_SMOKE_FAILED: complete={mission.IsComplete}, trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, wardVote={progress.wardVoteShare}, review={progress.governanceScore}, reach={progress.assemblyReach}, unity={progress.coalitionUnity}, ready={progress.assemblyReadiness}, nomination={progress.nominationScore}, support={progress.constituencySupport}, compliance={progress.campaignCompliance}, operations={progress.electionOperations}, vote={progress.assemblyVoteShare}, won={progress.assemblyElectionWon}, legislative={progress.legislativeEffectiveness}, service={progress.constituencyService}, ethics={progress.ethicsRecord}, allocation={progress.mlaAllocationLakhs}, performance={progress.mlaPerformanceScore}, onTrack={progress.mlaTermOnTrack}");
            Application.Quit(passed ? 0 : 3);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}
