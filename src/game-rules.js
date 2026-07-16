export const WIN_RULES = Object.freeze({
  demo: Object.freeze({
    minWinSeconds: 16,
    influenceTarget: 55,
    scoreTarget: 59,
    timeoutInfluence: 42,
    timeoutScore: 54
  }),
  standard: Object.freeze({
    minWinSeconds: 22,
    influenceTarget: 62,
    scoreTarget: 65,
    timeoutInfluence: 48,
    timeoutScore: 58
  })
});

export function getWinRules(demoMode) {
  return demoMode ? WIN_RULES.demo : WIN_RULES.standard;
}

export function evaluateMandate({ demoMode, roundElapsed, influence, score, timeLeft }) {
  const rules = getWinRules(demoMode);
  if (
    roundElapsed >= rules.minWinSeconds &&
    influence >= rules.influenceTarget &&
    score >= rules.scoreTarget
  ) {
    return { won: true, reason: "District mandate reached." };
  }
  if (timeLeft <= 0) {
    return {
      won: influence >= rules.timeoutInfluence && score >= rules.timeoutScore,
      reason: "The campaign clock ended."
    };
  }
  return null;
}
