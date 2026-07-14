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
        [SerializeField] private int expectedDistrictReach;
        [SerializeField] private int expectedCandidateQuality;
        [SerializeField] private int expectedOrganizationDiscipline;
        [SerializeField] private int expectedDistrictExpansionScore;
        [SerializeField] private bool expectedDistrictNetworkReady;
        [SerializeField] private int expectedStateCampaignReach;
        [SerializeField] private int expectedCandidateSlateIntegrity;
        [SerializeField] private int expectedStateElectionOperations;
        [SerializeField] private int expectedStateExpansionScore;
        [SerializeField] private int expectedStateSeatsWon;
        [SerializeField] private bool expectedStateFootholdSecured;
        [SerializeField] private int expectedStatePolicyCredibility;
        [SerializeField] private int expectedStateCaucusUnity;
        [SerializeField] private int expectedPublicLeadership;
        [SerializeField] private int expectedStateLeadershipScore;
        [SerializeField] private bool expectedStateLeaderSelected;
        [SerializeField] private int expectedStatewideSupport;
        [SerializeField] private int expectedStatewideCampaignCompliance;
        [SerializeField] private int expectedStatewideElectionOperations;
        [SerializeField] private int expectedStatewideVoteShare;
        [SerializeField] private int expectedStateAssemblySeatsWon;
        [SerializeField] private bool expectedChiefMinisterElected;
        [SerializeField] private int expectedChiefMinisterDelivery;
        [SerializeField] private int expectedCabinetIntegrity;
        [SerializeField] private int expectedStateFiscalDiscipline;
        [SerializeField] private int expectedChiefMinisterGovernanceScore;
        [SerializeField] private bool expectedChiefMinisterHundredDayReviewPassed;
        [SerializeField] private int expectedStateHealthOutcome;
        [SerializeField] private int expectedStateLearningOutcome;
        [SerializeField] private int expectedStateSafetyOutcome;
        [SerializeField] private int expectedStateLivelihoodOutcome;
        [SerializeField] private int expectedStateTermScore;
        [SerializeField] private bool expectedStateTermReviewPassed;

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
            bool mlaTermOnTrack = false,
            int districtReach = 0,
            int candidateQuality = 0,
            int organizationDiscipline = 0,
            int districtExpansionScore = 0,
            bool districtNetworkReady = false,
            int stateCampaignReach = 0,
            int candidateSlateIntegrity = 0,
            int stateElectionOperations = 0,
            int stateExpansionScore = 0,
            int stateSeatsWon = 0,
            bool stateFootholdSecured = false,
            int statePolicyCredibility = 0,
            int stateCaucusUnity = 0,
            int publicLeadership = 0,
            int stateLeadershipScore = 0,
            bool stateLeaderSelected = false,
            int statewideSupport = 0,
            int statewideCampaignCompliance = 0,
            int statewideElectionOperations = 0,
            int statewideVoteShare = 0,
            int stateAssemblySeatsWon = 0,
            bool chiefMinisterElected = false,
            int chiefMinisterDelivery = 0,
            int cabinetIntegrity = 0,
            int stateFiscalDiscipline = 0,
            int chiefMinisterGovernanceScore = 0,
            bool chiefMinisterHundredDayReviewPassed = false,
            int stateHealthOutcome = 0,
            int stateLearningOutcome = 0,
            int stateSafetyOutcome = 0,
            int stateLivelihoodOutcome = 0,
            int stateTermScore = 0,
            bool stateTermReviewPassed = false)
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
            expectedDistrictReach = districtReach;
            expectedCandidateQuality = candidateQuality;
            expectedOrganizationDiscipline = organizationDiscipline;
            expectedDistrictExpansionScore = districtExpansionScore;
            expectedDistrictNetworkReady = districtNetworkReady;
            expectedStateCampaignReach = stateCampaignReach;
            expectedCandidateSlateIntegrity = candidateSlateIntegrity;
            expectedStateElectionOperations = stateElectionOperations;
            expectedStateExpansionScore = stateExpansionScore;
            expectedStateSeatsWon = stateSeatsWon;
            expectedStateFootholdSecured = stateFootholdSecured;
            expectedStatePolicyCredibility = statePolicyCredibility;
            expectedStateCaucusUnity = stateCaucusUnity;
            expectedPublicLeadership = publicLeadership;
            expectedStateLeadershipScore = stateLeadershipScore;
            expectedStateLeaderSelected = stateLeaderSelected;
            expectedStatewideSupport = statewideSupport;
            expectedStatewideCampaignCompliance = statewideCampaignCompliance;
            expectedStatewideElectionOperations = statewideElectionOperations;
            expectedStatewideVoteShare = statewideVoteShare;
            expectedStateAssemblySeatsWon = stateAssemblySeatsWon;
            expectedChiefMinisterElected = chiefMinisterElected;
            expectedChiefMinisterDelivery = chiefMinisterDelivery;
            expectedCabinetIntegrity = cabinetIntegrity;
            expectedStateFiscalDiscipline = stateFiscalDiscipline;
            expectedChiefMinisterGovernanceScore = chiefMinisterGovernanceScore;
            expectedChiefMinisterHundredDayReviewPassed = chiefMinisterHundredDayReviewPassed;
            expectedStateHealthOutcome = stateHealthOutcome;
            expectedStateLearningOutcome = stateLearningOutcome;
            expectedStateSafetyOutcome = stateSafetyOutcome;
            expectedStateLivelihoodOutcome = stateLivelihoodOutcome;
            expectedStateTermScore = stateTermScore;
            expectedStateTermReviewPassed = stateTermReviewPassed;
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
                || (chapterNumber == 11 && System.Array.IndexOf(arguments, "-riskyLegislativeSmoke") >= 0)
                || (chapterNumber == 12 && System.Array.IndexOf(arguments, "-riskyDistrictSmoke") >= 0)
                || (chapterNumber == 13 && System.Array.IndexOf(arguments, "-riskyStateExpansionSmoke") >= 0)
                || (chapterNumber == 14 && System.Array.IndexOf(arguments, "-riskyStateLeadershipSmoke") >= 0)
                || (chapterNumber == 15 && System.Array.IndexOf(arguments, "-riskyStateElectionSmoke") >= 0)
                || (chapterNumber == 16 && System.Array.IndexOf(arguments, "-riskyCmGovernanceSmoke") >= 0)
                || (chapterNumber == 17 && System.Array.IndexOf(arguments, "-riskyStateReformSmoke") >= 0))
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
                    && System.Array.IndexOf(arguments, "-riskyLegislativeSmoke") >= 0)
                || (chapterNumber == 12
                    && System.Array.IndexOf(arguments, "-riskyDistrictSmoke") >= 0)
                || (chapterNumber == 13
                    && System.Array.IndexOf(arguments, "-riskyStateExpansionSmoke") >= 0)
                || (chapterNumber == 14
                    && System.Array.IndexOf(arguments, "-riskyStateLeadershipSmoke") >= 0)
                || (chapterNumber == 15
                    && System.Array.IndexOf(arguments, "-riskyStateElectionSmoke") >= 0)
                || (chapterNumber == 16
                    && System.Array.IndexOf(arguments, "-riskyCmGovernanceSmoke") >= 0)
                || (chapterNumber == 17
                    && System.Array.IndexOf(arguments, "-riskyStateReformSmoke") >= 0);

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
            else if (chapterNumber == 11)
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
            else if (chapterNumber == 12)
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
                GameSession.Instance.ApplyReward(0, 0, 0, 17);
                GameSession.Instance.ApplyPoliticalReward(3, 15, 20);
                GameSession.Instance.ApplyLegislativeReward(78, 80, 90, 30);
                GameSession.Instance.ResolveMlaPerformance();
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
                GameSession.Instance.CompleteChapter(11);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 13)
            {
                PrepareChapterThirteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 14)
            {
                PrepareChapterFourteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 15)
            {
                PrepareChapterFifteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 16)
            {
                PrepareChapterSixteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else
            {
                PrepareChapterSeventeenBaseline(GameSession.Instance);
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
            else if (chapterNumber == 12 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 40;
                requiredReputation = 90;
                requiredProof = 99;
            }
            else if (chapterNumber == 13 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 130;
                requiredReputation = 90;
                requiredProof = 99;
            }
            else if (chapterNumber == 14 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 40;
                requiredReputation = 90;
                requiredProof = 98;
            }
            else if (chapterNumber == 15 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 150;
                requiredReputation = 90;
                requiredProof = 98;
            }
            else if (chapterNumber == 16 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 30;
                requiredReputation = 90;
                requiredProof = 95;
            }
            else if (chapterNumber == 17 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 10;
                requiredReputation = 90;
                requiredProof = 95;
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
            else if (chapterNumber == 12 && riskyDecision)
            {
                requiredPower = 51;
                requiredVolunteers = 135;
                requiredPressure = 100;
            }
            else if (chapterNumber == 13 && riskyDecision)
            {
                requiredPower = 56;
                requiredVolunteers = 161;
                requiredPressure = 100;
            }
            else if (chapterNumber == 14 && riskyDecision)
            {
                requiredPower = 60;
                requiredVolunteers = 187;
                requiredPressure = 97;
            }
            else if (chapterNumber == 15 && riskyDecision)
            {
                requiredPower = 64;
                requiredVolunteers = 213;
                requiredPressure = 96;
            }
            else if (chapterNumber == 16 && riskyDecision)
            {
                requiredPower = 68;
                requiredVolunteers = 257;
                requiredPressure = 94;
            }
            else if (chapterNumber == 17 && riskyDecision)
            {
                requiredPower = 71;
                requiredVolunteers = 320;
                requiredPressure = 97;
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
            int requiredDistrictReach = chapterNumber == 12 && riskyDecision ? 90 : expectedDistrictReach;
            int requiredCandidateQuality = chapterNumber == 12 && riskyDecision ? 65 : expectedCandidateQuality;
            int requiredOrganizationDiscipline = chapterNumber == 12 && riskyDecision ? 68 : expectedOrganizationDiscipline;
            int requiredDistrictExpansionScore = chapterNumber == 12 && riskyDecision ? 77 : expectedDistrictExpansionScore;
            bool requiredDistrictNetworkReady = chapterNumber == 12 && riskyDecision || expectedDistrictNetworkReady;
            int requiredStateCampaignReach = chapterNumber == 13 && riskyDecision ? 92 : expectedStateCampaignReach;
            int requiredCandidateSlateIntegrity = chapterNumber == 13 && riskyDecision ? 67 : expectedCandidateSlateIntegrity;
            int requiredStateElectionOperations = chapterNumber == 13 && riskyDecision ? 74 : expectedStateElectionOperations;
            int requiredStateExpansionScore = chapterNumber == 13 && riskyDecision ? 73 : expectedStateExpansionScore;
            int requiredStateSeatsWon = chapterNumber == 13 && riskyDecision ? 5 : expectedStateSeatsWon;
            bool requiredStateFootholdSecured = chapterNumber == 13 && riskyDecision || expectedStateFootholdSecured;
            int requiredStatePolicyCredibility = chapterNumber == 14 && riskyDecision ? 64 : expectedStatePolicyCredibility;
            int requiredStateCaucusUnity = chapterNumber == 14 && riskyDecision ? 82 : expectedStateCaucusUnity;
            int requiredPublicLeadership = chapterNumber == 14 && riskyDecision ? 70 : expectedPublicLeadership;
            int requiredStateLeadershipScore = chapterNumber == 14 && riskyDecision ? 75 : expectedStateLeadershipScore;
            bool requiredStateLeaderSelected = chapterNumber == 14 && riskyDecision || expectedStateLeaderSelected;
            int requiredStatewideSupport = chapterNumber == 15 && riskyDecision ? 88 : expectedStatewideSupport;
            int requiredStatewideCampaignCompliance = chapterNumber == 15 && riskyDecision ? 75 : expectedStatewideCampaignCompliance;
            int requiredStatewideElectionOperations = chapterNumber == 15 && riskyDecision ? 68 : expectedStatewideElectionOperations;
            int requiredStatewideVoteShare = chapterNumber == 15 && riskyDecision ? 52 : expectedStatewideVoteShare;
            int requiredStateAssemblySeatsWon = chapterNumber == 15 && riskyDecision ? 23 : expectedStateAssemblySeatsWon;
            bool requiredChiefMinisterElected = chapterNumber == 15 && riskyDecision || expectedChiefMinisterElected;
            int requiredChiefMinisterDelivery = chapterNumber == 16 && riskyDecision ? 100 : expectedChiefMinisterDelivery;
            int requiredCabinetIntegrity = chapterNumber == 16 && riskyDecision ? 73 : expectedCabinetIntegrity;
            int requiredStateFiscalDiscipline = chapterNumber == 16 && riskyDecision ? 62 : expectedStateFiscalDiscipline;
            int requiredChiefMinisterGovernanceScore = chapterNumber == 16 && riskyDecision ? 78 : expectedChiefMinisterGovernanceScore;
            bool requiredChiefMinisterHundredDayReviewPassed = chapterNumber == 16 && riskyDecision || expectedChiefMinisterHundredDayReviewPassed;
            int requiredStateHealthOutcome = chapterNumber == 17 && riskyDecision ? 100 : expectedStateHealthOutcome;
            int requiredStateLearningOutcome = chapterNumber == 17 && riskyDecision ? 69 : expectedStateLearningOutcome;
            int requiredStateSafetyOutcome = chapterNumber == 17 && riskyDecision ? 70 : expectedStateSafetyOutcome;
            int requiredStateLivelihoodOutcome = chapterNumber == 17 && riskyDecision ? 97 : expectedStateLivelihoodOutcome;
            int requiredStateTermScore = chapterNumber == 17 && riskyDecision ? 82 : expectedStateTermScore;
            bool requiredStateTermReviewPassed = chapterNumber == 17 && riskyDecision || expectedStateTermReviewPassed;
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
                && progress.districtReach == requiredDistrictReach
                && progress.candidateQuality == requiredCandidateQuality
                && progress.organizationDiscipline == requiredOrganizationDiscipline
                && progress.districtExpansionScore == requiredDistrictExpansionScore
                && progress.districtNetworkReady == requiredDistrictNetworkReady
                && progress.stateCampaignReach == requiredStateCampaignReach
                && progress.candidateSlateIntegrity == requiredCandidateSlateIntegrity
                && progress.stateElectionOperations == requiredStateElectionOperations
                && progress.stateExpansionScore == requiredStateExpansionScore
                && progress.stateSeatsWon == requiredStateSeatsWon
                && progress.stateFootholdSecured == requiredStateFootholdSecured
                && progress.statePolicyCredibility == requiredStatePolicyCredibility
                && progress.stateCaucusUnity == requiredStateCaucusUnity
                && progress.publicLeadership == requiredPublicLeadership
                && progress.stateLeadershipScore == requiredStateLeadershipScore
                && progress.stateLeaderSelected == requiredStateLeaderSelected
                && progress.statewideSupport == requiredStatewideSupport
                && progress.statewideCampaignCompliance == requiredStatewideCampaignCompliance
                && progress.statewideElectionOperations == requiredStatewideElectionOperations
                && progress.statewideVoteShare == requiredStatewideVoteShare
                && progress.stateAssemblySeatsWon == requiredStateAssemblySeatsWon
                && progress.chiefMinisterElected == requiredChiefMinisterElected
                && progress.chiefMinisterDelivery == requiredChiefMinisterDelivery
                && progress.cabinetIntegrity == requiredCabinetIntegrity
                && progress.stateFiscalDiscipline == requiredStateFiscalDiscipline
                && progress.chiefMinisterGovernanceScore == requiredChiefMinisterGovernanceScore
                && progress.chiefMinisterHundredDayReviewPassed == requiredChiefMinisterHundredDayReviewPassed
                && progress.stateHealthOutcome == requiredStateHealthOutcome
                && progress.stateLearningOutcome == requiredStateLearningOutcome
                && progress.stateSafetyOutcome == requiredStateSafetyOutcome
                && progress.stateLivelihoodOutcome == requiredStateLivelihoodOutcome
                && progress.stateTermScore == requiredStateTermScore
                && progress.stateTermReviewPassed == requiredStateTermReviewPassed
                && (chapterNumber != 4 || progress.rescueApproach == requiredDecision)
                && (chapterNumber != 5 || progress.hospitalApproach == requiredDecision)
                && (chapterNumber != 6 || progress.oppositionResponse == requiredDecision)
                && (chapterNumber != 7 || progress.campaignStrategy == requiredDecision)
                && (chapterNumber != 8 || progress.governanceApproach == requiredDecision)
                && (chapterNumber != 9 || progress.expansionStrategy == requiredDecision)
                && (chapterNumber != 10 || progress.assemblyCampaignStrategy == requiredDecision)
                && (chapterNumber != 11 || progress.legislativeStrategy == requiredDecision)
                && (chapterNumber != 12 || progress.districtStrategy == requiredDecision)
                && (chapterNumber != 13 || progress.stateExpansionStrategy == requiredDecision)
                && (chapterNumber != 14 || progress.stateLeadershipStrategy == requiredDecision)
                && (chapterNumber != 15 || progress.stateElectionStrategy == requiredDecision)
                && (chapterNumber != 16 || progress.chiefMinisterGovernanceApproach == requiredDecision)
                && (chapterNumber != 17 || progress.stateReformApproach == requiredDecision)
                && (PrototypeHud.Instance == null || !PrototypeHud.Instance.IsDecisionOpen);
            string marker = chapterNumber == 1
                ? "PROTOTYPE"
                : riskyDecision ? $"CHAPTER_{chapterNumber}_RISKY" : $"CHAPTER_{chapterNumber}";
            Debug.Log(passed
                ? $"{marker}_SMOKE_PASSED: trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, mla={progress.mlaPerformanceScore}, districtReach={progress.districtReach}, quality={progress.candidateQuality}, discipline={progress.organizationDiscipline}, expansion={progress.districtExpansionScore}, stateReach={progress.stateCampaignReach}, slate={progress.candidateSlateIntegrity}, ops={progress.stateElectionOperations}, stateScore={progress.stateExpansionScore}, seats={progress.stateSeatsWon}, policy={progress.statePolicyCredibility}, caucus={progress.stateCaucusUnity}, publicLead={progress.publicLeadership}, leadership={progress.stateLeadershipScore}, statewideSupport={progress.statewideSupport}, rules={progress.statewideCampaignCompliance}, pollOps={progress.statewideElectionOperations}, statewideVote={progress.statewideVoteShare}, assemblySeats={progress.stateAssemblySeatsWon}, cm={progress.chiefMinisterElected}, cmDelivery={progress.chiefMinisterDelivery}, cabinet={progress.cabinetIntegrity}, fiscal={progress.stateFiscalDiscipline}, cmScore={progress.chiefMinisterGovernanceScore}, cmReview={progress.chiefMinisterHundredDayReviewPassed}, health={progress.stateHealthOutcome}, learning={progress.stateLearningOutcome}, safety={progress.stateSafetyOutcome}, livelihood={progress.stateLivelihoodOutcome}, termScore={progress.stateTermScore}, termReview={progress.stateTermReviewPassed}"
                : $"{marker}_SMOKE_FAILED: complete={mission.IsComplete}, trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, vote={progress.assemblyVoteShare}, legislative={progress.legislativeEffectiveness}, service={progress.constituencyService}, ethics={progress.ethicsRecord}, allocation={progress.mlaAllocationLakhs}, mla={progress.mlaPerformanceScore}, districtReach={progress.districtReach}, quality={progress.candidateQuality}, discipline={progress.organizationDiscipline}, expansion={progress.districtExpansionScore}, stateReach={progress.stateCampaignReach}, slate={progress.candidateSlateIntegrity}, ops={progress.stateElectionOperations}, stateScore={progress.stateExpansionScore}, seats={progress.stateSeatsWon}, policy={progress.statePolicyCredibility}, caucus={progress.stateCaucusUnity}, publicLead={progress.publicLeadership}, leadership={progress.stateLeadershipScore}, statewideSupport={progress.statewideSupport}, rules={progress.statewideCampaignCompliance}, pollOps={progress.statewideElectionOperations}, statewideVote={progress.statewideVoteShare}, assemblySeats={progress.stateAssemblySeatsWon}, cm={progress.chiefMinisterElected}, cmDelivery={progress.chiefMinisterDelivery}, cabinet={progress.cabinetIntegrity}, fiscal={progress.stateFiscalDiscipline}, cmScore={progress.chiefMinisterGovernanceScore}, cmReview={progress.chiefMinisterHundredDayReviewPassed}, health={progress.stateHealthOutcome}, learning={progress.stateLearningOutcome}, safety={progress.stateSafetyOutcome}, livelihood={progress.stateLivelihoodOutcome}, termScore={progress.stateTermScore}, termReview={progress.stateTermReviewPassed}");
            Application.Quit(passed ? 0 : 3);
        }

        private static void PrepareChapterThirteenBaseline(GameSession session)
        {
            session.ResetProgress();
            session.ApplyReward(88, -500, 86, 42);
            session.ApplyPoliticalReward(28, 55, 17);
            session.ApplyCampaignReward(62, 85);
            session.ResolveWardElection();
            session.ApplyReward(0, 0, 10, 14);
            session.ApplyPoliticalReward(5, 9, 14);
            session.ApplyCampaignReward(5, 0);
            session.ApplyGovernanceReward(65, 75, 2);
            session.ResolveHundredDayReview();
            session.ApplyReward(0, 0, 0, 2);
            session.ApplyGovernanceReward(0, 12, 3);
            session.ApplyAssemblyReward(2, 5, 2);
            session.ResolveHundredDayReview();
            session.ApplyReward(0, -200, 0, 10);
            session.ApplyPoliticalReward(5, 18, 11);
            session.ApplyAssemblyReward(68, 70, 77);
            session.ResolveAssemblyNomination();
            session.ApplyReward(0, -100, 0, 11);
            session.ApplyPoliticalReward(2, 17, 14);
            session.ApplyAssemblyReward(18, 18, 20);
            session.ApplyAssemblyCampaignReward(58, 92, 69);
            session.ResolveAssemblyElection();
            session.ApplyReward(0, 0, 0, 17);
            session.ApplyPoliticalReward(3, 15, 20);
            session.ApplyLegislativeReward(78, 80, 90, 30);
            session.ResolveMlaPerformance();
            session.ApplyReward(0, -40, 0, 8);
            session.ApplyPoliticalReward(4, 25, 21);
            session.ApplyDistrictReward(78, 90, 92);
            session.ResolveDistrictExpansion();
            session.SetStoryDecision("district-strategy", 1);
            for (int chapter = 1; chapter <= 12; chapter++)
            {
                session.CompleteChapter(chapter);
            }
        }

        private static void PrepareChapterFourteenBaseline(GameSession session)
        {
            PrepareChapterThirteenBaseline(session);
            session.ApplyReward(0, 30, 2, 8);
            session.ApplyPoliticalReward(5, 26, 2);
            session.ApplyStateExpansionReward(80, 89, 98);
            session.ResolveStateFoothold();
            session.SetStoryDecision("state-expansion-strategy", 1);
            session.CompleteChapter(13);
        }

        private static void PrepareChapterFifteenBaseline(GameSession session)
        {
            PrepareChapterFourteenBaseline(session);
            session.ApplyReward(0, -30, 0, 8);
            session.ApplyPoliticalReward(4, 25, -7);
            session.ApplyStateLeadershipReward(84, 86, 88);
            session.ResolveStateLeadership();
            session.SetStoryDecision("state-leadership-strategy", 1);
            session.CompleteChapter(14);
        }

        private static void PrepareChapterSixteenBaseline(GameSession session)
        {
            PrepareChapterFifteenBaseline(session);
            session.ApplyReward(0, 20, 0, 0);
            session.ApplyPoliticalReward(3, 28, -4);
            session.ApplyStateElectionReward(78, 100, 90);
            session.ResolveStateElection();
            session.SetStoryDecision("state-election-strategy", 1);
            session.CompleteChapter(15);
        }

        private static void PrepareChapterSeventeenBaseline(GameSession session)
        {
            PrepareChapterSixteenBaseline(session);
            session.ApplyReward(0, -20, 0, 0);
            session.ApplyPoliticalReward(3, 45, -5);
            session.ApplyChiefMinisterGovernanceReward(92, 100, 80);
            session.ResolveChiefMinisterHundredDayReview();
            session.SetStoryDecision("cm-governance-approach", 1);
            session.CompleteChapter(16);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}
