using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    public GameController gc;
    public PlayerMovement pm;
    [SerializeField] private AudioSource getUp;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void AddDamage(){
        Debug.Log("damage+");
        pm.baseDamage++;
        gc.clearUpgrades();
    }

    public void AddHealth(){
        Debug.Log("health+");
        pm.playerHealth++;
        gc.clearUpgrades();
    }

    public void AddSpeed(){
        Debug.Log("speed+");
        pm.playerSpeed++;
        gc.clearUpgrades();
    }
}
