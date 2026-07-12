using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeAudio : MonoBehaviour
    {
        private const int SampleRate = 22050;
        public static PrototypeAudio Instance { get; private set; }

        private AudioSource ambienceSource;
        private AudioSource feedbackSource;
        private AudioClip interactionClip;
        private AudioClip milestoneClip;
        private AudioClip completionClip;
        private AudioClip footstepClip;

        private void Awake()
        {
            Instance = this;
            ambienceSource = gameObject.AddComponent<AudioSource>();
            feedbackSource = gameObject.AddComponent<AudioSource>();
            ambienceSource.loop = true;
            ambienceSource.volume = 0.16f;
            ambienceSource.spatialBlend = 0f;
            feedbackSource.volume = 0.48f;
            feedbackSource.spatialBlend = 0f;

            ambienceSource.clip = CreateAmbience();
            interactionClip = CreateTone("Seva Confirm", 0.16f, 520f, 720f, 0.18f);
            milestoneClip = CreateTone("Mission Update", 0.34f, 410f, 690f, 0.22f);
            completionClip = CreateTone("Chapter Complete", 0.62f, 330f, 880f, 0.25f);
            footstepClip = CreateFootstep();
        }

        private void Start()
        {
            ambienceSource.Play();
        }

        public void PlayInteraction()
        {
            feedbackSource.PlayOneShot(interactionClip, 0.68f);
        }

        public void PlayMilestone()
        {
            feedbackSource.PlayOneShot(milestoneClip, 0.82f);
        }

        public void PlayCompletion()
        {
            feedbackSource.PlayOneShot(completionClip, 1f);
        }

        public void PlayFootstep(bool running)
        {
            feedbackSource.pitch = running ? 1.12f : 0.94f;
            feedbackSource.PlayOneShot(footstepClip, running ? 0.34f : 0.25f);
            feedbackSource.pitch = 1f;
        }

        private static AudioClip CreateAmbience()
        {
            int sampleCount = SampleRate * 4;
            float[] samples = new float[sampleCount];
            uint state = 2463534242;
            float smoothedNoise = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                state ^= state << 13;
                state ^= state >> 17;
                state ^= state << 5;
                float noise = (state / (float)uint.MaxValue) * 2f - 1f;
                smoothedNoise = Mathf.Lerp(smoothedNoise, noise, 0.018f);
                float river = Mathf.Sin(i * Mathf.PI * 2f * 0.31f / SampleRate) * 0.035f;
                samples[i] = smoothedNoise * 0.12f + river;
            }

            AudioClip clip = AudioClip.Create("Ghat Ambience", sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private static AudioClip CreateTone(string name, float duration, float startFrequency, float endFrequency, float volume)
        {
            int sampleCount = Mathf.CeilToInt(SampleRate * duration);
            float[] samples = new float[sampleCount];
            float phase = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                float progress = i / (float)sampleCount;
                float frequency = Mathf.Lerp(startFrequency, endFrequency, progress);
                phase += Mathf.PI * 2f * frequency / SampleRate;
                float envelope = Mathf.Sin(progress * Mathf.PI) * (1f - progress * 0.35f);
                samples[i] = Mathf.Sin(phase) * envelope * volume;
            }

            AudioClip clip = AudioClip.Create(name, sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private static AudioClip CreateFootstep()
        {
            int sampleCount = Mathf.CeilToInt(SampleRate * 0.11f);
            float[] samples = new float[sampleCount];
            uint state = 123456789;
            for (int i = 0; i < sampleCount; i++)
            {
                state = state * 1664525 + 1013904223;
                float noise = (state / (float)uint.MaxValue) * 2f - 1f;
                float progress = i / (float)sampleCount;
                samples[i] = noise * Mathf.Pow(1f - progress, 3f) * 0.25f;
            }

            AudioClip clip = AudioClip.Create("Stone Footstep", sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}
