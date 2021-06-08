using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains the methods related to the ball
/// </summary>
public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private float maxBallSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // make sure the ball doesn't exceed the maximum speed allowed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxBallSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the ball enters in the LeftGoal it means the player2 has scored
        if (other.CompareTag("LeftGoal"))
        {        
            Time.timeScale = 0f;
            // increase player2 score
            Game.Player2Scored();
            // reset the ball
            Game.ResetBall(true);
            // reset the players
            Game.ResetPlayers();
            // Delayed reset beacuse there are 3 seconds of countdown before restart
            StartCoroutine(Game.DelayedReset());
        }

        // If the ball enters the RightGoal it means the player1 has scored
        if (other.CompareTag("RightGoal"))
        {
            Time.timeScale = 0f;
            Game.Player1Scored();
            Game.ResetBall(false);
            Game.ResetPlayers();
            StartCoroutine(Game.DelayedReset());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ball hits the barrier will play the barrierHit clip audio
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Game.PlayBarrierHit();
        }

        // if the ball is hit by one of the players, play the ballHit audio clip
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Game.PlayBallHit();
        }
    }
}
