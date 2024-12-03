using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu; // Referencia al objeto del menú
    public Button[] sceneButtons; // Array de botones para cambiar de escena

    private bool isMenuActive = false; // Estado del menú

    void Start()
    {
        // Asegúrate de que el menú está desactivado al inicio
        menu.SetActive(false);

        // Asignar la acción de cada botón para cambiar de escena
        foreach (Button button in sceneButtons)
        {
            button.onClick.AddListener(() => LoadScene(button.name));
        }
    }

    void Update()
    {
        // Detectar la tecla Escape para mostrar/ocultar el menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // Función para mostrar/ocultar el menú
    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menu.SetActive(isMenuActive);

        // Si el menú está activo, congelar el tiempo
        if (isMenuActive)
        {
            Time.timeScale = 0; // Congelar el juego
        }
        else
        {
            Time.timeScale = 1; // Reanudar el juego
        }
    }

    // Función para cargar la escena según el nombre del botón
    void LoadScene(string sceneName)
    {
        // Asegurarse de restablecer el tiempo al cargar una nueva escena
        Time.timeScale = 1; // Reanudar el juego antes de cambiar de escena
        SceneManager.LoadScene(sceneName);
    }

    // Cuando se cambia de escena, desactivamos el menú si está visible
    void OnLevelWasLoaded(int level)
    {
        // Desactivar el menú cuando se carga una nueva escena
        menu.SetActive(false); // Asegura que el menú se desactive
        isMenuActive = false;

        // Asegurarnos de que el tiempo está activo
        Time.timeScale = 1;
    }
}
