using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {
    private static GameMaster instance;
    public static GameMaster _instance_
    {
        get
        {
            if (instance == null)
            {
                if (GameObject.FindObjectOfType(typeof(GameMaster)) != null)
                {
                    instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;

                }
                else
                {
                    GameObject temp = new GameObject();
                    temp.name = "~gameMaster";
                    temp.AddComponent<GameMaster>();
                    temp.tag = "gameMaster";
                    temp.isStatic = true;
                    DontDestroyOnLoad(temp);
                    instance = temp.GetComponent<GameMaster>();

                }
            }
            return instance;
        }
    }
    public string levelSelected=null;
    public string levelShop = null;
    public int playerDoubloons = 0;
    public string presetName = " ";
    public int maxLevel;
    public float[] settingNumbers=new float[8];
   
    private void Awake()
    {
        SetUpGameMaster();
    }
  
    /// <summary>
    /// Call this function at every "Menu/Scene Controller"
    /// When in doubt just call this function in start 
    /// 
    /// </summary>
    public static void init()
    {
        if (GameObject.FindObjectOfType(typeof(GameMaster)) != null)
        {
            instance = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;

        }
        else
        {
            GameObject temp = new GameObject();
            temp.name = "~gameMaster";
            temp.AddComponent<GameMaster>();
            temp.tag = "gameMaster";
            temp.isStatic = true;
            DontDestroyOnLoad(temp);
            instance = temp.GetComponent<GameMaster>();
        }
    }

    private void SetUpGameMaster()
    {
        ZPlayerPrefs.Initialize("ZombieTreasureIsland", "pw456239bpdfk");
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        ZPlayerPrefs.Save();
    }
}
