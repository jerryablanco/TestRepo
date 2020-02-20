using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    private int activeEnemyCount;
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            //Debug.Log("entered this gameobject" + this.gameObject.name);
            ChangeEnemyActivation(true); //activate enemies
            ChangeBreakablesActivation(true); //activate pots
            virtualCamera.SetActive(true);
            CloseDoors();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) {

            ChangeEnemyActivation(false); //deactivate enemies
            ChangeBreakablesActivation(false); //deactivate pots
            virtualCamera.SetActive(false);
        }
    }

    public void RoomEnemyDefeated()
    {
        activeEnemyCount--;
        if (activeEnemyCount <= 0) {
            OpenDoors();
        }
    }

    protected void CloseDoors()
    {
        activeEnemyCount = enemies.Length;
        Debug.Log("closed doors");
        for(int i =0; i < doors.Length; i++) {
            doors[i].Close();
        }
    }

    protected void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++) {
            doors[i].Open();
        }
    }
}
