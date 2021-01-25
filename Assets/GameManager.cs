using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int currentFlicked; //how many switches are currently flicked
    public int winFlicked; //how many need to be flicked to win
    private bool paused = false;
    public Transform helpPanel;
    public Transform pausePanel;
    public Transform quitPanel;
    public Transform levelWonPanel;

    public Text levelText;

    private void Start() {
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }
    // Update is called once per frame
    void Update()
    {
        if(currentFlicked == winFlicked){
            pausePanel.gameObject.SetActive(false);
            quitPanel.gameObject.SetActive(false);
            helpPanel.gameObject.SetActive(false);
            levelWonPanel.gameObject.SetActive(true);
            levelControl.instance.Win();
            Camera.main.GetComponent<SwitchClick>().enabled = false;
            PauseGame();

        }
        if(Input.GetKeyDown(KeyCode.I) && !paused)
            toggleHelpMenu();
        if(Input.GetKeyDown(KeyCode.Escape) && helpPanel.gameObject.activeSelf == false)
            togglePauseMenu();
        
        if(paused)
            PauseGame();
        else
            ResumeGame();
    }

    public void PauseGame(){
        paused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        paused = false;
        Time.timeScale = 1;
        helpPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
    }

    void toggleHelpMenu(){
        paused = !paused;
        helpPanel.gameObject.SetActive(!helpPanel.gameObject.activeSelf);
    }

    void togglePauseMenu(){
        paused = !paused;
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
    }

    public void showQuit(){
        pausePanel.gameObject.SetActive(false);
        quitPanel.gameObject.SetActive(true);
    }

    public void QuitDesktop(){
        Application.Quit(1);
    }

    public void QuitMenu(){
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int index){
        SceneManager.LoadScene(levelControl.instance.sceneIndex+1);
    }

}
