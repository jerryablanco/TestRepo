using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    Key,
    Enemey,
    Button
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;

    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public BoxCollider2D contextClueCollider;


    private void Update()
    {
        if (Input.GetButtonDown("attack")) {
            if (playerInRange && thisDoorType == DoorType.Key) {
                //does player have key//
                if(playerInventory.numberOfKeys > 0) {
                    playerInventory.numberOfKeys--;
                    Open();
                }

            }
        }
    }

    public void Open()
    {
        //turn off door sprite render
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
        contextClueCollider.enabled = false;
        //turn off door box collider
    }

    public void Close()
    {
        StartCoroutine(DoorLockDelay());//A small delay before doors lock
    }

    private IEnumerator DoorLockDelay()
    {
        yield return new WaitForSeconds(.5f);
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
        contextClueCollider.enabled = true;
    }
}
