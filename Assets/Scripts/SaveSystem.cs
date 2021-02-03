using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SaveSystem : MonoBehaviour
{
    // Makes it a singleton / single instance
    static public SaveSystem instance;
    int saveAmount = 0; //HOW MANY SAVES ARE CURRENTLY SAVED
    private void Awake()
    {
        // Check there are no other instances of this class in the scene
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

    }

    public void SaveGame(ProgressData saveData)
    {
        saveAmount++;
        string filePath = Application.persistentDataPath + "/save" + saveAmount + ".data";
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public void LoadGame(int saveAmount)
    {
        string filePath = Application.persistentDataPath + "/save" + saveAmount + ".data";
        if(File.Exists(filePath))
        {
            // File exists 
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            ProgressData saveData = converter.Deserialize(dataStream) as ProgressData;

            dataStream.Close();

            PlayerPrefs.SetInt("Coins", saveData.coins);
            PlayerPrefs.SetInt("Level", saveData.level);
            PlayerPrefs.SetInt("Coins", saveData.coins);
        }
        else
        {
            // File does not exist
            Debug.LogError("Save file not found in " + filePath);
        }
  }
}

