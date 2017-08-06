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
    [SerializeField]
    private string levelChosen=null;
    [SerializeField]
    private string curLevelShop = null;
    public int playerDoubloonsAmt = 0;
    [SerializeField]
    private string difPresetName = " ";
#pragma warning disable 649
    [SerializeField]
    private int lastLevel;
    [SerializeField]
    private List<int> levelsUnlocked;
    [SerializeField]
    private float[] difSettingNumbers=new float[8];
    [SerializeField]
    private AudioClip[] levelMusic;
    [SerializeField]
    private AudioClip menuMusic;
#pragma warning restore 649
    /// <summary>
    /// Get/set the current level chosen
    /// </summary>
    public string levelSelected
    {
        get { return levelChosen; }
        set { levelChosen = value; }
    }
    
    /// <summary>
    /// Get/set the current level shop
    /// </summary>
    public string levelShop
    {
        get { return curLevelShop; }
        set { curLevelShop = value; }
    }

    /// <summary>
    /// Gets/set the players doubloons
    /// </summary>
    public int playerDoubloons
    {
        get { return playerDoubloonsAmt; }
        set { playerDoubloonsAmt = value; }
    }

    /// <summary>
    /// Get/set the current difficult preset name
    /// </summary>
    public string presetName
    {
        get { return difPresetName; }
        set { difPresetName = value; }
    }

    /// <summary>
    /// Gets the array of settings values
    /// </summary>
    public float[] settingNumbers
    {
        get { return difSettingNumbers; }
    }

    /// <summary>
    /// Gets the list of levels currently available to the player
    /// </summary>
    public List<int> levelsAvailable
    {
        get { return levelsUnlocked; }
    }

    /// <summary>
    /// Gets the last level
    /// </summary>
    public int maxLevel
    {
        get { return lastLevel; }
    }

    /// <summary>
    /// Gets the array of available audio clips for levels
    /// </summary>
    public AudioClip[] levelAudio
    {
        get { return levelMusic; }
    }

    /// <summary>
    /// Gets the menu audio clip
    /// </summary>
    public AudioClip menuAudio
    {
        get { return menuMusic; }
    } 

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

    public void OnLevelWasLoaded(int level)
    {
        int levelLoaded = 0;
        int.TryParse(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(level).name, out levelLoaded);
        if (levelLoaded > 0)
        {
            gameObject.GetComponent<AudioSource>().clip = GameMaster._instance_.levelAudio[levelLoaded - 1];
            gameObject.GetComponent<AudioSource>().Play();

        }
        else
        {
            if (gameObject.GetComponent<AudioSource>().clip != menuMusic)
            {
                gameObject.GetComponent<AudioSource>().clip = menuMusic;
                gameObject.GetComponent<AudioSource>().Play();
            }
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(level).name == "levelSelect")
            {
                levelSelected = "1";

                GameObject levelDropdown = GameObject.Find("LevelSelectDropdown");

                List<UnityEngine.UI.Dropdown.OptionData> LevelsAvailable = new List<UnityEngine.UI.Dropdown.OptionData>();
                for (int i = 0; i < levelsUnlocked.Count; i++)
                {
                    LevelsAvailable.Add(new UnityEngine.UI.Dropdown.OptionData(levelsUnlocked[0].ToString()));
                }
                if (levelsUnlocked.Count == 0)
                {
                    LevelsAvailable.Add(new UnityEngine.UI.Dropdown.OptionData("1"));
                }
                levelDropdown.GetComponent<UnityEngine.UI.Dropdown>().options = LevelsAvailable;
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(level).name == "levelCleared")
            {
                int currentLevel = 0;
                int.TryParse(levelChosen, out currentLevel);
                if (currentLevel == lastLevel)
                {
                    GameObject.Find("nextLevel").SetActive(false);
                }
            }
                
            
        }
    }

    private void OnApplicationQuit()
    {
        ZPlayerPrefs.SetInt("PlayerDoubloons", playerDoubloonsAmt);
        ZPlayerPrefs.Save();
    }
}
