using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    protected Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.IDLE;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        animator.SetBool("wakeUp", true);
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && (currentState == EnemyState.WALKING || currentState == EnemyState.IDLE)) {
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            ChangeAnimator(movePosition - transform.position);
            myRigidbody.MovePosition(movePosition);
            ChangeState(EnemyState.WALKING);
            animator.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
            animator.SetBool("wakeUp", false);
        }
    }

    protected void ChangeAnimator(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0) {
                SetAnimatorFloat(Vector2.right);
            }
            else {
                SetAnimatorFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
            if (direction.y > 0) {
                SetAnimatorFloat(Vector2.up);
            }
            else {
                SetAnimatorFloat(Vector2.down);
            }
        }
    }

    private void SetAnimatorFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    protected void ChangeState(EnemyState newState)
    {
        if (currentState != newState) {
            currentState = newState;
        }
    }
}
