using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*
the 'main' script
handles the games state, including tracking coins, 
switches flicked, and handling all of the menus.

*/

public class GameManager : MonoBehaviour
{
    #region variables

    public int currentFlicked; //how many switches are currently flicked
    private int winFlicked; //how many need to be flicked to win
    public bool paused = false;
     //how much time has elapsed since starting level
    public TMP_Text timeWonText; //how much time level took - showed on level won screen

    public Transform helpPanel;
    public Transform pausePanel;
    public Transform quitPanel;
    public Transform levelWonPanel;
    public Transform economyPanel;
    public Transform goToMenuPanel;

    public TMP_Text timeText;
    public TMP_Text levelText;
    public TMP_Text coinText;

    public int coins;
    public int coinBonus = 1;

    public Button saveButton;

    public Text economyCoinText;

    public GameObject saveManager;

    public float time = 0;

    public bool tutorialPassed;
#endregion
    
    private void Start() {
        coins = PlayerPrefs.GetInt("Coins");
        winFlicked = (GameObject.Find("Switches").transform.childCount / 2); //half because of the green/red child with every switch
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        saveButton.GetComponent<Button>().onClick.AddListener(delegate { saveManager.GetComponent<SaveSystem>().SaveGame(); });
        Application.targetFrameRate = PlayerPrefs.GetInt("FPSLimit");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = Mathf.Round(time).ToString() + " secs";
        coinText.text = "Coins : " + coins.ToString();
        economyCoinText.text = "Coins : " + coins.ToString();
        if(currentFlicked == winFlicked && !paused){
            timeText.text = "";
            GameObject.Find("ComputerRobot").GetComponent<Robot>().enabled = false;
            coinText.enabled = false;
            pausePanel.gameObject.SetActive(false);
            helpPanel.gameObject.SetActive(false);
            levelWonPanel.gameObject.SetActive(true);
            timeText.enabled = false;
            timeWonText.text = "Completed in " + Mathf.Round(time) + " Seconds";
            economyPanel.gameObject.SetActive(false);
            levelControl.instance.Win();
            Camera.main.GetComponent<SwitchClick>().enabled = false;
            coins += (10*coinBonus);
            PlayerPrefs.SetInt("Coins", coins);
            PauseGame();

        }

        //pressing certain keys will open menus/panels
        if(Input.GetKeyDown(KeyCode.I)  && !pausePanel.gameObject.activeSelf && !economyPanel.gameObject.activeSelf)
            toggleHelpMenu();
        if(Input.GetKeyDown(KeyCode.Escape))
            togglePauseMenu();
        if(Input.GetKeyDown(KeyCode.E) && !pausePanel.gameObject.activeSelf && !helpPanel.gameObject.activeSelf)
            toggleEconomyMenu();
        
        if(paused)
            PauseGame();
        else
            ResumeGame();

        //prevent coins + flicked from going below zero, just incase
        if(coins < 0)
            coins = 0;
        
        if(currentFlicked < 0)
            currentFlicked = 0;
        
    }

    public void PauseGame(){
        paused = true;
        Time.timeScale = 0;
        Camera.main.GetComponent<SwitchClick>().enabled = false;
    }

    public void ResumeGame(){
        paused = false;
        Time.timeScale = 1;
        helpPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        economyPanel.gameObject.SetActive(false);
        Camera.main.GetComponent<SwitchClick>().enabled = true;
        goToMenuPanel.gameObject.SetActive(false);
    }

    void toggleHelpMenu(){
        paused = !paused;
        helpPanel.gameObject.SetActive(!helpPanel.gameObject.activeSelf);
    }

    void togglePauseMenu(){
        paused = !paused;
        economyPanel.gameObject.SetActive(false);
        helpPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
    }

    void toggleEconomyMenu(){
        paused = !paused;
        coinText.enabled = !coinText.isActiveAndEnabled;
        Camera.main.GetComponent<SwitchClick>().enabled = !Camera.main.GetComponent<SwitchClick>().isActiveAndEnabled;
        economyPanel.gameObject.SetActive(!economyPanel.gameObject.activeSelf);
    }

    public void showQuit(){
        pausePanel.gameObject.SetActive(false);
        levelWonPanel.gameObject.SetActive(false);
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

    public void ToggleGoToMenuPanel(){
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
        goToMenuPanel.gameObject.SetActive(!goToMenuPanel.gameObject.activeSelf); //if on, off vice versa
    }

}
