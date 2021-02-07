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
        int _level = level;
        int _coins = coins;
        string _saveDate = saveDate;
    }

    public void Save(){
        GetComponent<SaveSystem>().SaveGame();
    }

    public void Load(int saveNum){
        ProgressData loaded = GetComponent<SaveSystem>().LoadGame(saveNum);
        
        level = loaded.level;
        coins = loaded.coins;
        saveDate = loaded.saveDate;

    }
}
