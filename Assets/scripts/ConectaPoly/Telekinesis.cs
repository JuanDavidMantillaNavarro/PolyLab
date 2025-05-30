using UnityEngine;
using UnityEngine.Jobs;

public class TelekinesisZY : MonoBehaviour
{
    private Camera cam;
    private bool isSelected = false;
    private float moveSpeed = 10f;
    private float scrollSpeed = 2f;
    private float xDepth;
    private bool unido = false; // si ya se unio, entonces no se mueve m�s
    Rigidbody rb;
    Quaternion inicio;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        inicio = transform.rotation;
    }

    void Update()
    {
        if (unido) return;

        // Detectar click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                isSelected = true;
                xDepth = transform.position.x;
            }
        }

        // Mover mientras se mantenga click
        if (Input.GetMouseButton(0) && isSelected)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(cam.transform.position.x - xDepth);

            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            worldPos.x = xDepth;

            transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * moveSpeed);
            rb.isKinematic = true;
        }

        // Rueda del mouse para mover en eje X (profundidad)
        if (isSelected)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                xDepth += scroll * scrollSpeed;

            }
            rb.isKinematic = true;
        }


        // Soltar al soltar clic
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            rb.isKinematic = false;
        }
    }

    // Metodo para llamar cuando se una el monomero
    public void Unir()
    {
        unido = true;
        isSelected = false;
        rb.isKinematic = true;
        transform.rotation = inicio;
    }
}