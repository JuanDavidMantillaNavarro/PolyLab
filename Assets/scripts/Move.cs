using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 100f;           // Velocidad normal
    public float sprintMultiplier = 2f; // Multiplicador de velocidad al correr
    public float jumpForce = 15f;      // Fuerza del salto
    public LayerMask groundLayer;     // Capa para detectar el suelo

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificar si el jugador está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        // Definir la dirección del movimiento
        Vector3 dir = (transform.forward * vert + transform.right * hor).normalized;

        // Determinar la velocidad de movimiento (correr si se presiona Shift)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;
        Vector3 newVelocity = dir * currentSpeed;

        // Mantener la velocidad vertical actual
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}