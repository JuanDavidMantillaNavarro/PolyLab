using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this line

public class nuevacaminata : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 5f;
    //public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public Text textoHidrogeno;
    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;
    private int cont = 0;
    int hidrogeno = 0;
    public GameObject Camaraplayer;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        //HandleJump();

        // Camara
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateCamara1p(cont % 2 == 1);
            cont++;
        }
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
    }

    void OnTriggerEnter(Collider other)
    {
        // Recoger hidrógenos
        if (other.gameObject.CompareTag("Hidrogeno"))
        {
            other.gameObject.SetActive(false);
            hidrogeno = hidrogeno + 1;
            textoHidrogeno.text = "Hidrogenos: " + hidrogeno;
            if (hidrogeno == 6)
            {
                Debug.Log("Has recogido todos los hidrógenos");
                textoHidrogeno.color = Color.green; // Cambiar el color del texto a verde
            }
        }
    }

    /*
    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    */
}