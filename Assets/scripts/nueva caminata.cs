using TMPro;
using UnityEngine;
using UnityEngine.UI; // Importar el espacio de nombres UnityEngine.UI para usar la clase Text

public class nuevacaminata : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 5f;
    //public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public TextMeshProUGUI textoHidrogeno; // Cambia a TextMeshProUGUI para usar el componente de texto de TextMesh Pro
    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded, CamMapa, CamMapaBloqueada;
    private int cont = 0;
    public float tiempoMapa = 30f;
    private float tiempoRestante;
    int hidrogeno = 0;
    public GameObject Camaraplayer;
    public GameObject CamaraMapa;
    public GameObject Cilindro;

    public TextMeshProUGUI TimerMapa;

    public AudioSource audioSource;
    public AudioSource audiossj;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        tiempoRestante = tiempoMapa;

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (Camaraplayer.activeSelf) // solo mover cámara si está activa la 1ª persona
        {
            HandleMouseLook();
            HandleMovement();
            CamMapa = false;
        }
        // Camara
        if (Input.GetKeyDown(KeyCode.E) && CamMapaBloqueada != true)
        {
            ActivateCamara1p(cont % 2 == 1);
            cont++;
            CamMapa = true;
        }

        ControlarTimerCamaraMapa();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequeña fuerza hacia abajo para asegurar que esté en el suelo
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotar el jugador en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);

        // Rotar la cámara en el eje X (vertical)
        float newXRotation = cameraTransform.localEulerAngles.x - mouseY;
        cameraTransform.localEulerAngles = new Vector3(newXRotation, 0f, 0f);
    }

    public void ActivateCamara1p(bool active)
    {
        Camaraplayer.SetActive(active);
        if (!active)
        {
            CamaraMapa.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Recoger hidrógenos
        if (other.gameObject.CompareTag("Hidrogeno"))
        {
            audioSource.Play();
            other.gameObject.SetActive(false);
            hidrogeno = hidrogeno + 1;
            textoHidrogeno.text = "Hidrogenos: " + hidrogeno;
            if (hidrogeno == 6)
            {

                Debug.Log("Has recogido todos los hidrógenos");
                textoHidrogeno.color = Color.green; // Cambiar el color del texto a verde
                Cilindro.SetActive(true); // Activar el cilindro
                audiossj.Play();
            }
        }
    }

    void ControlarTimerCamaraMapa()
    {


        if (CamMapa && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            //Debug.Log("Tiempo restante: " + Mathf.CeilToInt(tiempoRestante));
            TimerMapa.text = "Tiempo de mapa: " + Mathf.CeilToInt(tiempoRestante) + "s";
        }

        if (tiempoRestante <= 0)
        {
            //Debug.Log("¡Se acabó el tiempo de la cámara del mapa!");
            // Aquí puedes ejecutar una acción si el tiempo llegó a 0
            CamMapaBloqueada = true;
            ActivateCamara1p(cont % 2 == 1);
        }

    }
    
}