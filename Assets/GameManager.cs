using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentFlicked; //how many switches are currently flicked
    public int winFlicked; //how many need to be flicked to win


    // Update is called once per frame
    void Update()
    {
        if(currentFlicked == winFlicked){
            Debug.Log("Level Won");
            PauseGame();
        }
    }

    void PauseGame(){
        Time.timeScale = 0;
    }

    void ResumeGame(){
        Time.timeScale = 1;
    }
}
