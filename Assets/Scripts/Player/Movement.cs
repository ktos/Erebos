using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Movement : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                //GetComponent<Animator>().SetTrigger("Jump");
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        float rotspeed = Input.GetAxis("Vertical") == 0 ? 300.0f : 200.0f;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotspeed;
        controller.transform.Rotate(new Vector3(0, x, 0));

        GetComponent<Animator>().SetBool("MakeWalk", Input.GetAxis("Vertical") != 0);
    }

    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }
}