using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class levelManager : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
    private float timeBetweenWaves= 10;
    [SerializeField]
    private int numberAddedPerWave = 2;
    private int wave;
    [SerializeField]
    [Header("Wave Settings")]
    [Tooltip("The total number of waves")]
    private int totalWaves;

    private float timeSinceSpawn = 0;
    [SerializeField]
    [Header("Zombie spawn settings")]
    [Tooltip("The array of locations to spawn zombies at")]
    private Transform[] zombieSpawns;
    private bool spawning;
    [SerializeField]
    [Tooltip("Prefab of the zombie to spawn")]
    private GameObject zombiePrefab;
#pragma warning restore 649
    // Use this for initialization
    private void Start () {
        //Make time between waves and number of zombies added per wave the percent of levels base set in difficulty settings
        if (GameMaster._instance_.presetName == "Easy Settings" || GameMaster._instance_.presetName == "Normal Settings" ||
            GameMaster._instance_.presetName == "Hard Settings")//if default preset setting
        {
            timeBetweenWaves = timeBetweenWaves * GameMaster._instance_.settingNumbers[2];//set corresponding level settings
            numberAddedPerWave =numberAddedPerWave * (int)GameMaster._instance_.settingNumbers[4];
        }
        else if (GameMaster._instance_.presetName == "Custom (1) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 1 settings for level to set time between waves and number of zombies added per wave the percent of levels base set in difficulty settings
                timeBetweenWaves = timeBetweenWaves * ZPlayerPrefs.custom1Settings[level-1][2]*0.01f;//set corresponding level settings
                numberAddedPerWave = numberAddedPerWave * (int)(ZPlayerPrefs.custom1Settings[level-1][4]*0.01f);
            }
            
          

        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 2 settings for level to set time between waves and number of zombies added per wave the percent of levels base set in difficulty settings
                timeBetweenWaves = timeBetweenWaves * ZPlayerPrefs.custom2Settings[level - 1][2] * 0.01f;//set corresponding level settings
                numberAddedPerWave = numberAddedPerWave * (int)(ZPlayerPrefs.custom2Settings[level - 1][4] * 0.01f);

            }

        }
        else if (GameMaster._instance_.presetName == "Custom (3) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 3 settings for level to set time between waves and number of zombies added per wave the percent of levels base set in difficulty settings
                timeBetweenWaves = timeBetweenWaves * ZPlayerPrefs.custom3Settings[level - 1][2] * 0.01f;//set corresponding level settings
                numberAddedPerWave = numberAddedPerWave * (int)(ZPlayerPrefs.custom3Settings[level - 1][4] * 0.01f);
            }

        }
    }
	
	// Update is called once per frame
	private void Update () {
        if(spawning == false && Time.time >= 4 && wave == 0)
        {
            StartCoroutine("spawnWaves");
        }
        if (spawning == false&& Time.time >= timeSinceSpawn + timeBetweenWaves&&wave!=0)
        {
            StartCoroutine("spawnWaves");
        }
        if (wave == totalWaves && GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            int currentLevel = 0;
            int.TryParse(GameMaster._instance_.levelSelected, out currentLevel);
            if (currentLevel > 0){
                if (GameMaster._instance_.maxLevel > currentLevel)
                {
                    GameMaster._instance_.levelsAvailable.Add(currentLevel + 1);
                }
            }
            SceneManager.LoadScene("levelCleared");
        }
	}

    private IEnumerator spawnWaves()//spawn waves as needed
    {
        spawning = true;
        if (wave==0)//spawn first wave
        {
            wave = 1;
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            Debug.Log(level + " is current level");

            foreach (Transform spawn in zombieSpawns)
            {
                Debug.Log("entering for");
                
                for(int num=level*2;num>0;num--)
                {
                    Debug.Log("entering while, making zombies");
                    GameObject zombie=Instantiate(zombiePrefab, spawn.position, Quaternion.identity)as GameObject;
                    yield return new WaitForSeconds(1f);//wait 1 second before spawning next zombie

                }
              
            }
            timeSinceSpawn = Time.time;
           
        }


        if (Time.time >= timeSinceSpawn + timeBetweenWaves&&wave!=0&&wave!=totalWaves)//if time passed since last wave spawned is >=timeBetweenWaves spawn next wave
        {
            wave++;
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            

            foreach (Transform spawn in zombieSpawns)
            {

                for (int num = level * 2+(numberAddedPerWave^ (wave-1)); num >0; num--)
                {
                    GameObject zombie=Instantiate(zombiePrefab, spawn.position, Quaternion.identity)as GameObject;
                    yield return new WaitForSeconds(1f);//wait 1 second before spawning next zombie
                }
              
            }
            timeSinceSpawn = Time.time;


        }
        spawning = false;
        yield return null;
    }

   
}
