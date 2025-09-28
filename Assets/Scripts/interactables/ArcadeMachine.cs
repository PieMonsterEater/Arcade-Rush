using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    private int brokenLevel = 0;
    [SerializeField] GameObject brokenIndicator;
    [SerializeField] private AudioClip fixing;
    [SerializeField] private AudioClip destroyed;

    GameManager manager;

    private AudioSource audiosource;

    void Awake()
    {
        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

        audiosource = GetComponent<AudioSource>();

        if (Random.Range(0, 2) == 1)
        {
            Debug.Log("I am a broken Arcade Machine!");
            brokenLevel = 5;
            brokenIndicator.SetActive(true);
        }

        manager.AddMachine(IsBroken());
    }

    public bool IsBroken() => brokenLevel > 0;

    public void FixUp()
    {

        if (brokenLevel > 0)
        {
            //Sound effect for player fixing machine
            audiosource.clip = fixing;

            audiosource.Play();

            brokenLevel--;
            if (brokenLevel == 0)
            {
                brokenIndicator.SetActive(false);
                manager.ChangeBrokenCount(-1);
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
            brokenIndicator.SetActive(true);
            manager.ChangeBrokenCount(1);
        }
    }
}
