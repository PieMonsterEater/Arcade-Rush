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

    [SerializeField] TMP_Text timeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayMinutes = (int)(timeRemaining / 60);
        displaySeconds = (int)(timeRemaining % 60);
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

        if (displayMinutes < 9) minutes = "0" + displayMinutes;
        else minutes = displayMinutes.ToString();

        if (displaySeconds < 9) seconds = "0" + displaySeconds;
        else seconds = displaySeconds.ToString();

        timeDisplay.text = minutes + ":" + seconds;
    }

    public void AddMachine(bool broken)
    {
        numMachines++;
        if (broken) numBrokenMachines++;

        Debug.Log("Number of machines: " + numMachines);
        Debug.Log("Number of broken machines" + numBrokenMachines);
    }
}
