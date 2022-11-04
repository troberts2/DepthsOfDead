using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonSerializer : MonoBehaviour
{
    public PlayerMovement pm;

    private void Awake(){
        SaveSystem.Init();
    }
    void Start(){
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Save(){
        int playerhealth = pm.playerHealth;
        int playerDamage = pm.baseDamage;
        float playerSpeed = pm.playerSpeed;
        int roomNumber = pm.roomNum;

        GameSaveData GSD = new GameSaveData{
            playerHealth = playerhealth,
            playerDamage = playerDamage,
            playerSpeed = playerSpeed,
            roomNumber = roomNumber
        };

        string json = JsonUtility.ToJson(GSD);
        SaveSystem.Save(json);

    }

    public void Load(){
        string saveString = SaveSystem.Load();
        if(saveString != null) {
            GameSaveData GSD = JsonUtility.FromJson<GameSaveData>(saveString);

            pm.playerHealth = GSD.playerHealth;
            pm.baseDamage = GSD.playerDamage;
            pm.playerSpeed = GSD.playerSpeed;
            pm.roomNum = GSD.roomNumber;
        }
    }
    private class GameSaveData {
        public int playerHealth;
        public int playerDamage;
        public float playerSpeed;
        public int roomNumber;
    }
}
