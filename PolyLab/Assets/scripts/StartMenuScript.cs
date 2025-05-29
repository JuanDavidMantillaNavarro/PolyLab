// using UnityEngine;

// public class StartMenuScript : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class StartMenuScript : MonoBehaviour
{
    // Variable pública para asignar el nombre de la escena de juego desde el Inspector
    public string gameSceneName = "NIVEL_UNO"; // Cambia "GameScene" por el nombre real de tu escena de juego

    // Función para el botón "Jugar"
    public void PlayGame()
    {
        Debug.Log("Botón Jugar presionado. Cargando escena: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }
}