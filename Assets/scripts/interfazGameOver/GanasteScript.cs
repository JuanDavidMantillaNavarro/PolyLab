using UnityEngine;

public class GanasteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Reinicia la escena actual
    }
}
