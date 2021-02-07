using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SaveSystem : MonoBehaviour
{
    int saveAmount = 0; //HOW MANY SAVES ARE CURRENTLY SAVED


    public void SaveGame()
    {
        Debug.Log("saving");
        saveAmount++;
        ProgressData saveData = new ProgressData();
        saveData.coins = GetComponent<GameManager>().coins;
        saveData.level = GetComponent<UnlockManager>().levelPassed;
        saveData.saveDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveAmount + ".data", FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public ProgressData LoadGame(int saveNumber)
    {
        Debug.Log("loading save " + saveNumber);
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveNumber + ".data", FileMode.Open);

        BinaryFormatter converter = new BinaryFormatter();
        ProgressData saveData = converter.Deserialize(dataStream) as ProgressData;

        dataStream.Close();

        ProgressData save = new ProgressData();
        save.coins = saveData.coins;
        save.level = saveData.level;
        save.saveDate = saveData.saveDate;
        
        return save;
  }
}

