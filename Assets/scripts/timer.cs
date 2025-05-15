using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para reiniciar o cambiar escenas

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float initialTime = 60f;
    [SerializeField] string gameOverSceneName = "GameOver"; // Escena de game over
    private float remainingTime;
    private Color originalColor;
    private bool gameEnded = false; // Para evitar múltiples llamados

    void Start()
    {
        remainingTime = initialTime;
        originalColor = timerText.color;
    }

    void Update()
    {
        if (gameEnded) return; // Si el juego ya terminó, no hacer nada

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            
            if (remainingTime < 10)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            remainingTime = 0;
            gameEnded = true; // Marcar que el juego terminó
            timerText.text = "Time's up!";
            EndGame(); // Llamar a la función que termina el juego
            return;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // Opción 1: Cargar escena de Game Over
        SceneManager.LoadScene(gameOverSceneName);
        
        // Opción 2: Mostrar pantalla de game over en la misma escena
        // (Necesitarías tener un Canvas con un panel desactivado)
        // gameOverPanel.SetActive(true);
        // Time.timeScale = 0f; // Pausar el juego
        
        // Opción 3: Reiniciar el nivel actual
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Debug.Log("Game Over - Time's up!");
    }

    public void ResetTimer()
    {
        remainingTime = initialTime;
        timerText.color = originalColor;
        gameEnded = false;
    }
}