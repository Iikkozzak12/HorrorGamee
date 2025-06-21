using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SideView3DCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJump();
    }

    void GroundCheck()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void HandleMovement()
    {
        // W = do góry ekranu (X+), S = w dół ekranu (X-)
        // D = do przodu postaci (Z+), A = do tyłu (Z-)
        float moveZ = Input.GetAxisRaw("Horizontal"); // A/D → Z
        float moveX = Input.GetAxisRaw("Vertical");   // W/S → X

        Vector3 move = new Vector3(-moveX, 0f, moveZ).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
