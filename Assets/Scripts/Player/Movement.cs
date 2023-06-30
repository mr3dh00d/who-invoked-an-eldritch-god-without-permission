using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed = 2.5f;
    [SerializeField] private Vector2 direction;
    private bool movementEnabled;
    private Rigidbody2D rb;
    private Animator playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Movement script is working");
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        movementEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled){
            direction = new Vector2(0, 0);
            if (Input.GetKey("up")){
                direction += Vector2.up;
            }
            if (Input.GetKey("left")){
                direction += Vector2.left;
            }
            if (Input.GetKey("down")){
                direction += Vector2.down;
            }
            if (Input.GetKey("right")){
                direction += Vector2.right;
            }
            direction = direction.normalized;

        }
        playerAnimation.SetFloat("Horizontal", direction.x);
        playerAnimation.SetFloat("Vertical", direction.y);
        playerAnimation.SetFloat("Speed", direction.sqrMagnitude);
    }

    private void FixedUpdate() {
        Vector2 targetPosition = rb.position + direction * speed * Time.deltaTime;
        rb.MovePosition(targetPosition);
    }

    public void StopMovement(){
        direction = new Vector2(0, 0);
    }

    public void EnableMovement(){
        movementEnabled = true;
    }

    public void DisableMovement(){
        movementEnabled = false;
    }

}
