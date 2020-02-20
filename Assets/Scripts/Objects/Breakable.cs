using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Animator anim;
    public LootTable myLoot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smash()
    {
        anim.SetBool("smash", true);
        MakeLoot();
        StartCoroutine(BreakCo());
    }

    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }

    private void MakeLoot()
    {
        if (myLoot != null) {
            PowerUp powerUp = myLoot.LootPowerUp();
            if (powerUp != null) {
                Instantiate(powerUp.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
