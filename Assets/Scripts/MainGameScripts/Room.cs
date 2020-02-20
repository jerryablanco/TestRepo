using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Breakable[] breakables;
    public GameObject virtualCamera;


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            //Debug.Log("entered this gameobject" + this.gameObject.name);
            ChangeEnemyActivation(true); //activate enemies
            ChangeBreakablesActivation(true); //activate pots
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            
            ChangeEnemyActivation(false); //deactivate enemies
            ChangeBreakablesActivation(false); //deactivate pots
            virtualCamera.SetActive(false);
        }
    }

    protected void ChangeEnemyActivation(bool activation)
    {
        for (int i = 0; i <  enemies.Length; i++) {
            ChangeActivation(enemies[i], activation);
        }
    }

    protected void ChangeBreakablesActivation(bool activation)
    {
        for (int i = 0; i < breakables.Length; i++) {
            ChangeActivation(breakables[i], activation);
        }
    }

    protected void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
