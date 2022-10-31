using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JSON : MonoBehaviour
{
    //public GameSaveData GSD;

    public void Save(){
        //var convertedJson = JsonConvert.SerializeObject(GSD);
        //File.WriteAllText(Application.persistentDataPath + "/SaveData/Data.json", convertedJson);
    }

    public void Load(){
        if(File.Exists(Application.dataPath + "/SaveData/Data.json"))
        {
            var json = File.ReadAllText(Application.dataPath + "/SaveData/Data.json");
        }
    }
}

//[System.Serializable]
//public class GameSaveData(){
  //  public int playerHealth;
    //public string lastScene;
//}
