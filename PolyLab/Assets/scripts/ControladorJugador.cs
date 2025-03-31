using UnityEngine;
using UnityEngine.UI;

public class ControladorJugador : MonoBehaviour
{

    public float velocidad = 10f;
    int hidrogeno = 0;
    public Text textoHidrogeno;
    Rigidbody miRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miRigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        miRigidbody.AddForce(new Vector3(horizontal, 0, vertical) * velocidad);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hidrogeno"))
        {
            other.gameObject.SetActive(false);
            hidrogeno = hidrogeno + 1;
            textoHidrogeno.text = "Hidrogeno: " + hidrogeno;
        }
    }
}
