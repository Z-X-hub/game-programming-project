using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(ParticleSystem))]
public class StaticStarfield : MonoBehaviour
{
    public int starCount = 520;
    public float innerRadius = 34f;
    public float outerRadius = 72f;
    public float starSize = 0.18f;
    public int seed = 2026;

    private ParticleSystem starParticles;

    private void Awake()
    {
        starParticles = GetComponent<ParticleSystem>();
        BuildStars();
    }

    private void OnEnable()
    {
        starParticles = GetComponent<ParticleSystem>();
        BuildStars();
    }

    private void BuildStars()
    {
        if (starParticles == null)
        {
            return;
        }

        ParticleSystem.MainModule main = starParticles.main;
        main.loop = false;
        main.playOnAwake = false;
        main.maxParticles = starCount;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        ParticleSystem.EmissionModule emission = starParticles.emission;
        emission.enabled = false;

        Random.InitState(seed);
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[starCount];

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            if (direction.y < -0.35f)
            {
                direction.y = Mathf.Abs(direction.y);
            }

            float radius = Random.Range(innerRadius, outerRadius);
            Color color = Color.Lerp(new Color(0.55f, 0.72f, 1f, 1f), Color.white, Random.value);

            particles[i].position = direction.normalized * radius;
            particles[i].startColor = color;
            particles[i].startSize = starSize * Random.Range(0.45f, 1.4f);
            particles[i].remainingLifetime = 999999f;
            particles[i].startLifetime = 999999f;
        }

        starParticles.SetParticles(particles, particles.Length);
        starParticles.Pause();
    }
}
