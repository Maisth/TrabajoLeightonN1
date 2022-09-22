using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// MOVEMENT
    private CharacterController controller;
    public float speed = 12f;
    float x;
    float z;
    Vector3 movePlayer;

    /// Gravedad
    Vector3 velocity;
    public float gravity = -15f;
    bool isGrounded = false;

    /// Jump
    public float jumpForce = 1f;
    float jumpValue;

    /// Camera
    private Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        jumpValue = Mathf.Sqrt(jumpForce * -2 * gravity);

        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true && velocity.y < 0)
        {
            velocity.y = gravity;
        }

        CamDirection();

        Movement();

        controller.transform.LookAt(controller.transform.position + movePlayer);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(movePlayer * speed * Time.deltaTime);


    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }


    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        movePlayer = x * camRight + z * camForward;


        //controller.Move(movePlayer * speed * Time.deltaTime);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = jumpValue;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Grounded"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }
    }
}
