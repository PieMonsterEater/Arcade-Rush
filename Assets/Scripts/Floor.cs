using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    SpriteRenderer sp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        sp.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
