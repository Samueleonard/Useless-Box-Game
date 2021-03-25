using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

*/
public class levelControl : MonoBehaviour
{

    public static levelControl instance = null;
    [HideInInspector]
    public int sceneIndex, levelPassed;

    // prevent 'double loading' bug and initialise levelcontrol management
    void Start()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

    // if we complete the final level, send us to the menu, if not, load next level
    public void Win()
    {
        if(sceneIndex == 5)
            SceneManager.LoadScene("Main Menu");
        else{
            if(levelPassed < sceneIndex)
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);
        }
    }
}
