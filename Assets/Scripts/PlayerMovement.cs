using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;

    [SerializeField]
    private float speed;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        myRB.velocity = inputVector * speed * Time.deltaTime;

        myAnim.SetFloat("moveX", inputVector.x);
        myAnim.SetFloat("moveY", inputVector.y);

        if (inputVector != Vector2.zero)
        {
            myAnim.SetFloat("lastMoveX", inputVector.x);
            myAnim.SetFloat("lastMoveY", inputVector.y);
        }
    }
}