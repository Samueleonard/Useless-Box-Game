using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public Button level01Button, level02Button, level03Button;
    public int levelPassed;

    // Start is called before the first frame update
    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level02Button.interactable = false;
        level03Button.interactable = false;

        switch(levelPassed){
            case 1:
                level02Button.interactable = true;
                break;
            case 2:
                level02Button.interactable = true;
                level03Button.interactable = true;
                break;
        }
    }
}
