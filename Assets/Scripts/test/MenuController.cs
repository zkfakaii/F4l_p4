using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    private const string LastSceneKey = "LastScene"; // Clave para almacenar la �ltima escena

    private void Start()
    {
        // Almacenar la escena actual antes de cambiar a otra
        if (SceneManager.GetActiveScene().name != "GameOver") // Evitar sobrescribir en la escena de Game Over
        {
            PlayerPrefs.SetString(LastSceneKey, SceneManager.GetActiveScene().name);
        }
    }

    // M�todo para cargar una escena espec�fica por nombre
    public void LoadNextScene(string nextScene)
    {
        Time.timeScale = 1;
        // Validar si la escena existe antes de cargarla
        if (SceneExists(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning($"La escena {nextScene} no est� configurada en el proyecto.");
        }
    }

    // M�todo para cargar la �ltima escena jugada
    public void LoadLastScene()
    {
        // Recuperar la �ltima escena almacenada
        string lastScene = PlayerPrefs.GetString(LastSceneKey, string.Empty);

        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No hay una escena previa registrada. Aseg�rate de configurar correctamente la clave de la escena anterior.");
        }
    }

    // M�todo para cargar el Nivel 1
    public void LoadNivel1()
    {
        LoadNextScene("mapa 1");
    }

    // M�todo para cargar el Nivel 2
    public void LoadNivel2()
    {
        LoadNextScene("mapa 2");
    }
    public void LoadVideo()
    {
        LoadNextScene("video");
    }

    
    // M�todo para cargar el Nivel 3
    public void LoadNivel3()
    {
        LoadNextScene("Nivel3");
    }

    // M�todo para cargar el Tutorial
    public void LoadTutorial()
    {
        LoadNextScene("mapa tutorial");
    }

    // M�todo para cargar la escena de selecci�n de niveles
    public void LoadLevelSelection()
    {
        LoadNextScene("seleccion"); // Nombre de la escena de selecci�n de niveles
    }

    // M�todo para cargar el nivel elegido desde la selecci�n
    public void LoadSelectedLevel(int levelIndex)
    {
        string levelName = GetLevelName(levelIndex);
        LoadNextScene(levelName);
    }

    // Obtener el nombre del nivel basado en el �ndice
    private string GetLevelName(int levelIndex)
    {
        switch (levelIndex)
        {
            case 1: return "mapa 1";
            case 2: return "mapa 2";
            case 3: return "Nivel3";
            case 4: return "mapa tutorial";
            default: return "mapa 1"; // Default en caso de que no se reciba un �ndice v�lido
        }
    }

    // M�todo para verificar si la escena existe en el proyecto
    private bool SceneExists(string sceneName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity");
        return sceneIndex != -1;
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPaused = !EditorApplication.isPaused;
#endif
    }
}
