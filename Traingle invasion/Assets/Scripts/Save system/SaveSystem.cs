using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void PlayerSaveData (ManageGame manageGame){

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playerData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSaveData data = new PlayerSaveData(manageGame);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerSaveData LoadPlayer (){

        string path = Application.persistentDataPath + "/playerData.txt";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
