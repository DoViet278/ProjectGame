using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component để tạo hiệu ứng particles bay quanh loading bar
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class LoadingParticles : MonoBehaviour
{
    [Header("Particle Settings")]
    [SerializeField] private Color startColor = new Color(0.3f, 0.8f, 1f, 1f);
    [SerializeField] private Color endColor = new Color(0.3f, 0.8f, 1f, 0f);
    [SerializeField] private float emissionRate = 10f;
    [SerializeField] private Vector2 sizeRange = new Vector2(0.5f, 2f);
    [SerializeField] private Vector2 lifetimeRange = new Vector2(1f, 2f);

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ColorOverLifetimeModule colorModule;

    private void Awake()
    {
        SetupParticleSystem();
    }

    private void SetupParticleSystem()
    {
        particleSystem = GetComponent<ParticleSystem>();
        
        // Main module
        mainModule = particleSystem.main;
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(lifetimeRange.x, lifetimeRange.y);
        mainModule.startSize = new ParticleSystem.MinMaxCurve(sizeRange.x, sizeRange.y);
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(1f, 3f);
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        mainModule.maxParticles = 100;

        // Emission module
        emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = emissionRate;

        // Color over lifetime
        colorModule = particleSystem.colorOverLifetime;
        colorModule.enabled = true;
        
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(startColor, 0f),
                new GradientColorKey(endColor, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        colorModule.color = gradient;

        // Shape module
        var shapeModule = particleSystem.shape;
        shapeModule.shapeType = ParticleSystemShapeType.Rectangle;
        shapeModule.scale = new Vector3(10f, 2f, 1f);

        // Velocity over lifetime
        var velocityModule = particleSystem.velocityOverLifetime;
        velocityModule.enabled = true;
        velocityModule.y = new ParticleSystem.MinMaxCurve(-1f, 1f);
        velocityModule.x = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);
    }

    public void SetEmissionRate(float rate)
    {
        emissionRate = rate;
        if (particleSystem != null)
        {
            var emission = particleSystem.emission;
            emission.rateOverTime = rate;
        }
    }

    public void SetColors(Color start, Color end)
    {
        startColor = start;
        endColor = end;
        
        if (particleSystem != null)
        {
            var colorOverLifetime = particleSystem.colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(startColor, 0f),
                    new GradientColorKey(endColor, 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(0f, 1f)
                }
            );
            colorOverLifetime.color = gradient;
        }
    }
}
