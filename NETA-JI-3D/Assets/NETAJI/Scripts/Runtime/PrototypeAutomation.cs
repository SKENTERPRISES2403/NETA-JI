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
        [SerializeField] private int expectedNationalOrganizationReach;
        [SerializeField] private int expectedFederalAllianceTrust;
        [SerializeField] private int expectedNationalPolicyCredibility;
        [SerializeField] private int expectedNationalReadinessScore;
        [SerializeField] private int expectedNationalRegionsAligned;
        [SerializeField] private bool expectedNationalExpansionReady;
        [SerializeField] private int expectedNationalCampaignSupport;
        [SerializeField] private int expectedNationalCampaignCompliance;
        [SerializeField] private int expectedNationalElectionOperations;
        [SerializeField] private int expectedFirstNationalVoteShare;
        [SerializeField] private int expectedFirstNationalSeatsWon;
        [SerializeField] private bool expectedFirstNationalElectionContested;
        [SerializeField] private bool expectedFirstNationalElectionWon;
        [SerializeField] private int expectedOppositionServiceRecord;
        [SerializeField] private int expectedNationalAllianceRenewal;
        [SerializeField] private int expectedNationalPolicyCorrection;
        [SerializeField] private int expectedNationalComebackScore;
        [SerializeField] private bool expectedNationalComebackReady;
        [SerializeField] private int expectedSecondNationalCampaignSupport;
        [SerializeField] private int expectedSecondNationalCampaignCompliance;
        [SerializeField] private int expectedSecondNationalElectionOperations;
        [SerializeField] private int expectedSecondNationalVoteShare;
        [SerializeField] private int expectedSecondNationalSeatsWon;
        [SerializeField] private bool expectedSecondNationalElectionContested;
        [SerializeField] private bool expectedPrimeMinisterElected;
        [SerializeField] private int expectedPrimeMinisterDelivery;
        [SerializeField] private int expectedUnionCabinetIntegrity;
        [SerializeField] private int expectedNationalFiscalDiscipline;
        [SerializeField] private int expectedInstitutionalTrust;
        [SerializeField] private int expectedPrimeMinisterHundredDayScore;
        [SerializeField] private bool expectedPrimeMinisterHundredDayReviewPassed;
        [SerializeField] private int expectedNationalHealthIndex;
        [SerializeField] private int expectedNationalLearningIndex;
        [SerializeField] private int expectedNationalSafetyJusticeIndex;
        [SerializeField] private int expectedNationalLivelihoodIndex;
        [SerializeField] private int expectedNationalDevelopmentScore;
        [SerializeField] private bool expectedNationalDevelopmentReviewPassed;
        [SerializeField] private int expectedGlobalTradeTrust;
        [SerializeField] private int expectedScienceInnovationLeadership;
        [SerializeField] private int expectedPeaceDefenseReadiness;
        [SerializeField] private int expectedHumanitarianClimateLeadership;
        [SerializeField] private int expectedGlobalLeadershipScore;
        [SerializeField] private bool expectedVishwaGuruOutcomeEarned;

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
            bool stateTermReviewPassed = false,
            int nationalOrganizationReach = 0,
            int federalAllianceTrust = 0,
            int nationalPolicyCredibility = 0,
            int nationalReadinessScore = 0,
            int nationalRegionsAligned = 0,
            bool nationalExpansionReady = false,
            int nationalCampaignSupport = 0,
            int nationalCampaignCompliance = 0,
            int nationalElectionOperations = 0,
            int firstNationalVoteShare = 0,
            int firstNationalSeatsWon = 0,
            bool firstNationalElectionContested = false,
            bool firstNationalElectionWon = false,
            int oppositionServiceRecord = 0,
            int nationalAllianceRenewal = 0,
            int nationalPolicyCorrection = 0,
            int nationalComebackScore = 0,
            bool nationalComebackReady = false,
            int secondNationalCampaignSupport = 0,
            int secondNationalCampaignCompliance = 0,
            int secondNationalElectionOperations = 0,
            int secondNationalVoteShare = 0,
            int secondNationalSeatsWon = 0,
            bool secondNationalElectionContested = false,
            bool primeMinisterElected = false,
            int primeMinisterDelivery = 0,
            int unionCabinetIntegrity = 0,
            int nationalFiscalDiscipline = 0,
            int institutionalTrust = 0,
            int primeMinisterHundredDayScore = 0,
            bool primeMinisterHundredDayReviewPassed = false,
            int nationalHealthIndex = 0,
            int nationalLearningIndex = 0,
            int nationalSafetyJusticeIndex = 0,
            int nationalLivelihoodIndex = 0,
            int nationalDevelopmentScore = 0,
            bool nationalDevelopmentReviewPassed = false,
            int globalTradeTrust = 0,
            int scienceInnovationLeadership = 0,
            int peaceDefenseReadiness = 0,
            int humanitarianClimateLeadership = 0,
            int globalLeadershipScore = 0,
            bool vishwaGuruOutcomeEarned = false)
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
            expectedNationalOrganizationReach = nationalOrganizationReach;
            expectedFederalAllianceTrust = federalAllianceTrust;
            expectedNationalPolicyCredibility = nationalPolicyCredibility;
            expectedNationalReadinessScore = nationalReadinessScore;
            expectedNationalRegionsAligned = nationalRegionsAligned;
            expectedNationalExpansionReady = nationalExpansionReady;
            expectedNationalCampaignSupport = nationalCampaignSupport;
            expectedNationalCampaignCompliance = nationalCampaignCompliance;
            expectedNationalElectionOperations = nationalElectionOperations;
            expectedFirstNationalVoteShare = firstNationalVoteShare;
            expectedFirstNationalSeatsWon = firstNationalSeatsWon;
            expectedFirstNationalElectionContested = firstNationalElectionContested;
            expectedFirstNationalElectionWon = firstNationalElectionWon;
            expectedOppositionServiceRecord = oppositionServiceRecord;
            expectedNationalAllianceRenewal = nationalAllianceRenewal;
            expectedNationalPolicyCorrection = nationalPolicyCorrection;
            expectedNationalComebackScore = nationalComebackScore;
            expectedNationalComebackReady = nationalComebackReady;
            expectedSecondNationalCampaignSupport = secondNationalCampaignSupport;
            expectedSecondNationalCampaignCompliance = secondNationalCampaignCompliance;
            expectedSecondNationalElectionOperations = secondNationalElectionOperations;
            expectedSecondNationalVoteShare = secondNationalVoteShare;
            expectedSecondNationalSeatsWon = secondNationalSeatsWon;
            expectedSecondNationalElectionContested = secondNationalElectionContested;
            expectedPrimeMinisterElected = primeMinisterElected;
            expectedPrimeMinisterDelivery = primeMinisterDelivery;
            expectedUnionCabinetIntegrity = unionCabinetIntegrity;
            expectedNationalFiscalDiscipline = nationalFiscalDiscipline;
            expectedInstitutionalTrust = institutionalTrust;
            expectedPrimeMinisterHundredDayScore = primeMinisterHundredDayScore;
            expectedPrimeMinisterHundredDayReviewPassed = primeMinisterHundredDayReviewPassed;
            expectedNationalHealthIndex = nationalHealthIndex;
            expectedNationalLearningIndex = nationalLearningIndex;
            expectedNationalSafetyJusticeIndex = nationalSafetyJusticeIndex;
            expectedNationalLivelihoodIndex = nationalLivelihoodIndex;
            expectedNationalDevelopmentScore = nationalDevelopmentScore;
            expectedNationalDevelopmentReviewPassed = nationalDevelopmentReviewPassed;
            expectedGlobalTradeTrust = globalTradeTrust;
            expectedScienceInnovationLeadership = scienceInnovationLeadership;
            expectedPeaceDefenseReadiness = peaceDefenseReadiness;
            expectedHumanitarianClimateLeadership = humanitarianClimateLeadership;
            expectedGlobalLeadershipScore = globalLeadershipScore;
            expectedVishwaGuruOutcomeEarned = vishwaGuruOutcomeEarned;
        }

        private void Start()
        {
            string[] arguments = System.Environment.GetCommandLineArgs();
            if (chapterNumber == 10 && System.Array.IndexOf(arguments, "-outfitSmoke") >= 0)
            {
                StartCoroutine(RunOutfitSmoke(arguments));
                return;
            }
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
                || (chapterNumber == 17 && System.Array.IndexOf(arguments, "-riskyStateReformSmoke") >= 0)
                || (chapterNumber == 18 && System.Array.IndexOf(arguments, "-riskyNationalExpansionSmoke") >= 0)
                || (chapterNumber == 19 && System.Array.IndexOf(arguments, "-riskyFirstNationalElectionSmoke") >= 0)
                || (chapterNumber == 20 && System.Array.IndexOf(arguments, "-riskyOppositionTermSmoke") >= 0)
                || (chapterNumber == 21 && System.Array.IndexOf(arguments, "-riskySecondNationalElectionSmoke") >= 0)
                || (chapterNumber == 22 && System.Array.IndexOf(arguments, "-riskyPrimeMinisterGovernanceSmoke") >= 0)
                || (chapterNumber == 23 && System.Array.IndexOf(arguments, "-riskyNationalDevelopmentSmoke") >= 0)
                || (chapterNumber == 24 && System.Array.IndexOf(arguments, "-riskyGlobalLeadershipSmoke") >= 0))
            {
                StartCoroutine(RunSmoke(arguments));
            }
        }

        private IEnumerator RunOutfitSmoke(string[] arguments)
        {
            string outputDirectory = ReadArgument(arguments, "-screenshotPath") ?? Application.persistentDataPath;
            Directory.CreateDirectory(outputDirectory);
            yield return new WaitForSeconds(1.2f);

            AzadController player = FindFirstObjectByType<AzadController>();
            Camera gameCamera = Camera.main;
            if (player == null || gameCamera == null)
            {
                Debug.LogError($"OUTFIT_SMOKE_FAILED: player={player != null}, camera={gameCamera != null}.");
                Application.Quit(4);
                yield break;
            }

            PrototypeHud hud = FindFirstObjectByType<PrototypeHud>();
            if (hud != null)
            {
                hud.enabled = false;
            }
            PrototypeInput input = PrototypeInput.Instance;
            if (input != null)
            {
                input.enabled = false;
            }
            ThirdPersonCamera orbit = gameCamera.GetComponent<ThirdPersonCamera>();
            if (orbit != null)
            {
                orbit.enabled = false;
            }

            player.SetControlEnabled(false);
            player.Teleport(new Vector3(0f, 0f, -8f), Quaternion.identity);
            gameCamera.fieldOfView = 38f;
            Vector3 focus = player.transform.position + new Vector3(0f, 1.05f, 0f);
            Vector3[] cameraOffsets =
            {
                new Vector3(0f, 1.20f, 4.3f),
                new Vector3(-4.3f, 1.20f, 0f),
                new Vector3(0f, 1.20f, -4.3f),
                new Vector3(4.3f, 1.20f, 0f)
            };
            string[] viewNames = { "front", "left", "back", "right" };
            for (int index = 0; index < cameraOffsets.Length; index++)
            {
                gameCamera.transform.position = focus + cameraOffsets[index];
                gameCamera.transform.rotation = Quaternion.LookRotation(focus - gameCamera.transform.position, Vector3.up);
                yield return new WaitForSeconds(0.35f);
                ScreenCapture.CaptureScreenshot(Path.Combine(outputDirectory, $"outfit-{viewNames[index]}.png"));
                yield return new WaitForSeconds(0.55f);
            }

            Transform root = player.transform;
            bool requiredPieces = IsActive(root, "Long Kurta Body")
                && IsActive(root, "Long Kurta Hem")
                && IsActive(root, "Party Stole Left")
                && IsActive(root, "Party Stole Right")
                && IsActive(root, "Kolhapuri Left")
                && IsActive(root, "Kolhapuri Right");
            bool oldAccessoriesHidden = !IsActive(root, "Shoulder Bag")
                && !IsActive(root, "Shoulder Bag Strap")
                && !IsActive(root, "Belt")
                && !IsActive(root, "Shoe Left")
                && !IsActive(root, "Shoe Right");
            Transform stoleLeft = root.Find("Party Stole Left");
            Transform stoleRight = root.Find("Party Stole Right");
            bool mirroredStole = stoleLeft != null && stoleRight != null
                && Mathf.Abs(stoleLeft.localPosition.x + stoleRight.localPosition.x) < 0.01f
                && Vector3.Distance(
                    new Vector3(0f, stoleLeft.localPosition.y, stoleLeft.localPosition.z),
                    new Vector3(0f, stoleRight.localPosition.y, stoleRight.localPosition.z)) < 0.01f;
            bool passed = requiredPieces && oldAccessoriesHidden && mirroredStole;
            Debug.Log(passed
                ? "OUTFIT_SMOKE_PASSED: front/left/back/right captured; kurta, stole and kolhapuri pieces are symmetric."
                : $"OUTFIT_SMOKE_FAILED: pieces={requiredPieces}, oldHidden={oldAccessoriesHidden}, mirrored={mirroredStole}.");
            Application.Quit(passed ? 0 : 4);
        }

        private static bool IsActive(Transform root, string childName)
        {
            Transform child = root != null ? root.Find(childName) : null;
            return child != null && child.gameObject.activeSelf;
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
                    && System.Array.IndexOf(arguments, "-riskyStateReformSmoke") >= 0)
                || (chapterNumber == 18
                    && System.Array.IndexOf(arguments, "-riskyNationalExpansionSmoke") >= 0)
                || (chapterNumber == 19
                    && System.Array.IndexOf(arguments, "-riskyFirstNationalElectionSmoke") >= 0)
                || (chapterNumber == 20
                    && System.Array.IndexOf(arguments, "-riskyOppositionTermSmoke") >= 0)
                || (chapterNumber == 21
                    && System.Array.IndexOf(arguments, "-riskySecondNationalElectionSmoke") >= 0)
                || (chapterNumber == 22
                    && System.Array.IndexOf(arguments, "-riskyPrimeMinisterGovernanceSmoke") >= 0)
                || (chapterNumber == 23
                    && System.Array.IndexOf(arguments, "-riskyNationalDevelopmentSmoke") >= 0)
                || (chapterNumber == 24
                    && System.Array.IndexOf(arguments, "-riskyGlobalLeadershipSmoke") >= 0);

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
            else if (chapterNumber == 17)
            {
                PrepareChapterSeventeenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 18)
            {
                PrepareChapterEighteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 19)
            {
                PrepareChapterNineteenBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 20)
            {
                PrepareChapterTwentyBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 21)
            {
                PrepareChapterTwentyOneBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 22)
            {
                PrepareChapterTwentyTwoBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else if (chapterNumber == 23)
            {
                PrepareChapterTwentyThreeBaseline(GameSession.Instance);
                mission.ResetMission(false);
            }
            else
            {
                PrepareChapterTwentyFourBaseline(GameSession.Instance);
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
            else if (chapterNumber == 18 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 90;
                requiredProof = 96;
            }
            else if (chapterNumber == 19 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 92;
                requiredProof = 94;
            }
            else if (chapterNumber == 20 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 90;
                requiredProof = 95;
            }
            else if (chapterNumber == 21 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 92;
                requiredProof = 96;
            }
            else if (chapterNumber == 22 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 92;
                requiredProof = 94;
            }
            else if (chapterNumber == 23 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 90;
                requiredProof = 96;
            }
            else if (chapterNumber == 24 && riskyDecision)
            {
                requiredTrust = 100;
                requiredMoney = 0;
                requiredReputation = 100;
                requiredProof = 100;
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
            else if (chapterNumber == 18 && riskyDecision)
            {
                requiredPower = 75;
                requiredVolunteers = 369;
                requiredPressure = 96;
            }
            else if (chapterNumber == 19 && riskyDecision)
            {
                requiredPower = 77;
                requiredVolunteers = 434;
                requiredPressure = 100;
            }
            else if (chapterNumber == 20 && riskyDecision)
            {
                requiredPower = 80;
                requiredVolunteers = 486;
                requiredPressure = 97;
            }
            else if (chapterNumber == 21 && riskyDecision)
            {
                requiredPower = 83;
                requiredVolunteers = 530;
                requiredPressure = 96;
            }
            else if (chapterNumber == 22 && riskyDecision)
            {
                requiredPower = 86;
                requiredVolunteers = 588;
                requiredPressure = 94;
            }
            else if (chapterNumber == 23 && riskyDecision)
            {
                requiredPower = 89;
                requiredVolunteers = 668;
                requiredPressure = 92;
            }
            else if (chapterNumber == 24 && riskyDecision)
            {
                requiredPower = 93;
                requiredVolunteers = 744;
                requiredPressure = 83;
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
            int requiredNationalOrganizationReach = chapterNumber == 18 && riskyDecision ? 100 : expectedNationalOrganizationReach;
            int requiredFederalAllianceTrust = chapterNumber == 18 && riskyDecision ? 74 : expectedFederalAllianceTrust;
            int requiredNationalPolicyCredibility = chapterNumber == 18 && riskyDecision ? 76 : expectedNationalPolicyCredibility;
            int requiredNationalReadinessScore = chapterNumber == 18 && riskyDecision ? 78 : expectedNationalReadinessScore;
            int requiredNationalRegionsAligned = chapterNumber == 18 && riskyDecision ? 14 : expectedNationalRegionsAligned;
            bool requiredNationalExpansionReady = chapterNumber == 18 && riskyDecision || expectedNationalExpansionReady;
            int requiredNationalCampaignSupport = chapterNumber == 19 && riskyDecision ? 100 : expectedNationalCampaignSupport;
            int requiredNationalCampaignCompliance = chapterNumber == 19 && riskyDecision ? 82 : expectedNationalCampaignCompliance;
            int requiredNationalElectionOperations = chapterNumber == 19 && riskyDecision ? 98 : expectedNationalElectionOperations;
            int requiredFirstNationalVoteShare = chapterNumber == 19 && riskyDecision ? 49 : expectedFirstNationalVoteShare;
            int requiredFirstNationalSeatsWon = chapterNumber == 19 && riskyDecision ? 42 : expectedFirstNationalSeatsWon;
            bool requiredFirstNationalElectionContested = chapterNumber == 19 && riskyDecision || expectedFirstNationalElectionContested;
            bool requiredFirstNationalElectionWon = chapterNumber == 19 && riskyDecision ? false : expectedFirstNationalElectionWon;
            int requiredOppositionServiceRecord = chapterNumber == 20 && riskyDecision ? 100 : expectedOppositionServiceRecord;
            int requiredNationalAllianceRenewal = chapterNumber == 20 && riskyDecision ? 70 : expectedNationalAllianceRenewal;
            int requiredNationalPolicyCorrection = chapterNumber == 20 && riskyDecision ? 74 : expectedNationalPolicyCorrection;
            int requiredNationalComebackScore = chapterNumber == 20 && riskyDecision ? 75 : expectedNationalComebackScore;
            bool requiredNationalComebackReady = chapterNumber == 20 && riskyDecision || expectedNationalComebackReady;
            int requiredSecondNationalCampaignSupport = chapterNumber == 21 && riskyDecision ? 88 : expectedSecondNationalCampaignSupport;
            int requiredSecondNationalCampaignCompliance = chapterNumber == 21 && riskyDecision ? 64 : expectedSecondNationalCampaignCompliance;
            int requiredSecondNationalElectionOperations = chapterNumber == 21 && riskyDecision ? 64 : expectedSecondNationalElectionOperations;
            int requiredSecondNationalVoteShare = chapterNumber == 21 && riskyDecision ? 52 : expectedSecondNationalVoteShare;
            int requiredSecondNationalSeatsWon = chapterNumber == 21 && riskyDecision ? 51 : expectedSecondNationalSeatsWon;
            bool requiredSecondNationalElectionContested = chapterNumber == 21 && riskyDecision || expectedSecondNationalElectionContested;
            bool requiredPrimeMinisterElected = chapterNumber == 21 && riskyDecision || expectedPrimeMinisterElected;
            int requiredPrimeMinisterDelivery = chapterNumber == 22 && riskyDecision ? 96 : expectedPrimeMinisterDelivery;
            int requiredUnionCabinetIntegrity = chapterNumber == 22 && riskyDecision ? 68 : expectedUnionCabinetIntegrity;
            int requiredNationalFiscalDiscipline = chapterNumber == 22 && riskyDecision ? 70 : expectedNationalFiscalDiscipline;
            int requiredInstitutionalTrust = chapterNumber == 22 && riskyDecision ? 72 : expectedInstitutionalTrust;
            int requiredPrimeMinisterHundredDayScore = chapterNumber == 22 && riskyDecision ? 75 : expectedPrimeMinisterHundredDayScore;
            bool requiredPrimeMinisterHundredDayReviewPassed = chapterNumber == 22 && riskyDecision || expectedPrimeMinisterHundredDayReviewPassed;
            int requiredNationalHealthIndex = chapterNumber == 23 && riskyDecision ? 100 : expectedNationalHealthIndex;
            int requiredNationalLearningIndex = chapterNumber == 23 && riskyDecision ? 76 : expectedNationalLearningIndex;
            int requiredNationalSafetyJusticeIndex = chapterNumber == 23 && riskyDecision ? 70 : expectedNationalSafetyJusticeIndex;
            int requiredNationalLivelihoodIndex = chapterNumber == 23 && riskyDecision ? 100 : expectedNationalLivelihoodIndex;
            int requiredNationalDevelopmentScore = chapterNumber == 23 && riskyDecision ? 82 : expectedNationalDevelopmentScore;
            bool requiredNationalDevelopmentReviewPassed = chapterNumber == 23 && riskyDecision || expectedNationalDevelopmentReviewPassed;
            int requiredGlobalTradeTrust = chapterNumber == 24 && riskyDecision ? 100 : expectedGlobalTradeTrust;
            int requiredScienceInnovationLeadership = chapterNumber == 24 && riskyDecision ? 84 : expectedScienceInnovationLeadership;
            int requiredPeaceDefenseReadiness = chapterNumber == 24 && riskyDecision ? 74 : expectedPeaceDefenseReadiness;
            int requiredHumanitarianClimateLeadership = chapterNumber == 24 && riskyDecision ? 88 : expectedHumanitarianClimateLeadership;
            int requiredGlobalLeadershipScore = chapterNumber == 24 && riskyDecision ? 87 : expectedGlobalLeadershipScore;
            bool requiredVishwaGuruOutcomeEarned = chapterNumber == 24 && riskyDecision ? false : expectedVishwaGuruOutcomeEarned;
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
                && progress.nationalOrganizationReach == requiredNationalOrganizationReach
                && progress.federalAllianceTrust == requiredFederalAllianceTrust
                && progress.nationalPolicyCredibility == requiredNationalPolicyCredibility
                && progress.nationalReadinessScore == requiredNationalReadinessScore
                && progress.nationalRegionsAligned == requiredNationalRegionsAligned
                && progress.nationalExpansionReady == requiredNationalExpansionReady
                && progress.nationalCampaignSupport == requiredNationalCampaignSupport
                && progress.nationalCampaignCompliance == requiredNationalCampaignCompliance
                && progress.nationalElectionOperations == requiredNationalElectionOperations
                && progress.firstNationalVoteShare == requiredFirstNationalVoteShare
                && progress.firstNationalSeatsWon == requiredFirstNationalSeatsWon
                && progress.firstNationalElectionContested == requiredFirstNationalElectionContested
                && progress.firstNationalElectionWon == requiredFirstNationalElectionWon
                && progress.oppositionServiceRecord == requiredOppositionServiceRecord
                && progress.nationalAllianceRenewal == requiredNationalAllianceRenewal
                && progress.nationalPolicyCorrection == requiredNationalPolicyCorrection
                && progress.nationalComebackScore == requiredNationalComebackScore
                && progress.nationalComebackReady == requiredNationalComebackReady
                && progress.secondNationalCampaignSupport == requiredSecondNationalCampaignSupport
                && progress.secondNationalCampaignCompliance == requiredSecondNationalCampaignCompliance
                && progress.secondNationalElectionOperations == requiredSecondNationalElectionOperations
                && progress.secondNationalVoteShare == requiredSecondNationalVoteShare
                && progress.secondNationalSeatsWon == requiredSecondNationalSeatsWon
                && progress.secondNationalElectionContested == requiredSecondNationalElectionContested
                && progress.primeMinisterElected == requiredPrimeMinisterElected
                && progress.primeMinisterDelivery == requiredPrimeMinisterDelivery
                && progress.unionCabinetIntegrity == requiredUnionCabinetIntegrity
                && progress.nationalFiscalDiscipline == requiredNationalFiscalDiscipline
                && progress.institutionalTrust == requiredInstitutionalTrust
                && progress.primeMinisterHundredDayScore == requiredPrimeMinisterHundredDayScore
                && progress.primeMinisterHundredDayReviewPassed == requiredPrimeMinisterHundredDayReviewPassed
                && progress.nationalHealthIndex == requiredNationalHealthIndex
                && progress.nationalLearningIndex == requiredNationalLearningIndex
                && progress.nationalSafetyJusticeIndex == requiredNationalSafetyJusticeIndex
                && progress.nationalLivelihoodIndex == requiredNationalLivelihoodIndex
                && progress.nationalDevelopmentScore == requiredNationalDevelopmentScore
                && progress.nationalDevelopmentReviewPassed == requiredNationalDevelopmentReviewPassed
                && progress.globalTradeTrust == requiredGlobalTradeTrust
                && progress.scienceInnovationLeadership == requiredScienceInnovationLeadership
                && progress.peaceDefenseReadiness == requiredPeaceDefenseReadiness
                && progress.humanitarianClimateLeadership == requiredHumanitarianClimateLeadership
                && progress.globalLeadershipScore == requiredGlobalLeadershipScore
                && progress.vishwaGuruOutcomeEarned == requiredVishwaGuruOutcomeEarned
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
                && (chapterNumber != 18 || progress.nationalExpansionApproach == requiredDecision)
                && (chapterNumber != 19 || progress.firstNationalCampaignApproach == requiredDecision)
                && (chapterNumber != 20 || progress.oppositionTermApproach == requiredDecision)
                && (chapterNumber != 21 || progress.secondNationalCampaignApproach == requiredDecision)
                && (chapterNumber != 22 || progress.primeMinisterGovernanceApproach == requiredDecision)
                && (chapterNumber != 23 || progress.nationalDevelopmentApproach == requiredDecision)
                && (chapterNumber != 24 || progress.globalLeadershipApproach == requiredDecision)
                && (PrototypeHud.Instance == null || !PrototypeHud.Instance.IsDecisionOpen);
            string marker = chapterNumber == 1
                ? "PROTOTYPE"
                : riskyDecision ? $"CHAPTER_{chapterNumber}_RISKY" : $"CHAPTER_{chapterNumber}";
            Debug.Log(passed
                ? $"{marker}_SMOKE_PASSED: trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, mla={progress.mlaPerformanceScore}, districtReach={progress.districtReach}, quality={progress.candidateQuality}, discipline={progress.organizationDiscipline}, expansion={progress.districtExpansionScore}, stateReach={progress.stateCampaignReach}, slate={progress.candidateSlateIntegrity}, ops={progress.stateElectionOperations}, stateScore={progress.stateExpansionScore}, seats={progress.stateSeatsWon}, policy={progress.statePolicyCredibility}, caucus={progress.stateCaucusUnity}, publicLead={progress.publicLeadership}, leadership={progress.stateLeadershipScore}, statewideSupport={progress.statewideSupport}, rules={progress.statewideCampaignCompliance}, pollOps={progress.statewideElectionOperations}, statewideVote={progress.statewideVoteShare}, assemblySeats={progress.stateAssemblySeatsWon}, cm={progress.chiefMinisterElected}, cmDelivery={progress.chiefMinisterDelivery}, cabinet={progress.cabinetIntegrity}, fiscal={progress.stateFiscalDiscipline}, cmScore={progress.chiefMinisterGovernanceScore}, cmReview={progress.chiefMinisterHundredDayReviewPassed}, health={progress.stateHealthOutcome}, learning={progress.stateLearningOutcome}, safety={progress.stateSafetyOutcome}, livelihood={progress.stateLivelihoodOutcome}, termScore={progress.stateTermScore}, termReview={progress.stateTermReviewPassed}, nationalReach={progress.nationalOrganizationReach}, alliance={progress.federalAllianceTrust}, nationalPolicy={progress.nationalPolicyCredibility}, nationalScore={progress.nationalReadinessScore}, regions={progress.nationalRegionsAligned}, nationalReady={progress.nationalExpansionReady}, nationalSupport={progress.nationalCampaignSupport}, nationalRules={progress.nationalCampaignCompliance}, nationalOps={progress.nationalElectionOperations}, firstNationalVote={progress.firstNationalVoteShare}, firstNationalSeats={progress.firstNationalSeatsWon}, contested={progress.firstNationalElectionContested}, firstNationalWin={progress.firstNationalElectionWon}, oppositionService={progress.oppositionServiceRecord}, allianceRenewal={progress.nationalAllianceRenewal}, policyCorrection={progress.nationalPolicyCorrection}, comebackScore={progress.nationalComebackScore}, comebackReady={progress.nationalComebackReady}, secondSupport={progress.secondNationalCampaignSupport}, secondRules={progress.secondNationalCampaignCompliance}, secondOps={progress.secondNationalElectionOperations}, secondVote={progress.secondNationalVoteShare}, secondSeats={progress.secondNationalSeatsWon}, secondContested={progress.secondNationalElectionContested}, pm={progress.primeMinisterElected}, pmDelivery={progress.primeMinisterDelivery}, unionCabinet={progress.unionCabinetIntegrity}, nationalFiscal={progress.nationalFiscalDiscipline}, institutions={progress.institutionalTrust}, pmScore={progress.primeMinisterHundredDayScore}, pmReview={progress.primeMinisterHundredDayReviewPassed}, devHealth={progress.nationalHealthIndex}, devLearning={progress.nationalLearningIndex}, devSafety={progress.nationalSafetyJusticeIndex}, devLivelihood={progress.nationalLivelihoodIndex}, devScore={progress.nationalDevelopmentScore}, devReview={progress.nationalDevelopmentReviewPassed}, globalTrade={progress.globalTradeTrust}, globalScience={progress.scienceInnovationLeadership}, globalPeace={progress.peaceDefenseReadiness}, globalHumanitarian={progress.humanitarianClimateLeadership}, globalScore={progress.globalLeadershipScore}, vishwaGuru={progress.vishwaGuruOutcomeEarned}"
                : $"{marker}_SMOKE_FAILED: complete={mission.IsComplete}, trust={progress.publicTrust}, money={progress.money}, reputation={progress.reputation}, proof={progress.caseProof}, power={progress.politicalPower}, team={progress.volunteers}, pressure={progress.oppositionPressure}, vote={progress.assemblyVoteShare}, legislative={progress.legislativeEffectiveness}, service={progress.constituencyService}, ethics={progress.ethicsRecord}, allocation={progress.mlaAllocationLakhs}, mla={progress.mlaPerformanceScore}, districtReach={progress.districtReach}, quality={progress.candidateQuality}, discipline={progress.organizationDiscipline}, expansion={progress.districtExpansionScore}, stateReach={progress.stateCampaignReach}, slate={progress.candidateSlateIntegrity}, ops={progress.stateElectionOperations}, stateScore={progress.stateExpansionScore}, seats={progress.stateSeatsWon}, policy={progress.statePolicyCredibility}, caucus={progress.stateCaucusUnity}, publicLead={progress.publicLeadership}, leadership={progress.stateLeadershipScore}, statewideSupport={progress.statewideSupport}, rules={progress.statewideCampaignCompliance}, pollOps={progress.statewideElectionOperations}, statewideVote={progress.statewideVoteShare}, assemblySeats={progress.stateAssemblySeatsWon}, cm={progress.chiefMinisterElected}, cmDelivery={progress.chiefMinisterDelivery}, cabinet={progress.cabinetIntegrity}, fiscal={progress.stateFiscalDiscipline}, cmScore={progress.chiefMinisterGovernanceScore}, cmReview={progress.chiefMinisterHundredDayReviewPassed}, health={progress.stateHealthOutcome}, learning={progress.stateLearningOutcome}, safety={progress.stateSafetyOutcome}, livelihood={progress.stateLivelihoodOutcome}, termScore={progress.stateTermScore}, termReview={progress.stateTermReviewPassed}, nationalReach={progress.nationalOrganizationReach}, alliance={progress.federalAllianceTrust}, nationalPolicy={progress.nationalPolicyCredibility}, nationalScore={progress.nationalReadinessScore}, regions={progress.nationalRegionsAligned}, nationalReady={progress.nationalExpansionReady}, nationalSupport={progress.nationalCampaignSupport}, nationalRules={progress.nationalCampaignCompliance}, nationalOps={progress.nationalElectionOperations}, firstNationalVote={progress.firstNationalVoteShare}, firstNationalSeats={progress.firstNationalSeatsWon}, contested={progress.firstNationalElectionContested}, firstNationalWin={progress.firstNationalElectionWon}, oppositionService={progress.oppositionServiceRecord}, allianceRenewal={progress.nationalAllianceRenewal}, policyCorrection={progress.nationalPolicyCorrection}, comebackScore={progress.nationalComebackScore}, comebackReady={progress.nationalComebackReady}, secondSupport={progress.secondNationalCampaignSupport}, secondRules={progress.secondNationalCampaignCompliance}, secondOps={progress.secondNationalElectionOperations}, secondVote={progress.secondNationalVoteShare}, secondSeats={progress.secondNationalSeatsWon}, secondContested={progress.secondNationalElectionContested}, pm={progress.primeMinisterElected}");
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

        private static void PrepareChapterEighteenBaseline(GameSession session)
        {
            PrepareChapterSeventeenBaseline(session);
            session.ApplyReward(0, -10, 0, 0);
            session.ApplyPoliticalReward(3, 63, 6);
            session.ApplyStateTermReward(93, 87, 92, 85);
            session.ResolveStateTermReview();
            session.SetStoryDecision("state-reform-approach", 1);
            session.CompleteChapter(17);
        }

        private static void PrepareChapterNineteenBaseline(GameSession session)
        {
            PrepareChapterEighteenBaseline(session);
            session.ApplyPoliticalReward(4, 50, -4);
            session.ApplyNationalExpansionReward(96, 100, 96);
            session.ResolveNationalExpansion();
            session.SetStoryDecision("national-expansion-approach", 1);
            session.CompleteChapter(18);
        }

        private static void PrepareChapterTwentyBaseline(GameSession session)
        {
            PrepareChapterNineteenBaseline(session);
            session.ApplyPoliticalReward(3, 64, 8);
            session.ApplyFirstNationalCampaignReward(84, 100, 92);
            session.ResolveFirstNationalElection();
            session.SetStoryDecision("first-national-campaign-approach", 1);
            session.CompleteChapter(19);
        }

        private static void PrepareChapterTwentyOneBaseline(GameSession session)
        {
            PrepareChapterTwentyBaseline(session);
            session.ApplyReward(0, 0, 0, 6);
            session.ApplyPoliticalReward(3, 50, -5);
            session.ApplyOppositionTermReward(94, 92, 92);
            session.ResolveNationalComeback();
            session.SetStoryDecision("opposition-term-approach", 1);
            session.CompleteChapter(20);
        }

        private static void PrepareChapterTwentyTwoBaseline(GameSession session)
        {
            PrepareChapterTwentyOneBaseline(session);
            session.ApplyPoliticalReward(3, 44, -3);
            session.ApplySecondNationalCampaignReward(76, 98, 76);
            session.ResolveSecondNationalElection();
            session.SetStoryDecision("second-national-campaign-approach", 1);
            session.CompleteChapter(21);
        }

        private static void PrepareChapterTwentyThreeBaseline(GameSession session)
        {
            PrepareChapterTwentyTwoBaseline(session);
            session.ApplyPoliticalReward(3, 56, -5);
            session.ApplyPrimeMinisterGovernanceReward(90, 96, 84, 94);
            session.ResolvePrimeMinisterHundredDayReview();
            session.SetStoryDecision("pm-governance-approach", 1);
            session.CompleteChapter(22);
        }

        private static void PrepareChapterTwentyFourBaseline(GameSession session)
        {
            PrepareChapterTwentyThreeBaseline(session);
            session.ApplyPoliticalReward(4, 80, -2);
            session.ApplyNationalDevelopmentReward(93, 95, 92, 90);
            session.ResolveNationalDevelopmentReview();
            session.SetStoryDecision("national-development-approach", 1);
            session.CompleteChapter(23);
        }

        private static string ReadArgument(string[] arguments, string name)
        {
            int index = System.Array.IndexOf(arguments, name);
            return index >= 0 && index + 1 < arguments.Length ? arguments[index + 1] : null;
        }
    }
}
