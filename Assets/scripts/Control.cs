using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line

public class Control : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 10f;
    public float speedJump = 20f;
    public float speedRotation = 40f;
    public GameObject Camaraplayer;
    private int cont = 0;
    int hidrogeno = 0;
    public Text textoHidrogeno;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moviemiento con teclado
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("girar izquierda");
            transform.Rotate(Vector3.down * speedRotation * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("girar derecha");
            transform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("alante");
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("atras");
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
        }

        // Camara
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateCamara1p(cont % 2 == 1);
            cont++;
        }
    }

    public void ActivateCamara1p(bool active)
    {
        Camaraplayer.SetActive(active);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hidrogeno"))
        {
            other.gameObject.SetActive(false);
            hidrogeno = hidrogeno + 1;
            textoHidrogeno.text = "Hidrogenos: " + hidrogeno;
        }
    }
}