using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    [Header("Gravity/Jump")]
    public float gravity = -20f;

    [Header("State")]
    public bool canMove = true;

    CharacterController cc;
    float yVel;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = (transform.right * h + transform.forward * v).normalized * (canMove ? speed : 0f);

        if (cc.isGrounded)
        {
            if (yVel < 0f) yVel = -2f;
        }
        yVel += gravity * Time.deltaTime;

        move.y = yVel;

        cc.Move(move * Time.deltaTime);
    }
}
