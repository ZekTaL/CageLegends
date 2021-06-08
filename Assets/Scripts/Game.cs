using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Class that acts as a GameManager with all the methods related to the game 
/// </summary>
public class Game : MonoBehaviour
{
    private int player1Score = 0;
    // player 1 initial starting position
    private Vector3 player1StartPos = new Vector3(20, 0f, 25);
    private int player2Score = 0;
    // player 2 initial starting position
    private Vector3 player2StartPos = new Vector3(80, 0f, 25);

    // ball spawning position in the player 1 half field
    private Vector3 BallLeftPos = new Vector3(32, 0f, 25);
    // ball spawning position in the center of the field
    private Vector3 BallOriginPos = new Vector3(50f, 0f, 25);
    // ball spawning position in the player 2 half field
    private Vector3 BallRightPos = new Vector3(68, 0f, 25);

    private bool isCountdown = false;
    private float countdownTime = 3f;
    private float currentMatchTime = 1f;

    private AudioSource audioSource;
    private AudioClip ballHit, barrierHit, goalClip, winClip, loseClip, drawClip;

    /// <summary>
    /// Enum containing the Final Result of the game
    /// </summary>
    public enum FinalResult
    {
        Win,
        Draw,
        Lost
    }


    public bool isGameOver = false;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Text player2ScoreText;
    [SerializeField] private Text matchTimeText;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text gameOverText;

    public static float matchTime;
    public static int maxScore;

    // static instance of this GameManager class
    private static Game instance;

    private void Start()
    {
        instance = this;

        // find the audio source in the hierarchy
        audioSource = GetComponentInChildren<AudioSource>();

        // load all the audioclips from the Resources Audio Folder
        ballHit = Resources.Load<AudioClip>("Sounds/ballHit");
        barrierHit = Resources.Load<AudioClip>("Sounds/barrierHit");
        goalClip = Resources.Load<AudioClip>("Sounds/goal");
        winClip = Resources.Load<AudioClip>("Sounds/win");
        loseClip = Resources.Load<AudioClip>("Sounds/lose");
        drawClip = Resources.Load<AudioClip>("Sounds/draw");

        // try to get the saved values from the system, if not use the default value
        matchTime = PlayerPrefs.GetFloat("matchTime", 1f);
        maxScore = PlayerPrefs.GetInt("maxScore", 2);

        // countdown of 3 seconds before starting the game
        countdownTime = 3f;

        // starts the game
        StartNewGame();
    }

