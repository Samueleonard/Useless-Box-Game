using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int currentFlicked; //how many switches are currently flicked
    public int winFlicked; //how many need to be flicked to win
    public bool paused = false;
    public Transform helpPanel;
    public Transform pausePanel;
    public Transform quitPanel;
    public Transform levelWonPanel;
    public Transform settingPanel;
    public Transform economyPanel;

    public Text levelText;
    public Text coinText;

    public int coins = 0;
    public int coinBonus = 1;

    public Button saveButton;

    private void Start() {
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        saveButton.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<ProgressData>().Save(); });
    }
    // Update is called once per frame
    void Update()
    {
        if(currentFlicked == winFlicked){
            pausePanel.gameObject.SetActive(false);
            quitPanel.gameObject.SetActive(false);
            helpPanel.gameObject.SetActive(false);
            levelWonPanel.gameObject.SetActive(true);
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
        else
            ResumeGame();
        
        coinText.text = "Coins : " + coins.ToString();
        
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
        settingPanel.gameObject.SetActive(false);
        //economyPanel.gameObject.SetActive(false);
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
        Time.timeScale = 0.1f;
        Camera.main.GetComponent<SwitchClick>().enabled = !Camera.main.GetComponent<SwitchClick>().isActiveAndEnabled;
        economyPanel.gameObject.SetActive(!economyPanel.gameObject.activeSelf);
    }
    public void showQuit(){
        pausePanel.gameObject.SetActive(false);
        quitPanel.gameObject.SetActive(true);
    }

    public void showSettings(){
        pausePanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(true);
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
