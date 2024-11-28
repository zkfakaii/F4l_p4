using UnityEngine;

public class SnowController : MonoBehaviour
{
    private ParticleSystem snowSystem;

    void Start()
    {
        snowSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Cambiar la intensidad de la nieve (Ejemplo: aumentar la velocidad)
        var main = snowSystem.main;
        main.startSpeed = Mathf.PingPong(Time.time * 0.5f, 1f);
    }
}
