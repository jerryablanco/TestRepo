using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        WALKING,
        ATTACKING,
        INTERACTING,
        STAGGER
    }

    public PlayerState currentState;
    public float moveSpeed;
    public float jumpHeight; //Don't need following tutorial
    public bool onGround = false;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    private Rigidbody2D playerRigidbody;
    private Vector3 change;
    private Animator animator;
    public Signal playerHit;
    
    //public AudioSource audioData;

    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = 2; //hardcoded for now
        currentState = PlayerState.WALKING;
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //audioData = GetComponent<AudioSource>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
        //GameManager.Instance.State = GameState.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        //Is the player interacting with soemthing.

        if(currentState == PlayerState.INTERACTING) {
            return;
        }

        change = Vector3.zero;
        //Get x & y //GetAxisRaw goes to max value without interpolation.... 
        change.x = Input.GetAxisRaw(_horizontal);
        change.y = Input.GetAxisRaw(_vertical);

        if (Input.GetButtonDown("attack") && (currentState != PlayerState.ATTACKING && currentState != PlayerState.STAGGER)) {
            StartCoroutine(AttackCoRoutine());
        }
        else if (currentState == PlayerState.WALKING) {
            UpdateAnimationAndMove();
        }
        //else if (GameManager.Instance.isVictory()) {
        //    animator.SetBool("isVictory", true);
        //}
    }

    private IEnumerator AttackCoRoutine()
    {
        currentState = PlayerState.ATTACKING;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.INTERACTING) {
            currentState = PlayerState.WALKING;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null) {
            if (currentState != PlayerState.INTERACTING) {
                animator.SetBool("getItem", true);
                currentState = PlayerState.INTERACTING;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else {
                animator.SetBool("getItem", false);
                currentState = PlayerState.WALKING;
                recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        playerRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
        //Vector2 newPosition = new Vector2(change.x * moveSpeed * Time.deltaTime, change.y * moveSpeed * Time.deltaTime);
        //if (transform != null) {
        //    transform.Translate(newPosition);
        //}
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else {
            animator.SetBool("moving", false);
        }

    }

    ////// This was there for killing player from Cubeworld.
    //////void OnCollisionEnter2D(Collision2D collision)
    //////{
    //////    if (collision.gameObject.tag == "Enemy")
    //////    {
    //////        currentHealth -=1;

    //////        if (currentHealth <= 0) {
    //////            audioData.Play();
    //////            GameManager.Instance.enableDefeat();
    //////            Destroy(this.gameObject);
    //////        }
    //////        else {

    //////        }
    //////    }
    //////}

    public void Knock(float knockTime, float damage)
    {
        currentHealth.runTimeValue -= damage;
        playerHealthSignal.Raise();
        //Debug.LogWarning("Player got hit and hp is " + currentHealth.ToString());
        if (currentHealth.runTimeValue > 0) {

            StartCoroutine(KnockCo(knockTime, damage));
        }
        else {
            //Deathsound
            //audioData.Play();
            //GameManager.Instance.enableDefeat();
            this.gameObject.SetActive(false);
          //  Destroy(this.gameObject);
        }
    }

    private IEnumerator KnockCo(float knockTime, float damage)
    {
        playerHit.Raise();
        if (playerRigidbody != null) {
            yield return new WaitForSeconds(knockTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.WALKING;
            playerRigidbody.velocity = Vector2.zero;
        }
    }
}
