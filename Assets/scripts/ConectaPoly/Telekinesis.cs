using UnityEngine;
using UnityEngine.SceneManagement;

public class TelekinesisZY : MonoBehaviour
{
    public static GameObject[] hidrogenos; // arreglo global de hidrógenos
    public static int cont = 0;            // contador global de hidrógenos unidos

    private static bool initialized = false;

    private Camera cam;
    private bool isSelected = false;
    private float moveSpeed = 10f;
    private float scrollSpeed = 2f;
    private float xDepth;

    public string Ganaste;
    private bool unido = false;
    private Rigidbody rb;
    private Quaternion inicio;

    void Start()
    {
        if (!initialized)
        {
            hidrogenos = GameObject.FindGameObjectsWithTag("Hidrogeno");
            initialized = true;
        }

        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        inicio = transform.rotation;
    }

    void Update()
    {
        if (cont >= hidrogenos.Length && hidrogenos.Length > 0)
        {
            SceneManager.LoadScene(Ganaste);
        }

        if (unido) return;

        // Selección con clic
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                isSelected = true;
                xDepth = transform.position.x;
            }
        }

        // Movimiento con el ratón
        if (Input.GetMouseButton(0) && isSelected)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(cam.transform.position.x - xDepth);
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            worldPos.x = xDepth;

            transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * moveSpeed);
            rb.isKinematic = true;
        }

        // Profundidad con scroll
        if (isSelected)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                xDepth += scroll * scrollSpeed;
            }
            rb.isKinematic = true;
        }

        // Soltar
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            rb.isKinematic = false;
        }
    }

    // Llamado por cada objeto cuando se une correctamente
    public void Unir()
    {
        if (unido) return;

        unido = true;
        cont++; // Aumentar contador global
        Debug.Log("Unidos: " + cont + " de " + hidrogenos.Length);

        isSelected = false;
        rb.isKinematic = true;
        transform.rotation = inicio;
    }
}
