using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Game : MonoBehaviour
{
    private int player1Score = 0;
    private Vector3 player1StartPos = new Vector3(20, 0f, 25);
    private int player2Score = 0;
    private Vector3 player2StartPos = new Vector3(80, 0f, 25);
    private Vector3 BallLeftPos = new Vector3(32, 0f, 25);
    private Vector3 BallOriginPos = new Vector3(50f, 0f, 25);
    private Vector3 BallRightPos = new Vector3(68, 0f, 25);
    private bool isCountdown = false;
    private float countdownTime = 3f;
    private float currentMatchTime = 1f;

    private AudioSource audioSource;
    private AudioClip ballHit, barrierHit, goalClip, winClip, loseClip, drawClip;

    public enum FinalResult
    {
        Win,
        Draw,
        Lost
    }


    public bool isGameOver = false;
    public static float matchTime;
    public static int maxScore;

    [SerializeField] private GameObject playerOne;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Text player2ScoreText;
    [SerializeField] private Text matchTimeText;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text gameOverText;

    private static Game instance;

    private void Start()
    {
        instance = this;

        audioSource = GetComponentInChildren<AudioSource>();

        ballHit = Resources.Load<AudioClip>("Sounds/ballHit");
        barrierHit = Resources.Load<AudioClip>("Sounds/barrierHit");
        goalClip = Resources.Load<AudioClip>("Sounds/goal");
        winClip = Resources.Load<AudioClip>("Sounds/win");
        loseClip = Resources.Load<AudioClip>("Sounds/lose");
        drawClip = Resources.Load<AudioClip>("Sounds/draw");

        matchTime = PlayerPrefs.GetFloat("matchTime", 1f);
        maxScore = PlayerPrefs.GetInt("maxScore", 2);

        countdownTime = 3f;

        StartNewGame();
    }

    private void Update()
    {
        if (isCountdown)
        {
            countdownText.gameObject.SetActive(true);
            countdownTime -= Time.unscaledDeltaTime;
            if (countdownTime > 0.5f)
            {
                countdownText.text = Mathf.RoundToInt(countdownTime).ToString("0");
            }
            else
            {
                // restart the matchTime
                isCountdown = false;
                countdownText.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else
        {
            if (matchTime != -1)
            {
                currentMatchTime -= Time.deltaTime;

                if (currentMatchTime > 0)
                {
                    UpdateMatchTime(currentMatchTime);
                }
                else
                {
                    if (!instance.isGameOver)
                    {
                        // if the time runs out, check the score
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
                matchTimeText.text = "99:99";
            }
        }
    }

    public void StartNewGame()
    {
        Time.timeScale = 0f;
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

    public static void GameOver(FinalResult _FinalResult)
    {
        Time.timeScale = 0f;
        instance.isGameOver = true;
        
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void Player1Scored()
    {
        instance.audioSource.PlayOneShot(instance.goalClip);
        instance.player1Score++;
        instance.player1ScoreText.text = instance.player1Score.ToString();
        if (instance.player1Score == maxScore)
        {
            GameOver(FinalResult.Win);
        }
        else
        {
            instance.countdownTime = 3f;
            instance.isCountdown = true;
        }
    }

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

    private void UpdateMatchTime(float _currentMatchTime)
    {
        int minutes = Mathf.FloorToInt(_currentMatchTime / 60f);
        int seconds = Mathf.RoundToInt(_currentMatchTime % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        matchTimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public GameObject GetPlayerOne()
    {
        return playerOne.gameObject;
    }

    public static void ResetPlayers()
    {
        instance.playerOne.GetComponent<Rigidbody>().velocity = instance.playerOne.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        instance.playerTwo.GetComponent<Rigidbody>().velocity = instance.playerTwo.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        instance.playerOne.transform.position = instance.player1StartPos;
        instance.playerTwo.transform.position = instance.player2StartPos;
    }

    public static void ResetBall(bool _hasPlayer1Scored)
    {
        instance.ball.GetComponent<Rigidbody>().velocity = instance.ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (_hasPlayer1Scored)
        {
            instance.ball.transform.position = instance.BallLeftPos;
            instance.ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
            
        else
        {
            instance.ball.transform.position = instance.BallRightPos;
            instance.ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public static IEnumerator DelayedReset()
    {
        yield return new WaitForSecondsRealtime(3);

        if (!instance.isGameOver)
            Time.timeScale = 1f;
    }

    public static void PlayBallHit()
    {
        instance.audioSource.PlayOneShot(instance.ballHit);
    }

    public static void PlayBarrierHit()
    {
        instance.audioSource.PlayOneShot(instance.barrierHit);
    }
}
