using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para volver al menú principal

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Asigna el Panel de Pausa desde el Inspector
    public KeyCode pauseKey = KeyCode.Escape; // Tecla para pausar/reanudar

    public static bool isPaused = false; // Variable estática para saber si el juego está pausado

    // Variable pública para asignar el nombre de la escena del menú principal
    public string mainMenuSceneName = "mainMenuScene"; // Cambia "StartMenuScene" por el nombre real de tu escena de menú

    // Variables para guardar el estado del cursor antes de pausar
    private bool previousCursorVisibleState;
    private CursorLockMode previousCursorLockState;
    void Start()
    {
        // Asegúrate de que el panel de pausa esté desactivado al inicio y el juego no esté pausado
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Guardar estado actual del cursor
        previousCursorVisibleState = Cursor.visible;
        previousCursorLockState = Cursor.lockState;

        // Mostrar y liberar cursor para UI
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Juego Pausado");
    }

    public void ResumeGame()
    {
        Cursor.visible = false; // O previousCursorVisibleState si quieres ser más genérico
        Cursor.lockState = CursorLockMode.Locked; // O previousCursorLockState

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Juego Reanudado");
    }

    public void LoadMainMenu()
    {
        Debug.Log("Volviendo al Menú Principal.");
        Time.timeScale = 1f; // Asegúrate de que el tiempo vuelva a la normalidad antes de cambiar de escena
        isPaused = false; // Resetea el estado de pausa
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void ResetGame()
    {
        Debug.Log("Volviendo al instrucciones para resetear el juego.");
        Time.timeScale = 1f; // Asegúrate de que el tiempo vuelva a la normalidad antes de cambiar de escena
        isPaused = false; // Resetea el estado de pausa
        SceneManager.LoadScene("instruccionesEnlaces");
    }
}