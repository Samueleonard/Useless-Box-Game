using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{

    public item[] items;

    public IEnumerator Buy(item purchased, float duration){
        Debug.Log("bought: " + purchased);
        Debug.Log("for " + duration + " seconds");
        //setbonus
        yield return new WaitForSeconds(duration);
        //resetbonus
    }
}
