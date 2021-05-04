using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float speed = 30;
    [SerializeField] private Rigidbody ball;

    private Vector3 defensePosition = new Vector3(93f, 0f, 25f);
    private Vector3 middlePosition = new Vector3(75f, 0f, 25f);
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
        // so i dont always aim at the center
        Vector3 offsetFromTarget = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        if (isAttacking)
        {
            AttackTheBall(offsetFromTarget);
        }

        if (isDefending)
        {
            BackToDefend(offsetFromTarget);
        }
    }

    private void AttackTheBall(Vector3 _offsetFromTarget)
    {

        rb.velocity = ((ball.transform.position + _offsetFromTarget) - rb.transform.position).normalized * speed;
    }

    private void BackToDefend(Vector3 _offsetFromTarget)
    {
        Vector3 targetPos = ball.position.x < 70 
            ? attackPosition 
            : ball.position.x < 50 
                ? middlePosition
                : defensePosition;

        rb.velocity = ((targetPos + _offsetFromTarget) - rb.transform.position).normalized * speed;

        if (Vector3.Distance(rb.transform.position, targetPos + _offsetFromTarget) < 3)
        {
            // back to attack
            isAttacking = true;
            isDefending = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // swap to defense
            isAttacking = false;
            isDefending = true;
        }
    }
}
