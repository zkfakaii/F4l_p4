using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu; // Referencia al objeto del men�
    public Button[] sceneButtons; // Array de botones para cambiar de escena

    private bool isMenuActive = false; // Estado del men�

    void Start()
    {
        // Aseg�rate de que el men� est� desactivado al inicio
        menu.SetActive(false);

        // Asignar la acci�n de cada bot�n para cambiar de escena
        foreach (Button button in sceneButtons)
        {
            button.onClick.AddListener(() => LoadScene(button.name));
        }
    }

    void Update()
    {
        // Detectar la tecla Escape para mostrar/ocultar el men�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // Funci�n para mostrar/ocultar el men�
    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menu.SetActive(isMenuActive);

        // Si el men� est� activo, congelar el tiempo
        if (isMenuActive)
        {
            Time.timeScale = 0; // Congelar el juego
        }
        else
        {
            Time.timeScale = 1; // Reanudar el juego
        }
    }

    // Funci�n para cargar la escena seg�n el nombre del bot�n
    void LoadScene(string sceneName)
    {
        // Asegurarse de restablecer el tiempo al cargar una nueva escena
        Time.timeScale = 1; // Reanudar el juego antes de cambiar de escena
        SceneManager.LoadScene(sceneName);
    }

    // Cuando se cambia de escena, desactivamos el men� si est� visible
    void OnLevelWasLoaded(int level)
    {
        // Desactivar el men� cuando se carga una nueva escena
        menu.SetActive(false); // Asegura que el men� se desactive
        isMenuActive = false;

        // Asegurarnos de que el tiempo est� activo
        Time.timeScale = 1;
    }
}
