using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    private int brokenLevel = 0;
    //private SpriteRenderer sp;
    Animator animator;
    [SerializeField] private AudioClip fixing;
    [SerializeField] private AudioClip destroyed;
    [SerializeField] private Sprite[] baseSprites;
    [SerializeField] private Sprite brokenSprite;

    GameManager manager;

    private AudioSource audiosource;

    void Awake()
    {
        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();

        //sp = GetComponent<SpriteRenderer>();

        animator.SetInteger("color", SelectCabinet());
    }

    public bool IsBroken() => brokenLevel > 0;

    public void FixUp()
    {
        if (brokenLevel > 0)
        {
            // Sound effect for player fixing machine
            audiosource.clip = fixing;

            manager.AddScore(150);

            audiosource.Play();

            brokenLevel--;
            if (brokenLevel == 0)
            {
                manager.ChangeBrokenCount(-1);
                animator.SetBool("isBurning", false);
                animator.SetInteger("color", SelectCabinet());
            }
        }
    }

    public void DestroyMachine()
    {
        if (!IsBroken())
        {
            animator.SetBool("isBurning", true);
            //Audio for destroyed machine
            audiosource.clip = destroyed;

            audiosource.Play();

            brokenLevel = 5;
            manager.ChangeBrokenCount(1);

            //sp.sprite = brokenSprite;
        }
    }

    private int SelectCabinet(){
      return Random.Range(0, 4);
    }
        //sp.sprite = baseSprites[Random.Range(0, baseSprites.Length)];
}
