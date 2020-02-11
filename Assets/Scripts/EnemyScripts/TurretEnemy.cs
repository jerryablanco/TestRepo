using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0) {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && (currentState == EnemyState.WALKING || currentState == EnemyState.IDLE)) {
            Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (canFire) {
                Vector3 tempVector = target.transform.position - transform.position;
                GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                currentProjectile.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
                ChangeState(EnemyState.WALKING);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
            animator.SetBool("wakeUp", false);
        }
    }
}
