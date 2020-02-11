using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && boundary.bounds.Contains(target.transform.position)
                && (currentState == EnemyState.WALKING || currentState == EnemyState.IDLE)) {
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            ChangeAnimator(movePosition - transform.position);
            myRigidbody.MovePosition(movePosition);
            ChangeState(EnemyState.WALKING);
            animator.SetBool("wakeUp", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius
                || !boundary.bounds.Contains(target.transform.position)
            ) {
            animator.SetBool("wakeUp", false);
        }
    }
}
