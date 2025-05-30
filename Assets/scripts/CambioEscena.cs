using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena; // Asigna el nombre de la escena a cargar desde el inspector

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("player"))
    {
        SceneManager.LoadScene(nombreEscena);
    }
}

}
