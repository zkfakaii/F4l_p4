using UnityEngine;

public class MovingFogWithIntensity : MonoBehaviour
{
    public ParticleSystem fogParticles;  // Referencia al sistema de part�culas
    public Vector3 movementDirection = new Vector3(0.1f, 0f, 0f);  // Direcci�n de movimiento de la niebla
    public float speed = 0.1f;   // Velocidad del movimiento
    public float gapChance = 0.2f; // Probabilidad de dejar un espacio en la niebla (menor valor = m�s huecos)

    // Par�metros para variar la intensidad de la niebla
    public float minFogDensity = 0.01f;  // Densidad m�nima de la niebla
    public float maxFogDensity = 0.1f;   // Densidad m�xima de la niebla
    public float fogIntensitySpeed = 0.5f;  // Velocidad a la que cambia la densidad

    private ParticleSystem.Particle[] particles; // Array de part�culas del sistema de part�culas
    private float currentFogDensity;  // Densidad actual de la niebla

    void Start()
    {
        // Obtener las part�culas del sistema de part�culas
        particles = new ParticleSystem.Particle[fogParticles.main.maxParticles];

        // Establecer densidad inicial
        currentFogDensity = minFogDensity;
    }

    void Update()
    {
        // Mover el sistema de part�culas en la direcci�n deseada
        fogParticles.transform.position += movementDirection * speed * Time.deltaTime;

        // Actualizar las part�culas y su transparencia para crear "espacios" (huecos)
        int particleCount = fogParticles.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            // Crear un "hueco" en la niebla
            if (Random.value < gapChance)
            {
                // Hacer que la part�cula se vuelva completamente transparente para simular el espacio
                particles[i].startSize = 0f;
                particles[i].startColor = new Color(1f, 1f, 1f, 0f);  // Hacer la part�cula invisible
            }
            else
            {
                // De lo contrario, hacer que la part�cula tenga un tama�o y opacidad normal
                particles[i].startSize = 1f;
                particles[i].startColor = new Color(1f, 1f, 1f, 0.2f);  // Opacidad normal (ajustable)
            }
        }

        // Aplicar las part�culas actualizadas al sistema de part�culas
        fogParticles.SetParticles(particles, particleCount);

        // Variar la densidad de la niebla con el tiempo
        // Usamos Mathf.PingPong para variar la densidad entre minFogDensity y maxFogDensity
        currentFogDensity = Mathf.PingPong(Time.time * fogIntensitySpeed, maxFogDensity - minFogDensity) + minFogDensity;

        // Actualizar la densidad de la niebla
        RenderSettings.fogDensity = currentFogDensity;
    }
}
