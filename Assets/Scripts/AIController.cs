using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains the player controller and methods for the AI opponent
/// </summary>
public class AIController : MonoBehaviour
{
    [SerializeField] private float speed = 30;
    [SerializeField] private Rigidbody ball;

    /// <summary>
    /// position close to the goal line
    /// </summary>
    private Vector3 defensePosition = new Vector3(93f, 0f, 25f);
    /// <summary>
    /// position around the quarter of the field
    /// </summary>
    private Vector3 middlePosition = new Vector3(75f, 0f, 25f);
    /// <summary>
    /// position in the middle of the field
    /// </summary>
    private Vector3 attackPosition = new Vector3(50f, 0f, 25f);

    private Rigidbody rb;
    private bool isDefending = false;
    private bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // i start attacking
        isAttacking = true;
    }

    private void FixedUpdate()
    {
        // offset so you dont always aim at the center of the ball
        Vector3 offsetFromTarget = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        // If it's attacking, it moves towards the ball
        if (isAttacking)
        {
            AttackTheBall(offsetFromTarget);
        }

        // if it's defending, it moves towards a defense position
        if (isDefending)
        {
            BackToDefend(offsetFromTarget);
        }
    }

    /// <summary>
    /// Function that makes the AI moving towards the ball
    /// </summary>
    /// <param name="_offsetFromTarget">offset from the center of the ball</param>
    private void AttackTheBall(Vector3 _offsetFromTarget)
    {
        rb.velocity = ((ball.transform.position + _offsetFromTarget) - rb.transform.position).normalized * speed;
    }

    /// <summary>
    /// Function that makes the AI moving to a defense position
    /// </summary>
    /// <param name="_offsetFromTarget">offset from the center of the ball</param>
    private void BackToDefend(Vector3 _offsetFromTarget)
    {
        // switch between defense positions according to the ball position
        Vector3 targetPos = ball.position.x < 70 
            ? attackPosition 
            : ball.position.x < 50 
                ? middlePosition
                : defensePosition;

        rb.velocity = ((targetPos + _offsetFromTarget) - rb.transform.position).normalized * speed;

        // when it gets very close to the defense position, it switch back to attack the ball
        if (Vector3.Distance(rb.transform.position, targetPos + _offsetFromTarget) < 3)
        {
            isAttacking = true;
            isDefending = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // when it hit the ball it switch to defending
        if (collision.gameObject.CompareTag("Ball"))
        {
            isAttacking = false;
            isDefending = true;
        }
    }
}
