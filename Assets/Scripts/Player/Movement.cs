using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Movement script is working");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        // transform.position += move * speed * Time.deltaTime;
    }

    private void FixedUpdate() {
        Vector2 targetPosition = rb.position + direction * speed * Time.deltaTime;
        rb.MovePosition(targetPosition);
    }

}
