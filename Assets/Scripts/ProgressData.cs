using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public int level;
    public int coins;

    public ProgressData(GameManager gm, UnlockManager um){
        level = um.levelPassed;
        coins = gm.coins;
    }
}
