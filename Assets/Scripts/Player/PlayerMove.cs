using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAnimation
{
    private Camera cam;
    private CharacterController controller;
    private CameraFollow cf;
    private float startInputSensitive;

    //movement
    private float inputX;
    private float inputZ;
    private Vector3 desiredMoveDirection;
    private bool blockRotationPlayer;
    private float speed;

    [Header("Movement")]
    public float desiredRotationSpeed;
    public float speedAnimation;
    public float velocity;

    //jump
    private Vector3 moveVector;
    private bool isGrounded;
    private float verticalVel;
    [Header("Jump and Dash")]
    public float jumpForce;
    public float gravity;

    //dash
    public float dashForce;
    private float horizontalVel;

    // Use this for initialization
    void Start ()
    {
        cf = FindObjectOfType<CameraFollow>();
        startInputSensitive = cf.inputSensitivity;
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update ()
    {
        PlayerMoveAndRotation();
        //Jump();
       // Dash();
    
	}

    #region Movement
    private void PlayerMoveAndRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //Calculate the Input Magnitude
        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;
        if (isAttacking(5) || isAttacking(6))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(camera.transform.forward.x,
                                                                                                          desiredMoveDirection.y,
                 
                                                                                                          camera.transform.forward.z)), desiredRotationSpeed);
            if(isAttacking(6))
            {
                cf.inputSensitivity = 0f;

            }
            else
            {
                cf.inputSensitivity = startInputSensitive;
            }

        }
        else if (!blockRotationPlayer && Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            cf.inputSensitivity = startInputSensitive;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

        //graduate move
        //lerpTime += Time.deltaTime * 0.1f;

        //animation and player speed
        Movement("Speed", speedAnimation);
        Movement("InputMagnitude", speed);
        Movement("Input", Input.GetButton("Horizontal") || Input.GetButton("Vertical"));

        /* lerp movement
        if (!isAttacking(0))
        {
            controller.Move(desiredMoveDirection*0.1f);
        }
        else
        {
            controller.Move(new Vector3(VariableLerp(0, desiredMoveDirection.x, lerpTime),
                                        VariableLerp(0, desiredMoveDirection.y, lerpTime),
                                        VariableLerp(0, desiredMoveDirection.z, lerpTime)));
        }
        */
        if (!isAttacking(0))
        {
            controller.Move(desiredMoveDirection * 0);

        }
        else
        {
            controller.Move(desiredMoveDirection * velocity);
        }


    }

    private float VariableLerp(float start, float end, float speed)
    {
        return Mathf.Lerp(start, end, speed);
    }

    #endregion

    private void Jump()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                verticalVel += jumpForce;
            }
        }
        else
        {
            verticalVel -= gravity * Time.deltaTime;
        }
       // anim.SetBool("Jump", !isGrounded);
        Movement("Jump", !isGrounded);

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = verticalVel;
        moveVector.z = Input.GetAxis("Vertical");
        controller.Move(moveVector * Time.deltaTime);
    }

    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Movement("Dash", true);

           // anim.SetBool("Dash", true);
        }
        else
        {
            Movement("Dash", false);

            //anim.SetBool("Dash", false);
        }
    }
}
