using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int currentFlicked; //how many switches are currently flicked
    private int winFlicked; //how many need to be flicked to win
    public bool paused = false;
    public Transform helpPanel;
    public Transform pausePanel;
    public Transform quitPanel;
    public Transform levelWonPanel;
    public Transform economyPanel;
    public Transform goToMenuPanel;

    public TMP_Text levelText;
    public TMP_Text coinText;
    public TMP_Text timeText; //how much time has elapsed since starting level
    public TMP_Text timeWonText; //how much time level took - showed on level won screen

    public int coins = 0;
    public int coinBonus = 1;

    public Button saveButton;

    public Text economyCoinText;

    public GameObject saveManager;

    public float time = 0;

    public bool tutorialPassed;

    private void Start() {
        winFlicked = (GameObject.Find("Switches").transform.childCount / 2); //half because of the green/red child with every switch
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        saveButton.GetComponent<Button>().onClick.AddListener(delegate { saveManager.GetComponent<SaveSystem>().SaveGame(); });
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = Mathf.Round(time).ToString() + " secs";
        coinText.text = "Coins : " + coins.ToString();
        economyCoinText.text = "Coins : " + coins.ToString();
        if(currentFlicked == winFlicked && !paused){
            coinText.enabled = false;
            pausePanel.gameObject.SetActive(false);
            helpPanel.gameObject.SetActive(false);
            levelWonPanel.gameObject.SetActive(true);
            timeWonText.text = "Completed in " + Mathf.Round(time) + " Seconds";
            economyPanel.gameObject.SetActive(false);
            levelControl.instance.Win();
            Camera.main.GetComponent<SwitchClick>().enabled = false;
            coins += (10*coinBonus);
            PauseGame();

        }
        if(Input.GetKeyDown(KeyCode.I) && !paused && economyPanel.gameObject.activeSelf == false)
            toggleHelpMenu();
        if(Input.GetKeyDown(KeyCode.Escape) && helpPanel.gameObject.activeSelf == false)
            togglePauseMenu();
        if(Input.GetKeyDown(KeyCode.E) && !paused && helpPanel.gameObject.activeSelf == false)
            toggleEconomyMenu();
        
        if(paused)
            PauseGame();
        
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
    }

    void toggleHelpMenu(){
        paused = !paused;
        helpPanel.gameObject.SetActive(!helpPanel.gameObject.activeSelf);
    }

    void togglePauseMenu(){
        paused = !paused;
        pausePanel.gameObject.SetActive(!pausePanel.gameObject.activeSelf);
    }

    void toggleEconomyMenu(){
        coinText.enabled = !coinText.isActiveAndEnabled;
        Time.timeScale = 0;
        Camera.main.GetComponent<SwitchClick>().enabled = !Camera.main.GetComponent<SwitchClick>().isActiveAndEnabled;
        economyPanel.gameObject.SetActive(!economyPanel.gameObject.activeSelf);
    }

    public void showQuit(){
        Debug.Log("show quit");
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
