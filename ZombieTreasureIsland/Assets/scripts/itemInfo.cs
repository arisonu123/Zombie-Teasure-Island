using UnityEngine;
using System.Collections;

public class itemInfo : MonoBehaviour {
    [SerializeField]
    [Tooltip("Item cost")]
    private int cost;
    private enum modType{
      zombieHP,
      zombieSpeed,
      timeBetweenWaves,
      zombieDamage,
      zombieNum,
      playerHP,
      playerDamage,
      playerSpeed
    }
    [SerializeField]
    [Tooltip("The type of difficulty modification for this item")]
    private modType itemMod;
    [SerializeField]
    [Tooltip("The amount that this item allows the further modification of difficulty.Must be negative if making it easier, positive for making it harder")]
    private int modAmount;
    [SerializeField]
    [Tooltip("The level that this difficulty modification applies" )]
    private int levelApplied;
	
    /// <summary>
    /// Activates the items effects
    /// </summary>
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
                    ZPlayerPrefs.minZombieHP[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied  + "minZHP", ZPlayerPrefs.minZombieHP[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieHP.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieHP[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZHP", ZPlayerPrefs.maxZombieHP[levelApplied - 1]);
                }
                break;
            case modType.zombieSpeed:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieSpeed.Length == 0)
                    {
                        ZPlayerPrefs.minZombieSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieSpeed[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZSpd", ZPlayerPrefs.minZombieSpeed[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieSpeed.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieSpeed[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZSpd", ZPlayerPrefs.maxZombieSpeed[levelApplied - 1]);
                }
                break;
            case modType.timeBetweenWaves:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minTimeBetweenWaves.Length == 0)
                    {
                        ZPlayerPrefs.minTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minTimeBetweenWaves[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minTBWs", ZPlayerPrefs.minTimeBetweenWaves[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxTimeBetweenWaves.Length == 0)
                    {
                        ZPlayerPrefs.maxTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxTimeBetweenWaves[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxTBWs", ZPlayerPrefs.maxTimeBetweenWaves[levelApplied - 1]);
                }
                break;
            case modType.zombieDamage:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieDamage.Length == 0)
                    {
                        ZPlayerPrefs.minZombieDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieDamage[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZD", ZPlayerPrefs.minZombieDamage[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieDamage.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieDamage[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxZD", ZPlayerPrefs.maxZombieDamage[levelApplied - 1]);
                }
                break;
            case modType.zombieNum:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minZombieNum.Length == 0)
                    {
                        ZPlayerPrefs.minZombieNum = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minZombieNum[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZNum", ZPlayerPrefs.minZombieNum[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxZombieNum.Length == 0)
                    {
                        ZPlayerPrefs.maxZombieNum = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxZombieNum[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minZNum", ZPlayerPrefs.minZombieNum[levelApplied - 1]);
                }
                break;
            case modType.playerHP:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerHP.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerHP[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPHP", ZPlayerPrefs.minPlayerHP[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxPlayerHP.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerHP = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerHP[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPHP", ZPlayerPrefs.maxPlayerHP[levelApplied - 1]);
                }
                break;
            case modType.playerDamage:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerDamage.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerDamage[levelApplied - 1] -= modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPD", ZPlayerPrefs.minPlayerDamage[levelApplied - 1]);
                }
                else
                {
                    if (ZPlayerPrefs.maxPlayerDamage.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerDamage = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerDamage[levelApplied - 1] += modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPD", ZPlayerPrefs.maxPlayerDamage[levelApplied - 1]);
                }
                break;
            case modType.playerSpeed:
                if (modAmount < 0)
                {
                    if (ZPlayerPrefs.minPlayerSpeed.Length == 0)
                    {
                        ZPlayerPrefs.minPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.minPlayerSpeed[levelApplied - 1] -=modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "minPSpd", ZPlayerPrefs.minPlayerSpeed[levelApplied - 1]);
                }
                else//TODO:Alison fix the the rest of these damn things to use this method like your mod system
                {
                    if (ZPlayerPrefs.maxPlayerSpeed.Length == 0)
                    {
                        ZPlayerPrefs.maxPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                    }
                    ZPlayerPrefs.maxPlayerSpeed[levelApplied - 1] +=modAmount;
                    ZPlayerPrefs.SetInt("level" + levelApplied + "maxPSpd", ZPlayerPrefs.maxPlayerSpeed[levelApplied - 1]);
                }
                break;
            default:
                break;
        }
         
    }
    

    /// <summary>
    /// Returns the cost of this item
    /// </summary>
    /// <value>Item cost</value>
    public int itemCost
    {
        get { return cost; }
    }
}

