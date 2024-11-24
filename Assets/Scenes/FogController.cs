using UnityEngine;

public class MovingFogWithIntensity : MonoBehaviour
{
    public ParticleSystem fogParticles;  // Referencia al sistema de partículas
    public Vector3 movementDirection = new Vector3(0.1f, 0f, 0f);  // Dirección de movimiento de la niebla
    public float speed = 0.1f;   // Velocidad del movimiento
    public float gapChance = 0.2f; // Probabilidad de dejar un espacio en la niebla (menor valor = más huecos)

    // Parámetros para variar la intensidad de la niebla
    public float minFogDensity = 0.01f;  // Densidad mínima de la niebla
    public float maxFogDensity = 0.1f;   // Densidad máxima de la niebla
    public float fogIntensitySpeed = 0.5f;  // Velocidad a la que cambia la densidad

    private ParticleSystem.Particle[] particles; // Array de partículas del sistema de partículas
    private float currentFogDensity;  // Densidad actual de la niebla

    void Start()
    {
        // Obtener las partículas del sistema de partículas
        particles = new ParticleSystem.Particle[fogParticles.main.maxParticles];

        // Establecer densidad inicial
        currentFogDensity = minFogDensity;
    }

    void Update()
    {
        // Mover el sistema de partículas en la dirección deseada
        fogParticles.transform.position += movementDirection * speed * Time.deltaTime;

        // Actualizar las partículas y su transparencia para crear "espacios" (huecos)
        int particleCount = fogParticles.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            // Crear un "hueco" en la niebla
            if (Random.value < gapChance)
            {
                // Hacer que la partícula se vuelva completamente transparente para simular el espacio
                particles[i].startSize = 0f;
                particles[i].startColor = new Color(1f, 1f, 1f, 0f);  // Hacer la partícula invisible
            }
            else
            {
                // De lo contrario, hacer que la partícula tenga un tamaño y opacidad normal
                particles[i].startSize = 1f;
                particles[i].startColor = new Color(1f, 1f, 1f, 0.2f);  // Opacidad normal (ajustable)
            }
        }

        // Aplicar las partículas actualizadas al sistema de partículas
        fogParticles.SetParticles(particles, particleCount);

        // Variar la densidad de la niebla con el tiempo
        // Usamos Mathf.PingPong para variar la densidad entre minFogDensity y maxFogDensity
        currentFogDensity = Mathf.PingPong(Time.time * fogIntensitySpeed, maxFogDensity - minFogDensity) + minFogDensity;

        // Actualizar la densidad de la niebla
        RenderSettings.fogDensity = currentFogDensity;
    }
}
