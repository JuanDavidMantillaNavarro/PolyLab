using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres UnityEngine.SceneManagement para usar SceneManager

public class olo : MonoBehaviour
{
    public int creador;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            // Cambia a la escena "Escena2" al entrar en el trigger
            SceneManager.LoadScene(creador);
        }
    }
}
