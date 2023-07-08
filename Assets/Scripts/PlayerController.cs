using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] Vector2 jumpForce;


    float Horizontal;





    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {

        //GET INPUT
        Horizontal = Input.GetAxis("Horizontal");


        //APPLY INPUT (MOVEMENT)
        float tmp_x_speed = Horizontal* speed *Time.deltaTime;
        transform.Translate(new Vector2(tmp_x_speed, 0));


        //JUMP
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }



}
