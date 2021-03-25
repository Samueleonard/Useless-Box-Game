using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
handles the economy side of the game, including tracking current powerups,
the panel itself, and applying boosts
*/
public class Economy : MonoBehaviour
{

    public item[] items;
    public TMPro.TMP_Text statusText;
    public bool activePwrup;

    public GameObject robot;

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
        else if(activePwrup)  //if we already have a powerup
            statusText.text = "Powerup Active Already. Try Again Soon.";
        else //no active powerups but we dont have enough coins
            statusText.text = "Not enough coins.";
    }

    //all powerups are ienumators so that we can apply a delay (duration)

    public IEnumerator CoinBoost(float wait)
    {
        int orig = GetComponent<GameManager>().coinBonus; //original
        GetComponent<GameManager>().coinBonus = 5; // add boost
        yield return new WaitForSeconds(wait);
        GetComponent<GameManager>().coinBonus = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }

    public IEnumerator RobotFreeze(float wait) //increase delay to huge amount
    {
        float orig = robot.GetComponent<Robot>().delay; //original
        robot.GetComponent<Robot>().delay = 2000; // add boost
        yield return new WaitForSeconds(wait);
        robot.GetComponent<Robot>().delay = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }

    public IEnumerator RobotSlow(float wait) //slow speed by 50%
    {
        float orig = robot.GetComponent<PathManager>().moveSpeed; //original
        robot.GetComponent<PathManager>().moveSpeed = orig * 0.5f; // add boost
        yield return new WaitForSeconds(wait);
        robot.GetComponent<PathManager>().moveSpeed = orig; // remove boost
        statusText.text = "";
        activePwrup = false;
    }
}
