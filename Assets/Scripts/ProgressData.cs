using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData : MonoBehaviour
{
    public int level,coins;
    public string saveDate;

    public ProgressData(){
        int _level = level;
        int _coins = coins;
        string _saveDate = saveDate;
    }

}
