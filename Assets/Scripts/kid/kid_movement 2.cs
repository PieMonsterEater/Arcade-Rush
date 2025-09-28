using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour


{
     private Vector2 moveDir = new Vector2(0, 0);
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private BoxCollider2D[] fixBoxes = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1,100)==99)
        {
rb.linearVelocity = new Vector2(Random.Range(-5, 5), Random.Range(-5,5));
        }
        
        /*var unispeed = new Vector2(Random.Range(0, 5), Random.Range(0,5)).Normalize();*/

    }
}
