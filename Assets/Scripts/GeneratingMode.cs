using UnityEngine;

public class GeneratingMode : MonoBehaviour
{
    public float mielPerTick = 5f;
    public float tickInterval = 2f;
    private float tickTimer;

    private bool isGenerating = false;
    private bool isAerial = false;
    public MonoBehaviour behaviorScript;
    private Miel mielSystem;

    public Animator animator;
    public string[] stateTriggers = { "NormalMode", "GeneratingMode", "AerialMode" };

    void Start()
    {
        mielSystem = FindObjectOfType<Miel>();

        if (behaviorScript == null)
        {
            Debug.LogWarning("No se ha asignado un script de comportamiento en " + gameObject.name);
        }
    }

    void Update()
    {
        if (isGenerating)
        {
            tickTimer += Time.deltaTime;

            if (tickTimer >= tickInterval)
            {
                GenerateMiel();
                tickTimer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isAerial)
            {
                SetStateToNormal();
            }
            else
            {
                SetStateToAerial();
            }
        }
    }

    private void OnMouseDown()
    {
        isGenerating = !isGenerating;

        if (behaviorScript != null)
        {
            behaviorScript.enabled = !isGenerating;
        }

        if (animator != null)
        {
            if (isGenerating)
            {
                animator.SetTrigger(stateTriggers[1]);
            }
            else
            {
                animator.SetTrigger(stateTriggers[0]);
            }
        }
    }

    private void SetStateToAerial()
    {
        isAerial = true;

        if (animator != null)
        {
            animator.SetTrigger(stateTriggers[2]);
        }

        if (behaviorScript != null)
        {
            behaviorScript.enabled = false;
        }
    }

    private void SetStateToNormal()
    {
        isAerial = false;

        if (animator != null)
        {
            animator.SetTrigger(stateTriggers[0]);
        }

        if (behaviorScript != null)
        {
            behaviorScript.enabled = true;
        }
    }

    private void GenerateMiel()
    {
        if (mielSystem != null)
        {
            mielSystem.currentMiel += (int)mielPerTick;
            mielSystem.currentMiel = Mathf.Clamp(mielSystem.currentMiel, 0, (int)mielSystem.maxMiel);
        }
    }

    // Agregar la propiedad pública IsGenerating para acceder desde otros scripts
    public bool IsGenerating
    {
        get { return isGenerating; }
    }
}
