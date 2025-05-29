using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para reiniciar o cambiar escenas


public class Timerp : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI timerText; // UI en pantalla
    [SerializeField] TextMeshPro worldTimerText; // Texto en la pared
    [SerializeField] Transform player;
    [SerializeField] float initialTime = 60f;
    [SerializeField] string gameOverSceneName = "GameOver"; // Escena de game over
    [SerializeField] LayerMask laberintoLayer; // Capa para las paredes del laberinto
    [SerializeField] TextMeshPro HidrogenosRecogidos;
    private float remainingTime;
    private Color originalColor;
    private bool gameEnded = false; // Para que no pasen múltiples llamados
    private Vector3 wallOffset = new Vector3(0, 1.5f, 0.5f); // Ajuste de posición sobre la pared

    void Start()
    {
        remainingTime = initialTime;
        originalColor = timerText.color;
    }

    void Update()
    {
        if (gameEnded) return; // Si el juego ya termino, dejar texto quieto

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
            timerText.text = "Se acabo el tiempo!";
            worldTimerText.text = "Se acabo el tiempo!";
            EndGame(); // Llamar a la función que termina el juego
            return;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timeString;
        worldTimerText.text = timeString; // Actualizar texto en la pared
       

        UpdateWorldTimerPosition(); // Ajustar posición
    }

    void UpdateWorldTimerPosition()
    {
        if (worldTimerText != null && player != null)
        {
            RaycastHit hitRight;
            Vector3 rayOrigin = player.position + Vector3.up * 1.5f; // seguimiento desde el centro del jugador
            Vector3 rayDirectionRight = player.right; // Derecha del jugador

            bool hasWallRight = Physics.Raycast(rayOrigin, rayDirectionRight, out hitRight, 2f, laberintoLayer);

            Vector3 targetPos = worldTimerText.transform.position; // Mantener la ultima posición válida
            Quaternion targetRotation = worldTimerText.transform.rotation;

            if (hasWallRight)
            {
                //  Angulo de visión segun la normal de la pared
                float dotProduct = Vector3.Dot(player.forward, -hitRight.normal);

                //  Si el jugador no está mirando la pared de frente, colocar el texto a la derecha
                if (dotProduct < 0.85f) // 0.85f es el punto antes de que esté casi de frente
                {
                    Vector3 wallOffset = hitRight.normal * 0.05f;
                    targetPos = hitRight.point + wallOffset;
                    targetPos.y = player.position.y + 0.4f;
                    targetRotation = Quaternion.LookRotation(-hitRight.normal);
                }
                //  Si el jugador sigue girando más allá, mantener el texto en su última posición válida
            }

            // Posición y rotación corregidas
            worldTimerText.transform.position = targetPos;
            worldTimerText.transform.rotation = targetRotation;

        }
        HidrogenosRecogidos.transform.position = worldTimerText.transform.position + Vector3.down * 0.25f;
        HidrogenosRecogidos.transform.rotation = worldTimerText.transform.rotation;
    }








    void EndGame()
    {
        SceneManager.LoadScene(gameOverSceneName);
        Debug.Log("Game Over - Time's up!");
    }

    public void ResetTimer()
    {
        remainingTime = initialTime;
        timerText.color = originalColor;
        gameEnded = false;
    }
}
