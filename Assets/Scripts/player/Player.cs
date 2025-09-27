using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 moveDir = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private BoxCollider2D[] fixBoxes = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
