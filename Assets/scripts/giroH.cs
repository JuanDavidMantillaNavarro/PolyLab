using UnityEngine;
using System.Collections;

public class giroH : MonoBehaviour
{
    public float velocidad = 200f;
    int hidrogeno = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, velocidad * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hidrogeno"))
        {
            hidrogeno = hidrogeno + 1;
        }
    }
}