using UnityEngine;
using System.Collections.Generic;

public class kid_movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private SpriteRenderer sp;

    [SerializeField] private bool canKillPlayer = false; // If false, breaks machines, if true, kills player but can't break machines

    GameManager manager;

    public Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized * moveSpeed;
        startPos = this.gameObject.transform.position;

        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

        if (canKillPlayer)
        {
            sp.color = new Color(184f/255f, 90f/255f, 166f/255f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isStarting || manager.isEnding) rb.linearVelocity = new Vector2(0, 0);

        if (Random.Range(1, 100) == 99)
        {
            if (!manager.isStarting || !manager.isEnding) rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized * moveSpeed;
        }

        /*var unispeed = new Vector2(Random.Range(0, 5), Random.Range(0,5)).Normalize();*/

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("ArcadeMachine") && !canKillPlayer)
        {
            ArcadeMachine machine = collision.gameObject.GetComponent<ArcadeMachine>();
            machine.DestroyMachine();
        }
        else if (collision.gameObject.name.StartsWith("Player") && canKillPlayer)
        {
            manager.GameOver();
        }

        //rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5,5)).normalized * moveSpeed;        
    }

    public void UpdateSpeed(float change) => moveSpeed += change;
}
