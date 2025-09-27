using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool isMoving = false;
    Animator animator;
    private Vector2 moveDir = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Rigidbody2D rb = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("Moving", isMoving);
        if (((moveDir.x <= 0.01f && moveDir.x >= -0.01f) || (moveDir.x >= 0.01f && moveDir.x <= -0.01f)) && (moveDir.y <= 0.01f && moveDir.y >= -0.01f) || (moveDir.y >= 0.01f && moveDir.y <= -0.01f))
        {
            isMoving = false;
        }
        else {
            isMoving = true;
        }
            rb.linearVelocity = moveDir * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}
