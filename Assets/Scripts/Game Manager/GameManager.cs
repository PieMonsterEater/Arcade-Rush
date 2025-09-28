using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int numMachines = 0;
    private int numBrokenMachines = 0;
    private int score = 0;
    [SerializeField] private float startTimeRemaining = 120;
    [SerializeField] private float timeRemaining = 120;

    [SerializeField] private float gameOverTime = 8;
    private float roundStartTimer = 3;
    private float roundEndTimer = 3;
    private int displayMinutes = 0;
    private int displaySeconds = 0;
    [Header("Audio Stuffs")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    public int Round { get; private set; }

    [HideInInspector] public Player player;

    [Header("Misc")]
    private kid_movement[] kids;
    private ArcadeMachine[] machineArray;
    public bool isStarting { get; private set; }
    public bool isEnding { get; private set; }
    public bool isOver { get; private set; }

    [SerializeField] TMP_Text timeDisplay;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text brokenDisplay;
    [SerializeField] TMP_Text roundDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isStarting = true;
        isEnding = false;
        isOver = false;
        Round = 1;

        displayMinutes = (int)(timeRemaining / 60);
        displaySeconds = (int)(timeRemaining % 60);

        machineArray = UnityEngine.Object.FindObjectsByType<ArcadeMachine>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );

        numMachines = machineArray.Length;

        player = UnityEngine.Object.FindAnyObjectByType<Player>();
        kids = UnityEngine.Object.FindObjectsByType<kid_movement>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );
    }

    public void StartRound()
    {
        roundStartTimer -= Time.deltaTime;
        roundDisplay.gameObject.SetActive(true);
        roundDisplay.text = "Round " + Round;

        if (roundStartTimer <= 0)
        {
            isStarting = false;
            roundDisplay.gameObject.SetActive(false);
            roundStartTimer = 3;
            timeRemaining = startTimeRemaining;
            
            for (int i = 0; i < machineArray.Length / 3; i++)
            {
                int toBreak = Random.Range(0, machineArray.Length);

                if (!machineArray[toBreak].IsBroken()) machineArray[toBreak].DestroyMachine();
                else i--;
            }
        }
    }

    private void EndRound()
    {
        roundEndTimer -= Time.deltaTime;

        if (roundEndTimer <= 0)
        {
            isEnding = false;
            player.gameObject.transform.position = player.startPos;
            foreach (kid_movement kid in kids)
            {
                kid.gameObject.transform.position = kid.startPos;
                kid.UpdateSpeed(1);
            }
            Round++;
            isStarting = true;
            roundEndTimer = 3;
        }
    }

    public void GameOver()
    {
        isOver = true;
        isEnding = true;

        roundDisplay.text = "Game Over";
        roundDisplay.gameObject.SetActive(true);

        audioSource.clip = loseSound;
        audioSource.Play();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOver)
        {
            gameOverTime -= Time.deltaTime;

            if (gameOverTime <= 0)
            { 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            return;
        }

        if (isStarting) { StartRound(); return; }
        if (isEnding) { EndRound(); return; }

        if (timeRemaining <= 0) GameOver();

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
            audioSource.clip = winSound;
            audioSource.Play();
        }
    }

    public void AddScore(int change) => score += change;
} 
