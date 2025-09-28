using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numMachines = 0;
    private int numBrokenMachines = 0;
    private int score = 0;
    private float timeRemaining = 120;
    private int displayMinutes = 0;
    private int displaySeconds = 0;
    public int Round { get; private set; }

    public Player player;
    public bool isStarting { get; private set; }
    public bool isEnding { get; private set; }

    [SerializeField] TMP_Text timeDisplay;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text brokenDisplay;

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
    }

    public void StartRound()
    { 
        
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
            Round++;
        }
    }
} 
