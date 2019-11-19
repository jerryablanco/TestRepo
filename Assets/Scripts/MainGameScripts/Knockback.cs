using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit detected : " + collision.gameObject.tag + ":: and this tag: " + this.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Breakable") && this.gameObject.CompareTag("Player")) {
            collision.GetComponent<BreakPot>().Smash();
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null) {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.STAGGER;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (collision.gameObject.CompareTag("Player")) {
                    if (collision.gameObject.GetComponent<PlayerMovement>().currentState != PlayerMovement.PlayerState.STAGGER) {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerMovement.PlayerState.STAGGER;
                        collision.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
                //StartCoroutine(KnockCo(hit));
            }
        }
    }


}
