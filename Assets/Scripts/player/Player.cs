using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool isMoving = false;
    Animator animator;
    private Vector2 moveDir = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private BoxCollider2D[] fixBoxes = null;
    [SerializeField] private AudioClip footsteps;

    public Transform startPos;

    private GameManager manager;

    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

        startPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.isStarting || manager.isEnding) return;

        animator.SetBool("Moving", isMoving);
        if (((moveDir.x <= 0.01f && moveDir.x >= -0.01f) || (moveDir.x >= 0.01f && moveDir.x <= -0.01f)) && (moveDir.y <= 0.01f && moveDir.y >= -0.01f) || (moveDir.y >= 0.01f && moveDir.y <= -0.01f))
        {
            isMoving = false;
            
            audioSource.clip = footsteps;

            audioSource.Play();
        }
        else {
            isMoving = true;

            //audioSource.clip = footsteps;

            //audioSource.Play();
        }
       
        // Moves the player in the direction they are pressing the keys
        rb.linearVelocity = moveDir * moveSpeed;
    }

    // Reads the move input from the PlayerControls input actions
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    // Checks the area around the player for things that they are trying to interact with
    public void OnFix(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        foreach (BoxCollider2D fixBox in fixBoxes)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(fixBox.transform.TransformPoint(fixBox.offset), fixBox.size, 0f, 1);

            if (hits.Length != 0)
            {
                foreach (Collider2D hit in hits)
                {
                    // Debug.Log(fixBox.gameObject.name + ": " + hit.gameObject.name);
                    if (hit.gameObject.name.StartsWith("ArcadeMachine"))
                    {
                        Debug.Log("Fixing Machine");
                        ArcadeMachine am = hit.gameObject.GetComponent<ArcadeMachine>();

                        if (am.IsBroken()) { am.FixUp(); return; }
                    }
                }
            }
        }
    }
}
