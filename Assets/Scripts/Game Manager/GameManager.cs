using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numMachines = 0;
    private int numBrokenMachines = 0;
    private int score = 0;
    private float timeRemaining = 120;
    private float roundStartTimer = 3;
    private float roundEndTimer = 3;
    private int displayMinutes = 0;
    private int displaySeconds = 0;
    public int Round { get; private set; }

    public Player player;
    private kid_movement[] kids;
    public bool isStarting { get; private set; }
    public bool isEnding { get; private set; }

    [SerializeField] TMP_Text timeDisplay;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text brokenDisplay;
    [SerializeField] TMP_Text roundDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isStarting = true;
        isEnding = false;
        Round = 1;

        displayMinutes = (int)(timeRemaining / 60);
        displaySeconds = (int)(timeRemaining % 60);

        ArcadeMachine[] machineArray = UnityEngine.Object.FindObjectsByType<ArcadeMachine>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        numMachines = machineArray.Length;

        for (int i = 0; i < machineArray.Length / 3; i++)
        {
            int toBreak = Random.Range(0, machineArray.Length);

            if (!machineArray[toBreak].IsBroken()) machineArray[toBreak].DestroyMachine();
            else i--;
        }

        player = UnityEngine.Object.FindAnyObjectByType<Player>();
        kids = UnityEngine.Object.FindObjectsByType<kid_movement>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );
    }

    public void StartRound()
    {
        roundStartTimer -= Time.deltaTime;
        roundDisplay.SetActive(true);
        roundDisplay.text = "Round " + Round;

        if (roundStartTimer <= 0)
        {
            isStarting = false;
            roundDisplay.SetActive(false);
            roundStartTimer = 3;
        }
    }

    private void EndRound()
    {
        roundEndTimer -= Time.deltaTime;

        if (roundEndTimer <= 0)
        {
            isEnding = false;
            foreach (kid_movement kid in kids)
            {
                kid.gameObject.transform = kid.startPos;
                kid.UpdateSpeed(1);
            }
            Round++;
            isStarting = true;
            roundEndTimer = 3;
        }
    }

    private void GameOver()
    {
        roundDisplay.text = "Game Over";
        roundDisplay.SetActive(true);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeRemaining <= 0) return;

        timeRemaining -= Time.deltaTime;

        displayMinutes = (int)(timeRemaining / 60);
        displaySeconds = (int)(timeRemaining % 60);

        string minutes;
        string seconds;

        if (displayMinutes <= 9) minutes = "0" + displayMinutes;
        else minutes = displayMinutes.ToString();

        if (displaySeconds <= 9) seconds = "0" + displaySeconds;
        else seconds = displaySeconds.ToString();

        timeDisplay.text = minutes + ":" + seconds;
        scoreDisplay.text = score.ToString();
        brokenDisplay.text = numBrokenMachines + "/" + numMachines;
    }

    public void ChangeBrokenCount(int change)
    {
        numBrokenMachines += change;
        if (numBrokenMachines <= 0)
        {
            isEnding = true;
        }
    }

    public void AddScore(int change) => score += change;
} 
