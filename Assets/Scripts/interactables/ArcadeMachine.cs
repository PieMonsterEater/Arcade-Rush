using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    private int brokenLevel = 0;
    [SerializeField] GameObject brokenIndicator;

    GameManager manager;

    void Awake()
    {
        manager = UnityEngine.Object.FindAnyObjectByType<GameManager>();

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
            brokenLevel = 5;
            brokenIndicator.SetActive(true);
            manager.ChangeBrokenCount(1);
        }
    }
}
