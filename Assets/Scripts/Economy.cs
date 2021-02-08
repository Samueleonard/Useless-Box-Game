using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economy : MonoBehaviour
{

    public item[] items;
    public TMPro.TMP_Text statusText;
    public bool activePwrup;

    public void Buy(string purchased, float duration, int cost){
        if(cost <= GetComponent<GameManager>().coins && !activePwrup) //if we have enough to buy and dont already have a pwr up
        {
            GetComponent<GameManager>().coins -= cost;
            statusText.text = ("bought " + purchased + " for " + duration + " seconds");
            activePwrup = true;
            if(purchased == "Coin Boost")
                StartCoroutine(CoinBoost(duration));
            if(purchased == "Robot Freeze")
                StartCoroutine(RobotFreeze(duration));
            if(purchased == "Robot Slow")
                StartCoroutine(RobotSlow(duration));
        }
        else if(activePwrup)
            statusText.text = "Powerup Active Already. Try Again Soon.";
        else
            statusText.text = "Not enough coins.";
    }

    public IEnumerator CoinBoost(float wait)
    {
        int orig = GetComponent<GameManager>().coinBonus;
        GetComponent<GameManager>().coinBonus = 5; // add boost
        yield return new WaitForSeconds(wait);
        GetComponent<GameManager>().coinBonus = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }

    public IEnumerator RobotFreeze(float wait)
    {
        //int orig = GetComponent<GameManager>().coinBonus;
        //GetComponent<GameManager>().coinBonus = 5; // add boost
        yield return new WaitForSeconds(wait);
        //GetComponent<GameManager>().coinBonus = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }

    public IEnumerator RobotSlow(float wait)
    {
        //int orig = GetComponent<GameManager>().coinBonus;
        //GetComponent<GameManager>().coinBonus = 5; // add boost
        yield return new WaitForSeconds(wait);
        //GetComponent<GameManager>().coinBonus = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }
}
