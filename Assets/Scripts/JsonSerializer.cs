using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonSerializer : MonoBehaviour
{
    public GameSaveData GSD;
    public PlayerMovement pm;

    void Start(){
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Save(){
        GSD.playerHealth = pm.playerHealth;
        GSD.playerDamage = pm.baseDamage;
        GSD.playerSpeed = pm.playerSpeed;
        var convertedJson = JsonConvert.SerializeObject(GSD);
        File.WriteAllText(Application.dataPath + "/SaveData/Data.json", convertedJson);
    }

    public void Load(){
        if(File.Exists(Application.dataPath + "/SaveData/Data.json")){
            var json = File.ReadAllText(Application.dataPath + "/SaveData/Data.json");
            GSD = JsonConvert.DeserializeObject<GameSaveData>(json);
        }
        pm.UpdateValues(GSD.playerHealth, GSD.playerDamage, GSD.playerSpeed, GSD.roomNumber);
    }
}
[System.Serializable]
public class GameSaveData {
    public int playerHealth;
    public int playerDamage;
    public float playerSpeed;
    public int roomNumber;
}
