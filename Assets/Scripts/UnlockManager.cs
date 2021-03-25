using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public Button level01Button,level02Button,level03Button,level04Button,level05Button;
    public int levelPassed;

    // Start is called before the first frame update
    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
       /*
       for i less than max levels
          gameobject.get(level + levelpassed).getcomponent<button>().interactable = false
        TODO: fix if time, below works 
        for(int i = 1; i <= levelPassed; i++){
            GameObject current = GameObject.Find("LevelSelect (" + i + ")");
            //Debug.Log(current.name);
        }
        */
        level02Button.interactable = false;
        level03Button.interactable = false;
        level04Button.interactable = false;
        level05Button.interactable = false;

        switch(levelPassed){
            case 1:
                level02Button.interactable = true;
                break;
            case 2:
                level02Button.interactable = true;
                level03Button.interactable = true;
                break;
            case 3:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                break;
            case 4:
                level02Button.interactable = true;
                level03Button.interactable = true;
                level04Button.interactable = true;
                level05Button.interactable = true;
                break;
        }
    }
}
