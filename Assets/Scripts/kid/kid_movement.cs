using UnityEngine;
using System.Collections.Generic;

public class kid_movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private Rigidbody2D rb = null;

    private Transform startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized * moveSpeed;
        startPos = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 100) == 99)
        {
            rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized * moveSpeed;
        }

        /*var unispeed = new Vector2(Random.Range(0, 5), Random.Range(0,5)).Normalize();*/

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("ArcadeMachine"))
        {
            ArcadeMachine machine = collision.gameObject.GetComponent<ArcadeMachine>();
            machine.DestroyMachine();
        }

        //rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5,5)).normalized * moveSpeed;        
    }

    public void UpdateSpeed(float change) => moveSpeed += change;
}