    private void Update()
    {
        // check if it's still on the countdown before the start
        if (isCountdown)
        {
            countdownText.gameObject.SetActive(true);
            // keep decreasing the countdown time
            countdownTime -= Time.unscaledDeltaTime;
            // check if the countdown is more then 0
            if (countdownTime > 0.5f)
            {
                // I update the text only with the integers 3,2,1
                countdownText.text = Mathf.RoundToInt(countdownTime).ToString("0");
            }
            else
            {
                // countdown is finished, reset the timescale and start the actual match
                isCountdown = false;
                countdownText.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else
        {
            // check if the matchtime is unlimited (-1) 
            if (matchTime != -1)
            {
                // if the matchtime is fixed, keep decreasing it
                currentMatchTime -= Time.deltaTime;

                // as long as you have time remaining, keep decreasing the time
                if (currentMatchTime > 0)
                {
                    UpdateMatchTime(currentMatchTime);
                }
                else
                {
                    // if the time ran out, check the score to choose the winner
                    if (!instance.isGameOver)
                    {
                        if (instance.player1Score > instance.player2Score)
                            GameOver(FinalResult.Win);
                        else if (instance.player1Score < instance.player2Score)
                            GameOver(FinalResult.Lost);
                        else
                            GameOver(FinalResult.Draw);
                    }
                }
            }
            else
            {
                // if unlimited time, set a fixed time text
                matchTimeText.text = "99:99";
            }
        }
    }

    /// <summary>
    /// Reset players, balls, scores, time, and Starts the game!
    /// </summary>
    public void StartNewGame()
    {
        // set timescale to 0 cause there are the 3s countdown before the actual start
        Time.timeScale = 0f;
        // reset all the other variables and UI texts and get ready to start
        isGameOver = false;
        gameOverScreen.SetActive(false);
        player1Score = 0;
        player2Score = 0;
        player1ScoreText.text = player1Score.ToString();
        player1ScoreText.enabled = true;
        player2ScoreText.text = player2Score.ToString();
        player2ScoreText.enabled = true;

        currentMatchTime = matchTime * 60;
        isCountdown = true;

        playerOne.transform.position = player1StartPos;
        playerTwo.transform.position = player2StartPos;
    }

    /// <summary>
    /// Game over function
    /// </summary>
    /// <param name="_FinalResult">Enum of the outcome result of the match</param>
    public static void GameOver(FinalResult _FinalResult)
    {
        Time.timeScale = 0f;
        instance.isGameOver = true;
        
        // customize the game over text and sound depending on the Final Result
        switch (_FinalResult)
        {
            case FinalResult.Win: 
                instance.gameOverText.text = "YOU WIN!";
                instance.audioSource.PlayOneShot(instance.winClip);
                break;
            case FinalResult.Lost: 
                instance.gameOverText.text = "YOU LOST!";
                instance.audioSource.PlayOneShot(instance.loseClip);
                break;
            case FinalResult.Draw: 
                instance.gameOverText.text = "DRAW!";
                instance.audioSource.PlayOneShot(instance.drawClip);
                break;
            default: instance.gameOverText.text = "GAME OVER!"; break;
        }

        instance.gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Functions that brings you back to the main menu
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Function called when the player1 scores
    /// </summary>
    public static void Player1Scored()
    {
        // play a goal audio clip
        instance.audioSource.PlayOneShot(instance.goalClip);
        // increase player1 score
        instance.player1Score++;
        instance.player1ScoreText.text = instance.player1Score.ToString();
        // check if the player1 score is the maximum score
        if (instance.player1Score == maxScore)
        {
            // if it's the score limit, it's game over
            GameOver(FinalResult.Win);
        }
        else
        {
            // if not, reset the countdown timer before restart the match
            instance.countdownTime = 3f;
            instance.isCountdown = true;
        }
    }

    /// <summary>
    /// Function called when the player2 scores
    /// </summary>
    public static void Player2Scored()
    {
        instance.audioSource.PlayOneShot(instance.goalClip);
        instance.player2Score++;
        instance.player2ScoreText.text = instance.player2Score.ToString();
        if (instance.player2Score == maxScore)
        {
            GameOver(FinalResult.Lost);
        }
        else
        {
            instance.countdownTime = 3f;
            instance.isCountdown = true;
        }
    }

    /// <summary>
    /// Update the match time, including UI text
    /// </summary>
    /// <param name="_currentMatchTime"></param>
    private void UpdateMatchTime(float _currentMatchTime)
    {
        // converts the time in seconds in minutes and seconds
        int minutes = Mathf.FloorToInt(_currentMatchTime / 60f);
        int seconds = Mathf.RoundToInt(_currentMatchTime % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        matchTimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    /// <summary>
    /// Function to return the player1
    /// </summary>
    public GameObject GetPlayerOne()
    {
        return playerOne.gameObject;
    }

    /// <summary>
    /// Function that resets the players into their starting positions
    /// </summary>
    public static void ResetPlayers()
    {
        // reset velocity first
        instance.playerOne.GetComponent<Rigidbody>().velocity = instance.playerOne.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        instance.playerTwo.GetComponent<Rigidbody>().velocity = instance.playerTwo.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        instance.playerOne.transform.position = instance.player1StartPos;
        instance.playerTwo.transform.position = instance.player2StartPos;
    }

    /// <summary>
    /// Function that reset the ball according on who scored
    /// </summary>
    /// <param name="_hasPlayer1Scored">check if if was player1 that scored</param>
    public static void ResetBall(bool _hasPlayer1Scored)
    {
        // reset velocity first
        instance.ball.GetComponent<Rigidbody>().velocity = instance.ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // if player1 scored, spawn ball in player2 half-field
        if (_hasPlayer1Scored)
        {
            instance.ball.transform.position = instance.BallLeftPos;
            instance.ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        // else spawn the ball in player1 half-field   
        else
        {
            instance.ball.transform.position = instance.BallRightPos;
            instance.ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// Delayed reset cause you need to wait the 3 seconds ofthe countdown pre-start
    /// </summary>
    public static IEnumerator DelayedReset()
    {
        yield return new WaitForSecondsRealtime(3);

        // you don't have to wait if it's gameover
        if (!instance.isGameOver)
            Time.timeScale = 1f;
    }

    /// <summary>
    /// Play the sound on Ballhit
    /// </summary>
    public static void PlayBallHit()
    {
        instance.audioSource.PlayOneShot(instance.ballHit);
    }

    /// <summary>
    /// Play the sound on BarrierHit
    /// </summary>
    public static void PlayBarrierHit()
    {
        instance.audioSource.PlayOneShot(instance.barrierHit);
    }
}
