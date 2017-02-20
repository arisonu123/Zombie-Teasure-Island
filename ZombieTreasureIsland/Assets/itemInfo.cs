using UnityEngine;
using System.Collections;

public class itemInfo : MonoBehaviour {
    public int cost;
    public enum modType{
      zombieHP,
      zombieSpeed,
      timeBetweenWaves,
      zombieDamage,
      zombieNum,
      playerHP,
      playerDamage,
      playerSpeed
    }
    public modType itemMod;
    public int modAmount;
    public int levelApplied;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void activateItem()
    {
        switch (itemMod)
        {
            case modType.zombieHP:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieHP.Length == 0)
                    {
                        ZPlayerPrefs.minZombieHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieHP[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZHP", ZPlayerPrefs.minZombieHP[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieHP.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieHP[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZHP", ZPlayerPrefs.maxZombieHP[levelApplied]);
                }
                break;
            case modType.zombieSpeed:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieSpeed.Length == 0)
                    {
                        ZPlayerPrefs.minZombieSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieSpeed[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZSpd", ZPlayerPrefs.minZombieSpeed[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieSpeed.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieSpeed[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZSpd", ZPlayerPrefs.maxZombieSpeed[levelApplied]);
                }
                break;
            case modType.timeBetweenWaves:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minTimeBetweenWaves.Length == 0)
                    {
                        ZPlayerPrefs.minTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minTimeBetweenWaves[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minTBWs", ZPlayerPrefs.minTimeBetweenWaves[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxTimeBetweenWaves.Length == 0)
                    {
                        ZPlayerPrefs.maxTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxTimeBetweenWaves[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxTBWs", ZPlayerPrefs.maxTimeBetweenWaves[levelApplied]);
                }
                break;
            case modType.zombieDamage:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieDamage.Length == 0)
                    {
                        ZPlayerPrefs.minZombieDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieDamage[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZD", ZPlayerPrefs.minZombieDamage[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieDamage.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieDamage[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZD", ZPlayerPrefs.maxZombieDamage[levelApplied]);
                }
                break;
            case modType.zombieNum:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieNum.Length == 0)
                    {
                        ZPlayerPrefs.minZombieNum = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieNum[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZNum", ZPlayerPrefs.minZombieNum[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieNum.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieNum = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieNum[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZNum", ZPlayerPrefs.minZombieNum[levelApplied]);
                }
                break;
            case modType.playerHP:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerHP.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerHP[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPHP", ZPlayerPrefs.minPlayerHP[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxPlayerHP.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerHP[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPHP", ZPlayerPrefs.maxPlayerHP[levelApplied]);
                }
                break;
            case modType.playerDamage:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerDamage.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerDamage[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPD", ZPlayerPrefs.minPlayerDamage[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxPlayerDamage.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerDamage[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPD", ZPlayerPrefs.maxPlayerDamage[levelApplied]);
                }
                break;
            case modType.playerSpeed:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerSpeed.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerSpeed[levelApplied] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPSpd", ZPlayerPrefs.minPlayerSpeed[levelApplied]);
                }
                else
                {
                    if (ZPlayerPrefs.maxPlayerSpeed.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerSpeed[levelApplied] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPSpd", ZPlayerPrefs.maxPlayerSpeed[levelApplied]);
                }
                break;
            default:
                break;
        }
         
    }
}
