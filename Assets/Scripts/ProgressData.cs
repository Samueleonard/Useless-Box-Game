using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData : MonoBehaviour
{
    [HideInInspector]
    public int level,coins;
    [HideInInspector]
    public string saveDate;

    public ProgressData(){
        level = GetComponent<UnlockManager>().levelPassed;
        coins = GetComponent<GameManager>().coins;
        //saveDate = System.DateTime.Now.ToString("yyyy-MM-dd\\Z");
    }
}
