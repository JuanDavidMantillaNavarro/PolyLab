using UnityEngine;

public class ValidadorDeUnion : MonoBehaviour
{
    private bool unido = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hidrogeno"))
        {
            Debug.Log("¡Monomero correcto!");

            // 🔊 Reproducir sonido cuando se conecta correctamente
            GetComponent<AudioSource>()?.Play();

            // Pegar al punto exacto
            other.transform.position = transform.position;

            // Bloquear el movimiento
            TelekinesisZY telekinesis = other.GetComponent<TelekinesisZY>();
            if (telekinesis != null)
            {
                telekinesis.Unir();
            }

            // gameObject.SetActive(false); // para que solo se pueda un solo hidrogeno en el enlace
        }
    }
}