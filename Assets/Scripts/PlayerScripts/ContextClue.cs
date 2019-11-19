﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{

    public GameObject contextClue;
    public bool contextActive = false;

    public void ChangeContext()
    {
        contextActive = !contextActive;
        contextClue.SetActive(contextActive);
    }
    //public void Enable()
    //{
    //    contextClue.SetActive(true);
    //}

    //public void Disable()
    //{
    //    contextClue.SetActive(false);
    //}
}
