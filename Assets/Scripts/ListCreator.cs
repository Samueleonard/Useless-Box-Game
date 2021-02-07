using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System.IO;  
using UnityEngine.UI;
using TMPro;

public class ListCreator : MonoBehaviour
{
    ///Variables for listing the directory contents    
    //A string that holds the directory path  
    private string directoryPath;   
    //A List of strings that holds the file names with their respective extensions  
    private List<string> fileNames;  
    //A string that stores the full file path  
    private string fullFilePath;  
  
    //The initial position of the scroll view  
    private Vector2 scrollPosition = Vector2.zero;  
    ////  
  
    public GameObject savePrefab;
    public RectTransform content;
    public TMP_Text text;
  
    // Use this for initialization  
    void Start ()   
    {  
        try  
        {  
            //Get the path of all files inside the directory and save them on a List  
            fileNames = new List<string>( Directory.GetFiles(Application.persistentDataPath) );  
            if(fileNames.Count == 0){
                text.text = "No Save Files found in directory. Have you saved any progress?";
            }
            //For each string in the fileNames List   
            for (int i = 0; i < fileNames.Count; i++)  
            {  
                string saveName = Path.GetFileName(fileNames[i]).Split('.')[0];
                if(Path.GetFileName(fileNames[i]).Split('.')[1] == "data")
                {
                    //instantiate 
                    GameObject buttonPrefab = Instantiate(savePrefab);
                    buttonPrefab.transform.SetParent(content, false);
                    buttonPrefab.name = saveName;  //remove the .save from the file name
                    buttonPrefab.transform.Find("SaveNumberText").GetComponent<TMP_Text>().text = saveName;
                    int saveInt = int.Parse(saveName.Split('e')[1]);
buttonPrefab.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<ProgressData>().Load(saveInt) ; });                    
                }     
            }
        }  
        //Catch any of the following exceptions and store the error message at the outputMessage string  
        catch (System.Exception e)  
        {  
            text.text = e.Message;  
        }  
    }  
}
