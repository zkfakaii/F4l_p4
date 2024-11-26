using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuController : MonoBehaviour
{
    private const string LastSceneKey = "LastScene"; // Clave para almacenar la última escena

    private void Start()
    {
        // Almacenar la escena actual antes de cambiar a otra
        if (SceneManager.GetActiveScene().name != "GameOver") // Evitar sobrescribir en la escena de Game Over
        {
            PlayerPrefs.SetString(LastSceneKey, SceneManager.GetActiveScene().name);
        }
    }

    public void LoadNextScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void LoadLastScene()
    {
        // Recuperar la última escena almacenada
        string lastScene = PlayerPrefs.GetString(LastSceneKey, string.Empty);

        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No hay una escena previa registrada. Asegúrate de configurar correctamente la clave de la escena anterior.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPaused = !EditorApplication.isPaused;
#endif
    }
}
