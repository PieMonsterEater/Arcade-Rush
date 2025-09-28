using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    private int brokenLevel = 0;
    private SpriteRenderer sp;
    [SerializeField] private AudioClip fixing;
    [SerializeField] private AudioClip destroyed;
    [SerializeField] private Sprite[] baseSprites;
    [SerializeField] private Sprite brokenSprite;

    GameManager manager;

    private AudioSource audiosource;

    void Awake()
    {
        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

        audiosource = GetComponent<AudioSource>();

        sp = GetComponent<SpriteRenderer>();

        SelectCabinet();
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

                SelectCabinet();
            }
        }
    }

    public void DestroyMachine()
    {
        if (!IsBroken())
        {
            //Audio for destroyed machine
            audiosource.clip = destroyed;

            audiosource.Play();

            brokenLevel = 5;
            manager.ChangeBrokenCount(1);

            sp.sprite = brokenSprite;
        }
    }

    private void SelectCabinet() => sp.sprite = baseSprites[Random.Range(0, baseSprites.Length)];
}
