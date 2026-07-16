using UnityEngine;

namespace NetaJi.Prototype
{
    public sealed class PrototypeAudio : MonoBehaviour
    {
        private const int SampleRate = 22050;
        public static PrototypeAudio Instance { get; private set; }

        [SerializeField] private bool locationAwareOpenWorld;
        [SerializeField] private Transform ambienceTarget;

        private AudioSource ambienceSource;
        private AudioSource feedbackSource;
        private AudioSource riverSource;
        private AudioSource marketSource;
        private AudioSource trafficSource;
        private AudioSource worldFeedbackSource;
        private AudioClip interactionClip;
        private AudioClip milestoneClip;
        private AudioClip completionClip;
        private AudioClip footstepClip;
        private AudioClip hornClip;
        private float nextHornAt;

        public bool IsLocationAware => locationAwareOpenWorld && ambienceTarget != null;
        public float RiverMix { get; private set; }
        public float MarketMix { get; private set; }
        public float TrafficMix { get; private set; }

        public void ConfigureOpenWorld(Transform target)
        {
            locationAwareOpenWorld = true;
            ambienceTarget = target;
        }

        private void Awake()
        {
            Instance = this;
            ambienceSource = gameObject.AddComponent<AudioSource>();
            feedbackSource = gameObject.AddComponent<AudioSource>();
            ambienceSource.loop = true;
            ambienceSource.volume = locationAwareOpenWorld ? 0.055f : 0.16f;
            ambienceSource.spatialBlend = 0f;
            feedbackSource.volume = 0.48f;
            feedbackSource.spatialBlend = 0f;

            ambienceSource.clip = CreateAmbience();
            interactionClip = CreateTone("Seva Confirm", 0.16f, 520f, 720f, 0.18f);
            milestoneClip = CreateTone("Mission Update", 0.34f, 410f, 690f, 0.22f);
            completionClip = CreateTone("Chapter Complete", 0.62f, 330f, 880f, 0.25f);
            footstepClip = CreateFootstep();

            if (locationAwareOpenWorld)
            {
                riverSource = CreateLoopSource(CreateRiverLayer(), 0f);
                marketSource = CreateLoopSource(CreateMarketLayer(), 0f);
                trafficSource = CreateLoopSource(CreateTrafficLayer(), 0f);
                worldFeedbackSource = gameObject.AddComponent<AudioSource>();
                worldFeedbackSource.volume = 0.22f;
                worldFeedbackSource.spatialBlend = 0f;
                hornClip = CreateTone("Distant City Horn", 0.24f, 360f, 300f, 0.14f);
                nextHornAt = 8f;
            }
        }

        private void Start()
        {
            ambienceSource.Play();
            riverSource?.Play();
            marketSource?.Play();
            trafficSource?.Play();
        }

        private void Update()
        {
            if (!IsLocationAware)
            {
                return;
            }

            Vector3 position = ambienceTarget.position;
            RiverMix = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(58f, 158f, position.x));
            float marketDistance = Vector2.Distance(
                new Vector2(position.x, position.z),
                new Vector2(30f, -72f));
            MarketMix = 1f - Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(18f, 105f, marketDistance));
            float nearestRoad = Mathf.Min(
                Mathf.Abs(position.z),
                Mathf.Abs(position.x),
                Mathf.Abs(position.z - 101f),
                Mathf.Abs(position.z + 125f),
                Mathf.Abs(position.x + 111f),
                Mathf.Abs(position.x - 79f),
                Mathf.Abs(position.x - 126f));
            TrafficMix = 1f - Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(3f, 34f, nearestRoad));

            riverSource.volume = Mathf.Lerp(0.012f, 0.19f, RiverMix);
            marketSource.volume = Mathf.Lerp(0.006f, 0.13f, MarketMix);
            trafficSource.volume = Mathf.Lerp(0.012f, 0.10f, TrafficMix);

            if (worldFeedbackSource != null && hornClip != null && Time.time >= nextHornAt)
            {
                if (TrafficMix > 0.32f)
                {
                    worldFeedbackSource.pitch = 0.92f + Mathf.PingPong(Time.time * 0.05f, 0.16f);
                    worldFeedbackSource.PlayOneShot(hornClip, Mathf.Lerp(0.08f, 0.22f, TrafficMix));
                }
                nextHornAt = Time.time + Mathf.Lerp(13f, 7f, TrafficMix);
            }
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

        private AudioSource CreateLoopSource(AudioClip clip, float volume)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = true;
            source.volume = volume;
            source.spatialBlend = 0f;
            return source;
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

        private static AudioClip CreateRiverLayer()
        {
            int sampleCount = SampleRate * 5;
            float[] samples = new float[sampleCount];
            uint state = 327184219;
            float wash = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                state = state * 1664525 + 1013904223;
                float noise = (state / (float)uint.MaxValue) * 2f - 1f;
                wash = Mathf.Lerp(wash, noise, 0.010f);
                float time = i / (float)SampleRate;
                float wave = Mathf.Sin(time * Mathf.PI * 2f * 0.21f)
                    + Mathf.Sin(time * Mathf.PI * 2f * 0.37f) * 0.45f;
                samples[i] = wash * 0.16f + wave * 0.025f;
            }

            AudioClip clip = AudioClip.Create("Clean Ganga River Layer", sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private static AudioClip CreateMarketLayer()
        {
            int sampleCount = SampleRate * 4;
            float[] samples = new float[sampleCount];
            uint state = 918273645;
            float murmur = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                state ^= state << 13;
                state ^= state >> 17;
                state ^= state << 5;
                float noise = (state / (float)uint.MaxValue) * 2f - 1f;
                murmur = Mathf.Lerp(murmur, noise, 0.055f);
                float time = i / (float)SampleRate;
                float voices = Mathf.Sin(time * Mathf.PI * 2f * 132f) * 0.014f
                    + Mathf.Sin(time * Mathf.PI * 2f * 177f) * 0.010f;
                float pulse = 0.55f + Mathf.Sin(time * Mathf.PI * 2f * 0.42f) * 0.22f;
                samples[i] = (murmur * 0.09f + voices) * pulse;
            }

            AudioClip clip = AudioClip.Create("Loknath Market Murmur", sampleCount, 1, SampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private static AudioClip CreateTrafficLayer()
        {
            int sampleCount = SampleRate * 4;
            float[] samples = new float[sampleCount];
            uint state = 192837465;
            float rumble = 0f;
            for (int i = 0; i < sampleCount; i++)
            {
                state = state * 1103515245 + 12345;
                float noise = (state / (float)uint.MaxValue) * 2f - 1f;
                rumble = Mathf.Lerp(rumble, noise, 0.004f);
                float time = i / (float)SampleRate;
                float engine = Mathf.Sin(time * Mathf.PI * 2f * 47f) * 0.030f
                    + Mathf.Sin(time * Mathf.PI * 2f * 71f) * 0.018f;
                samples[i] = rumble * 0.15f + engine;
            }

            AudioClip clip = AudioClip.Create("Prayagraj Traffic Bed", sampleCount, 1, SampleRate, false);
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
