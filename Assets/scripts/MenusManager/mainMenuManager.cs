using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "instruccionesEnlaces";

    // Función para el botón "Jugar"
    public void PlayGame()
    {
        Debug.Log("Botón Jugar presionado. Cargando escena: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }
}
