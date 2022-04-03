using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPun
{
    public CharacterController CharacterController;

    public float speed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    bool onGround;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
        }
    }

    void Move()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        CharacterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        CharacterController.Move(velocity * Time.deltaTime);
    }
}
