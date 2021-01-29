using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelControl : MonoBehaviour
{

    public static levelControl instance = null;
    [HideInInspector]
    public int sceneIndex, levelPassed;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }

    // Update is called once per frame
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
