using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
class for storing infomation about the current save
also used to store data when loaded from savemanager
*/
[System.Serializable]
public class ProgressData : MonoBehaviour
{
    public int level,coins;
    public string saveDate;

    public ProgressData(){ //constructor
        int _level = level;
        int _coins = coins;
        string _saveDate = saveDate;
    }

}
