
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    public static RockPool rockPoolInstance;

    [SerializeField]
    private GameObject pooledSnowball;
    private bool notEnoughSnowballsInPool = true;

    private List<GameObject> snowballs;

    private void Awake()
    {
        rockPoolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        snowballs = new List<GameObject>();
    }

    public GameObject GetSnowball()
    {
        if (snowballs.Count > 0) {
            for (int i = 0; i < snowballs.Count; i++) {
                if (!snowballs[i].activeInHierarchy) {
                    return snowballs[i];
                }
            }
            notEnoughSnowballsInPool = true;
        }

        if (notEnoughSnowballsInPool) {
            GameObject snowball = Instantiate(pooledSnowball);
            snowball.SetActive(false);
            snowballs.Add(snowball);
            return snowball;
        }

        return null;
    }
}