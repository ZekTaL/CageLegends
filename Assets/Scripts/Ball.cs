using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxBallSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftGoal"))
        {        
            Time.timeScale = 0f;
            Game.Player2Scored();
            Game.ResetBall(true);
            Game.ResetPlayers();
            StartCoroutine(Game.DelayedReset());
        }

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
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Game.PlayBarrierHit();
        }

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Game.PlayBallHit();
        }
    }
}
