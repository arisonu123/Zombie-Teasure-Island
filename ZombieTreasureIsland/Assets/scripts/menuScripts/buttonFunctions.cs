/*Alison Taylor
  03/02/2016
  buttonFunctions-Contains functions for button events
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class buttonFunctions : MonoBehaviour
{
    private int item1;
    private int item2;
    private int item3;

    private bool playerLoaded = false;
    private bool zombieLoaded = false;
#pragma warning disable 649
    [SerializeField]
    [Header("Items")]
    [Tooltip("The item picture UI")]
    private GameObject[] items;
    [SerializeField]
    [Tooltip("The item info UI")]
    private GameObject[] itemInfo;

    private GameObject buttonPressed;
    private GameObject sliderUsing;
    private GameObject playerSettings;
    private GameObject zombieSettings;

    [Header("Difficulty settings sliders and labels GameObjects")]
    [SerializeField]
    private GameObject[] sliders;
    [SerializeField]
    private GameObject[] slidersText;

    private int[] zSlidersValue;
    private int[] pSlidersValue;

    [Header("The player's doubloons text GameObject")]
    [SerializeField]
    private GameObject coinsText;

    private GameObject replayLevelStart;
    private GameObject replayLevelDiffSelect;
    private Text presetText;
    private Text coinsTextNum;
#pragma warning restore 649
    // Use this for initialization
    private void Start()
    {
        if (items.Length >= 3)
        {
            item1 = 0;
            item2 = 1;
            item3 = 2;
        }
        if (GameObject.Find("settingsTitle") != null)
        {
            GameObject.Find("settingsTitle").GetComponent<Text>().text = GameMaster._instance_.presetName;
        }
        if (GameObject.Find("currentLevel") != null)//set level selected text
        {
            GameObject.Find("currentLevel").GetComponent<Text>().text = "Level Selected: " + GameMaster._instance_.levelSelected;
        }
        if (SceneManager.GetActiveScene().name == "difficultySettings")
        {
            zSlidersValue = new int[5];
            zSlidersValue[0] = 100;
            zSlidersValue[1] = 100;
            zSlidersValue[2] = 100;
            zSlidersValue[3] = 100;
            zSlidersValue[4] = 100;
            pSlidersValue = new int[3];
            pSlidersValue[0] = 100;
            pSlidersValue[1] = 100;
            pSlidersValue[2] = 100;
            if (GameObject.Find("settingsTitle").GetComponent<Text>().text.Contains("Custom"))//if customs settings screen set min and max for each slider
            {
                if (ZPlayerPrefs.minZombieHP.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[0].GetComponent<Slider>().minValue += ZPlayerPrefs.minZombieHP[level];
                        sliders[0].GetComponent<Slider>().value = (int)(sliders[0].GetComponent<Slider>().maxValue - (sliders[0].GetComponent<Slider>().maxValue - sliders[0].GetComponent<Slider>().minValue));
                        zSlidersValue[0]= (int)(sliders[0].GetComponent<Slider>().maxValue - (sliders[0].GetComponent<Slider>().maxValue - sliders[0].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxZombieHP.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[0].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieHP[level];
                        sliders[0].GetComponent<Slider>().value = (int)(sliders[0].GetComponent<Slider>().maxValue - (sliders[0].GetComponent<Slider>().maxValue - sliders[0].GetComponent<Slider>().minValue));
                        zSlidersValue[0] = (int)(sliders[0].GetComponent<Slider>().maxValue - (sliders[0].GetComponent<Slider>().maxValue - sliders[0].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minZombieSpeed.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[1].GetComponent<Slider>().minValue += ZPlayerPrefs.minZombieSpeed[level];
                        sliders[1].GetComponent<Slider>().value = (int)(sliders[1].GetComponent<Slider>().maxValue - (sliders[1].GetComponent<Slider>().maxValue - sliders[1].GetComponent<Slider>().minValue));
                        zSlidersValue[1] = (int)(sliders[1].GetComponent<Slider>().maxValue - (sliders[1].GetComponent<Slider>().maxValue - sliders[1].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxZombieSpeed.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[1].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieSpeed[level];
                        sliders[1].GetComponent<Slider>().value = (int)(sliders[1].GetComponent<Slider>().maxValue - (sliders[1].GetComponent<Slider>().maxValue - sliders[1].GetComponent<Slider>().minValue));
                        zSlidersValue[1] = (int)(sliders[1].GetComponent<Slider>().maxValue - (sliders[1].GetComponent<Slider>().maxValue - sliders[1].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minTimeBetweenWaves.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[2].GetComponent<Slider>().minValue +=ZPlayerPrefs.minTimeBetweenWaves[level];
                        sliders[2].GetComponent<Slider>().value = (int)(sliders[2].GetComponent<Slider>().maxValue - (sliders[2].GetComponent<Slider>().maxValue - sliders[2].GetComponent<Slider>().minValue));
                        zSlidersValue[2] = (int)(sliders[2].GetComponent<Slider>().maxValue - (sliders[2].GetComponent<Slider>().maxValue - sliders[2].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxTimeBetweenWaves.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[2].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxTimeBetweenWaves[level];
                        sliders[2].GetComponent<Slider>().value = (int)(sliders[2].GetComponent<Slider>().maxValue - (sliders[2].GetComponent<Slider>().maxValue - sliders[2].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minZombieDamage.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[3].GetComponent<Slider>().minValue += ZPlayerPrefs.minZombieDamage[level];
                        sliders[3].GetComponent<Slider>().value = (int)(sliders[3].GetComponent<Slider>().maxValue - (sliders[3].GetComponent<Slider>().maxValue - sliders[3].GetComponent<Slider>().minValue));
                        zSlidersValue[3] = (int)(sliders[3].GetComponent<Slider>().maxValue - (sliders[3].GetComponent<Slider>().maxValue - sliders[3].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxZombieDamage.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[3].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieDamage[level];
                        sliders[3].GetComponent<Slider>().value = (int)(sliders[3].GetComponent<Slider>().maxValue - (sliders[3].GetComponent<Slider>().maxValue - sliders[3].GetComponent<Slider>().minValue));
                        zSlidersValue[3] = (int)(sliders[3].GetComponent<Slider>().maxValue - (sliders[3].GetComponent<Slider>().maxValue - sliders[3].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minZombieNum.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[4].GetComponent<Slider>().minValue += ZPlayerPrefs.minZombieNum[level];
                        sliders[4].GetComponent<Slider>().value = (int)(sliders[4].GetComponent<Slider>().maxValue - (sliders[4].GetComponent<Slider>().maxValue - sliders[4].GetComponent<Slider>().minValue));
                        zSlidersValue[4] = (int)(sliders[4].GetComponent<Slider>().maxValue - (sliders[4].GetComponent<Slider>().maxValue - sliders[4].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxZombieNum.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[4].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieNum[level];
                        sliders[4].GetComponent<Slider>().value = (int)(sliders[4].GetComponent<Slider>().maxValue - (sliders[4].GetComponent<Slider>().maxValue - sliders[4].GetComponent<Slider>().minValue));
                        zSlidersValue[4] = (int)(sliders[4].GetComponent<Slider>().maxValue - (sliders[4].GetComponent<Slider>().maxValue - sliders[4].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minPlayerHP.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[5].GetComponent<Slider>().minValue += ZPlayerPrefs.minPlayerHP[level];
                        sliders[5].GetComponent<Slider>().value = (int)(sliders[5].GetComponent<Slider>().maxValue - (sliders[5].GetComponent<Slider>().maxValue - sliders[5].GetComponent<Slider>().minValue));
                        pSlidersValue[0] = (int)(sliders[5].GetComponent<Slider>().maxValue - (sliders[5].GetComponent<Slider>().maxValue - sliders[5].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxPlayerHP.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[5].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxPlayerHP[level];
                        sliders[5].GetComponent<Slider>().value = (int)(sliders[5].GetComponent<Slider>().maxValue - (sliders[5].GetComponent<Slider>().maxValue - sliders[5].GetComponent<Slider>().minValue));
                        pSlidersValue[0] = (int)(sliders[5].GetComponent<Slider>().maxValue - (sliders[5].GetComponent<Slider>().maxValue - sliders[5].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minPlayerSpeed.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[6].GetComponent<Slider>().minValue += ZPlayerPrefs.minPlayerSpeed[level];
                        sliders[6].GetComponent<Slider>().value = (int)(sliders[6].GetComponent<Slider>().maxValue - (sliders[6].GetComponent<Slider>().maxValue - sliders[6].GetComponent<Slider>().minValue));
                        pSlidersValue[1] = (int)(sliders[6].GetComponent<Slider>().maxValue - (sliders[6].GetComponent<Slider>().maxValue - sliders[6].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxPlayerSpeed.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[6].GetComponent<Slider>().minValue += ZPlayerPrefs.maxPlayerSpeed[level];
                        sliders[6].GetComponent<Slider>().value = (int)(sliders[6].GetComponent<Slider>().maxValue - (sliders[6].GetComponent<Slider>().maxValue - sliders[6].GetComponent<Slider>().minValue));
                        pSlidersValue[1] = (int)(sliders[6].GetComponent<Slider>().maxValue - (sliders[6].GetComponent<Slider>().maxValue - sliders[6].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.minPlayerDamage.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[7].GetComponent<Slider>().minValue += ZPlayerPrefs.minPlayerDamage[level];
                        sliders[7].GetComponent<Slider>().value = (int)(sliders[7].GetComponent<Slider>().maxValue - (sliders[7].GetComponent<Slider>().maxValue - sliders[7].GetComponent<Slider>().minValue));
                        pSlidersValue[2] = (int)(sliders[7].GetComponent<Slider>().maxValue - (sliders[7].GetComponent<Slider>().maxValue - sliders[7].GetComponent<Slider>().minValue));
                    }
                }
                if (ZPlayerPrefs.maxPlayerDamage.Length != 0)
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        sliders[7].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxPlayerDamage[level];
                        sliders[7].GetComponent<Slider>().value = (int)(sliders[7].GetComponent<Slider>().maxValue - (sliders[7].GetComponent<Slider>().maxValue - sliders[7].GetComponent<Slider>().minValue));
                        pSlidersValue[2] = (int)(sliders[7].GetComponent<Slider>().maxValue - (sliders[7].GetComponent<Slider>().maxValue - sliders[7].GetComponent<Slider>().minValue));
                    }
                }
            }
        }
        if (coinsText != null)
        {
            coinsTextNum = coinsText.GetComponent<Text>();
            coinsTextNum.text = "Doubloons: "+GameMaster._instance_.playerDoubloons;
        }
        
       
    }



    /// <summary>
    /// Sets all settings based on the difficult preset button clicked, loads the difficult settings screen if a custom setting button is clicked and currently has no settings associated with it
    /// </summary>
    public void selectPreset()//activated when clicking and releasing a button over mouse
    {
        GameObject difSelect = GameObject.Find("selectedDifLevel");
        Text difSelectText = difSelect.GetComponent<Text>();
        GameObject presetButton = null;
        presetButton = EventSystem.current.currentSelectedGameObject;//if mouse is hitting object
        if (presetButton != null)
        {//sets text
            Debug.Log("button hit");
            presetText = presetButton.GetComponentInChildren<Text>();
            GameMaster._instance_.presetName = presetText.text + " Settings";
            difSelectText.text = "Currently Selected Difficulty Level:" + " " + presetText.text;
            Debug.Log("preset name is " + GameMaster._instance_.presetName);
            if (GameMaster._instance_.presetName == "Easy Settings" && presetButton != null)//easy default settings
            {
                Debug.Log("setting variables");
                for (int i = 0; i < 4; i++)//all zombie related variables and number of zombies added each wave,80 percent of base
                {

                    GameMaster._instance_.settingNumbers[i] = .80f;

                }


                for (int i = 4; i < 8; i++)//all player related variables and time between waves,120 percent of base
                {
                    GameMaster._instance_.settingNumbers[i] = 1.2f;
                  
                }
            }
            if (GameMaster._instance_.presetName == "Normal Settings" && presetButton != null)//normal default settings
            {
                Debug.Log("setting variables");
                for (int i = 0; i < 8; i++)//all variables 100 percent of base
                {
                    GameMaster._instance_.settingNumbers[i] = 1f;
                }
            }
            if (GameMaster._instance_.presetName == "Hard Settings" && presetButton != null)//easy default settings
            {
                Debug.Log("setting variables");
                for (int i = 0; i < 4; i++)//all zombie related variables and number of zombies added each wave,120 percent of base
                {
                    GameMaster._instance_.settingNumbers[i] = 1.2f;
                }
                for (int i = 4; i < 8; i++)//all player related variables and time between waves,80 percent of base
                {
                    GameMaster._instance_.settingNumbers[i] = .8f;
                   
                }
            }
            if (presetButton.gameObject.name == "Custom1" && presetButton != null)
            {
                Debug.Log("setting variables");
                if (ZPlayerPrefs.custom1Settings.Count >= 1 && GameMaster._instance_.levelSelected == "1")//if previously saved preset for custom 1 level 1
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        for (int i = 0; i < 8; i++)//set all settings based off of save data for custom settings
                        {
                            GameMaster._instance_.settingNumbers[i] =
                            ZPlayerPrefs.custom1Settings[level - 1][i];
                        }
                    }
                    Debug.Log("values: " + ZPlayerPrefs.custom1Settings[0][1]);
                    presetButton.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    if (GameObject.Find("modifyButton(1)") != null)
                    {
                        GameObject.Find("modifyButton(1)").SetActive(false);
                    }
                    if (GameObject.Find("modifyButton(2)") != null)
                    {
                        GameObject.Find("modifyButton(2)").SetActive(false);
                    }
                }
                //TODO: Add more ifs for additional levels
                else//if no save data load to allow player to set settings
                {
                    loadDifSettings();
                }
            }
            else if (presetButton.gameObject.name == "Custom2" && presetButton != null)
            {
                if (ZPlayerPrefs.custom2Settings.Count >= 1 && GameMaster._instance_.levelSelected == "1")//if previously saved preset for custom 2 level 1
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        Debug.Log("setting variables");
                        for (int i = 0; i < 8; i++)//set all settings based off of save data for custom settings
                        {
                            GameMaster._instance_.settingNumbers[i] =
                            ZPlayerPrefs.custom2Settings[level - 1][i];
                        }
                    }
                    presetButton.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    if (GameObject.Find("modifyButton") != null)
                    {
                        GameObject.Find("modifyButton").SetActive(false);
                    }
                    if (GameObject.Find("modifyButton(2)") != null)
                    {
                        GameObject.Find("modifyButton(2)").SetActive(false);
                    }
                }
                else//if no save data load to allow player to set settings
                {
                    loadDifSettings();
                }

            }
            else if (presetButton.gameObject.name == "Custom3" && presetButton != null)
            {
                if (ZPlayerPrefs.custom3Settings.Count >= 1 && GameMaster._instance_.levelSelected == "1")//if previously saved preset for custom 3 level 1
                {
                    int level;
                    int.TryParse(GameMaster._instance_.levelSelected, out level);
                    if (level != 0)
                    {
                        for (int i = 0; i < 8; i++)//set all settings based off of save data for custom settings
                        {
                            Debug.Log("setting variables");
                            GameMaster._instance_.settingNumbers[i] =
                            ZPlayerPrefs.custom3Settings[level - 1][i];
                        }
                    }
                    presetButton.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    if (GameObject.Find("modifyButton") != null)
                    {
                        GameObject.Find("modifyButton").SetActive(false);
                    }
                    if (GameObject.Find("modifyButton(1)") != null)
                    {
                        GameObject.Find("modifyButton(1)").SetActive(false);
                    }
                }
                else//if no save data load to allow player to set settings
                {
                    loadDifSettings();
                }

            }
            else
            {
                if (GameObject.Find("modifyButton") != null)
                {
                    GameObject.Find("modifyButton").SetActive(false);
                }
                if (GameObject.Find("modifyButton(1)") != null)
                {
                    GameObject.Find("modifyButton(1)").SetActive(false);
                }
                if (GameObject.Find("modifyButton(2)") != null)
                {
                    GameObject.Find("modifyButton(2)").SetActive(false);
                }
            }

        }
   }

    
    /// <summary>
    /// Loads the currently selected level
    /// </summary>
    public void loadLevel()
    {
        if (GameMaster._instance_.levelSelected == null || GameMaster._instance_.levelSelected == "")
        {
            GameMaster._instance_.levelSelected = "1";
        }
        SceneManager.LoadScene((GameMaster._instance_.levelSelected));
    }

    /// <summary>
    /// Loads the next level's difficulty selection screen based on numerical order of levels
    /// </summary>
    public void loadNextLevel()
    {
        if (replayLevelStart != null && replayLevelDiffSelect != null)
        {
            if (replayLevelStart.activeInHierarchy== true && replayLevelDiffSelect.activeInHierarchy == true)
            {
                replayLevelStart.SetActive(false);
                replayLevelDiffSelect.SetActive(false);
            }
        }
        if (GameMaster._instance_.levelSelected == null || GameMaster._instance_.levelSelected == "")
        {
            GameMaster._instance_.levelSelected = "1";
        }
        else
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                level++;
                GameMaster._instance_.levelSelected = level.ToString();
            }
        }
        SceneManager.LoadScene("difficultySelection");
    }

    /// <summary>
    /// Displays the replay options
    /// </summary>
    public void displayReplayOptions()
    {
        replayLevelDiffSelect.SetActive(true);
        replayLevelStart.SetActive(true);
    }

    /// <summary>
    /// Loads the level's shop
    /// </summary>
    public void loadLevelShop()
    {
        if (GameMaster._instance_.levelSelected == null || GameMaster._instance_.levelSelected == "")
        {
            GameMaster._instance_.levelSelected = "1";
        }
        GameMaster._instance_.levelShop = GameMaster._instance_.levelSelected + "Shop";
        SceneManager.LoadScene(GameMaster._instance_.levelShop);
    }

    /// <summary>
    /// Loads level select
    /// </summary>
    public void loadLevelSelect()//loads difficulty selection screen
    {
        if (replayLevelStart != null && replayLevelDiffSelect != null)
        {
            if (replayLevelStart.activeInHierarchy == true && replayLevelDiffSelect.activeInHierarchy == true)
            {
                replayLevelStart.SetActive(false);
                replayLevelDiffSelect.SetActive(false);
            }
        }
        SceneManager.LoadScene("levelSelect");
    }

    /// <summary>
    /// Loads the difficulty selection screen
    /// </summary>
    public void loadDifScreen()
    {
        SceneManager.LoadScene("difficultySelection");

    }

    private void loadDifSettings()//loads difficulty settings menu
    {
        buttonPressed=EventSystem.current.currentSelectedGameObject;//if mouse is hitting object
        if (buttonPressed.transform.gameObject.name != "modifyButton" && buttonPressed.transform.gameObject.name != "modifyButton(1)" ||
            buttonPressed.transform.gameObject.name != "modifyButton(2)")//if not modify button
        {
           // presetText = buttonPressed.transform.Find("Text").gameObject.GetComponentInChildren<Text>();
        }
        else
        {
           // presetText = buttonPressed.transform.parent.Find("Text").gameObject.GetComponent<Text>();
        }
        //GameMaster._instance_.presetName = presetText.text+ " Settings";
        SceneManager.LoadScene("difficultySettings");

    }

    /// <summary>
    /// Scrolls through items to the right
    /// </summary>
    public void scrollItemsRight()
    {
        //item1 position
        Vector3 item1Pos = items[item1].transform.position;
        Vector3 item1InfoPos = itemInfo[item1].transform.position;
        //item2 position
        Vector3 item2Pos = items[item2].transform.position;
        Vector3 item2InfoPos = itemInfo[item2].transform.position;
        //item3 position
        Vector3 item3Pos = items[item3].transform.position;
        Vector3 item3InfoPos = itemInfo[item3].transform.position;
        if (item1 - 1 <= items.Length - 3 && item1 - 1 != -1)

        {
            //disable item 3
            items[item3].GetComponent<Image>().enabled = false;
            itemInfo[item3].GetComponent<Text>().enabled = false;

            //item 2 becomes item 3
            itemInfo[item2].transform.position = item3InfoPos;
            items[item2].transform.position = item3Pos;
            //item 1 becomes item 2
            items[item1].transform.position = item2Pos;
            itemInfo[item1].transform.position = item2InfoPos;
            //item 1 -1 becomes item 1
            items[item1 - 1].GetComponent<Image>().enabled = true;
            itemInfo[item1 - 1].GetComponent<Text>().enabled = true;
            int temp1 = item1;
            int temp2 = item2;
            int temp3 = item3;
            //item -1 is now item 1
            item1 = item1 - 1;
            //item 2 is now item1 in array on screen
            item2 = temp1;
            //item 3 is now item 2 in array
            item3 = temp2;

        }




    }

    /// <summary>
    /// Scrolls through items to the left
    /// </summary>
    public void scrollItemsLeft()
    {
        //item1 position
        Vector3 item1Pos = items[item1].transform.position;
        Vector3 item1InfoPos = itemInfo[item1].transform.position;
        //item2 position
        Vector3 item2Pos = items[item2].transform.position;
        Vector3 item2InfoPos = itemInfo[item2].transform.position;
        //item3 position
        Vector3 item3Pos = items[item3].transform.position;
        Vector3 item3InfoPos = itemInfo[item3].transform.position;
        if (item3 + 1 <= items.Length - 1)
        {
            //disable item 1
            items[item1].GetComponent<Image>().enabled = false;
            itemInfo[item1].GetComponent<Text>().enabled = false;
            //move item 2 to item 1 position
            items[item2].transform.position = item1Pos;
            itemInfo[item2].transform.position = item1InfoPos;
            //move 3 to item 2 position
            itemInfo[item3].transform.position = item2InfoPos;
            items[item3].transform.position = item2Pos;
            //enable item 3+1 in item 3's origional spot
            items[item3 + 1].GetComponent<Image>().enabled = true;
            itemInfo[item3 + 1].GetComponent<Text>().enabled = true;
            //item 4 is now item 3
            //item 3 is now item3+1 in array on screen
            int temp1 = item1;
            int temp2 = item2;
            int temp3 = item3;
            item3 = item3 + 1;
            //item 2 is now item 3 in array
            item2 = temp3;
            //item 1 is now equal to item 2 in array
            item1 = temp2;

        }



    }

    /// <summary>
    /// Buys an item
    /// </summary>
    public void buyItem()
    {
        Vector3 item1BuyPos = GameObject.Find("buy1").transform.position;
        Vector3 item2BuyPos = GameObject.Find("buy2").transform.position;
        Vector3 item3BuyPos = GameObject.Find("buy3").transform.position;
     
            if (EventSystem.current.currentSelectedGameObject.name == "buy1")//if item 1's buy button was pressed
            {
                if (GameMaster._instance_.playerDoubloons >= items[item1].GetComponent<itemInfo>().cost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item1].GetComponent<itemInfo>().cost;
                    coinsTextNum.text = "Doubloons: " + GameMaster._instance_.playerDoubloons;
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;//disable button after buying item
                    EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Sold";//display sold text on button
                    items[item1].GetComponent<itemInfo>().activateItem();
                }
                else
                {
                    GameObject.Find("notEnough").GetComponent<Text>().enabled=true;
                    Invoke("disableCurrencyMsg", 2f);
                }


            }
            if (EventSystem.current.currentSelectedGameObject.name == "buy2")//if item 2's buy button was pressed
            {
                if (GameMaster._instance_.playerDoubloons >= items[item2].GetComponent<itemInfo>().cost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item2].GetComponent<itemInfo>().cost;
                    coinsTextNum.text = "Doubloons: " + GameMaster._instance_.playerDoubloons;
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;//disable button after buying item
                    EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text="Sold";//display sold text on button
                    items[item2].GetComponent<itemInfo>().activateItem();
                }
                else
                {
                    GameObject.Find("notEnough").GetComponent<Text>().enabled = true;
                    Invoke("disableCurrencyMsg", 2f);
                }


            }
            if (EventSystem.current.currentSelectedGameObject.name == "buy3")//if item 3's buy button was pressed
            {
                if (GameMaster._instance_.playerDoubloons >= items[item3].GetComponent<itemInfo>().cost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item3].GetComponent<itemInfo>().cost;
                    coinsTextNum.text = "Doubloons: "+GameMaster._instance_.playerDoubloons;
                    EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;//disable button after buying item
                    EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Sold";//display sold text on button
                    items[item3].GetComponent<itemInfo>().activateItem();
                }
                else
                {
                    GameObject.Find("notEnough").GetComponent<Text>().enabled = true;
                    Invoke("disableCurrencyMsg", 2f);
                }


            }
       }
    


    private void disableCurrencyMsg()//disable not enough msg
    {
        GameObject.Find("notEnough").GetComponent<Text>().enabled = false;
    }
    private void disableSaveMsg()//disable saved settings message
    {
        GameObject.Find("savedText").GetComponent<Text>().enabled = false;
    }

    /// <summary>
    /// Enables selected settings options
    /// </summary>
    public void enableSettings()//enable selected settings options
    {
        buttonPressed=EventSystem.current.currentSelectedGameObject;//if mouse is hitting object
        if (buttonPressed.transform.gameObject.name == "zombieButton")
        {

            playerSettings.SetActive(false);
            
            zombieSettings.SetActive(true);
            if (SceneManager.GetActiveScene().name == "difficultySettings")
            {
                int levelNum;
                int.TryParse(GameMaster._instance_.levelSelected, out levelNum);
                levelNum--;
                Debug.Log(levelNum + " level");
                Debug.Log(GameMaster._instance_.levelSelected+" is selected level");
                if (GameMaster._instance_.presetName == "Custom (1) Settings" &&
                    ZPlayerPrefs.custom1Settings.Count >= 1 &&
                    ZPlayerPrefs.custom1Settings[levelNum]
                       != null
                    )//load custom 1 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (2) Settings" &&
                    ZPlayerPrefs.custom2Settings.Count >= 1 &&
                     ZPlayerPrefs.custom2Settings[levelNum]
                      != null)//load custom 2 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (3) Settings" &&
                   ZPlayerPrefs.custom3Settings.Count >= 1 &&
                    ZPlayerPrefs.custom3Settings[levelNum]
                     != null)//load custom 3 settings for level if they exist
                {
                    loadPreset();
                }
               /* else
                {//if settings do not exist display default
                    zSlidersValue = new int[5];
                    zSlidersValue[0] = 100;
                    zSlidersValue[1] = 100;
                    zSlidersValue[2] = 100;
                    zSlidersValue[3] = 100;
                    zSlidersValue[4] = 100;
                    pSlidersValue = new int[3];
                    pSlidersValue[0] = 100;
                    pSlidersValue[1] = 100;
                    pSlidersValue[2] = 100;
                }*/
            }
        }
        else if (buttonPressed.transform.gameObject.name == "playerButton")
        {

            zombieSettings.SetActive(false);

            playerSettings.SetActive(true);
            if (SceneManager.GetActiveScene().name == "difficultySettings")
            {
                int levelNum;
                int.TryParse(GameMaster._instance_.levelSelected, out levelNum);
                levelNum--;
                if (GameMaster._instance_.presetName == "Custom (1) Settings" &&
                    ZPlayerPrefs.custom1Settings.Count >= 1 &&
                    ZPlayerPrefs.custom1Settings[levelNum]
                       != null
                    )//load custom 1 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (2) Settings" &&
                    ZPlayerPrefs.custom2Settings.Count >= 1 &&
                     ZPlayerPrefs.custom2Settings[levelNum]
                      != null)//load custom 2 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (3) Settings" &&
                    ZPlayerPrefs.custom3Settings.Count >= 1 &&
                    ZPlayerPrefs.custom3Settings[levelNum]
                     != null)//load custom 3 settings for level if they exist
                {
                    loadPreset();
                }
                /*else
                {//if settings do not exist display default
                    zSlidersValue = new int[5];
                    zSlidersValue[0] = 100;
                    zSlidersValue[1] = 100;
                    zSlidersValue[2] = 100;
                    zSlidersValue[3] = 100;
                    zSlidersValue[4] = 100;
                    pSlidersValue = new int[3];
                    pSlidersValue[0] = 100;
                    pSlidersValue[1] = 100;
                    pSlidersValue[2] = 100;
                }*/
            }
        }
          
    }


    /// <summary>
    /// Changes sliders number text as player uses the slider
    /// </summary>
    public void sliderNumbers()
    {
        sliderUsing = EventSystem.current.currentSelectedGameObject;
        //TODO:modify this so that it uses the public gameobject variables instead of gameobject.find
        if (sliderUsing.gameObject.name == "zombieHpSlider")
        {
            GameObject handleText = GameObject.Find("zHpPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            zSlidersValue[0] = (int)sliderUsing.GetComponent<Slider>().value;
        }
        if (sliderUsing.gameObject.name == "zombieSpeedSlider")
        {
            GameObject handleText = GameObject.Find("zSpdPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            zSlidersValue[1] = (int)sliderUsing.GetComponent<Slider>().value;
        }
        if (sliderUsing.gameObject.name == "Time Between Wave Spawns")
        {
            GameObject handleText = GameObject.Find("zTBWPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            zSlidersValue[2] = (int)sliderUsing.GetComponent<Slider>().value;

        }
        if (sliderUsing.gameObject.name == "zombieDamageSlider")
        {
            GameObject handleText = GameObject.Find("zDamagePercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            zSlidersValue[3] = (int)sliderUsing.GetComponent<Slider>().value;
        }
        if (sliderUsing.gameObject.name == "zombieNumSlider")
        {
            GameObject handleText = GameObject.Find("zNumPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            zSlidersValue[4] = (int)sliderUsing.GetComponent<Slider>().value;
        }
        if (sliderUsing.gameObject.name == "playerHpSlider")
        {
            GameObject handleText = GameObject.Find("pHpPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            pSlidersValue[0] = (int)sliderUsing.GetComponent<Slider>().value;

        }
        if (sliderUsing.gameObject.name == "playerSpeedSlider")
        {
            GameObject handleText = GameObject.Find("pSpeedPercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            pSlidersValue[1] = (int)sliderUsing.GetComponent<Slider>().value;
        }
        if (sliderUsing.gameObject.name == "playerDamageSlider")
        {
            GameObject handleText = GameObject.Find("pDamagePercent");
            Text sliderNum = handleText.gameObject.GetComponent<Text>();
            sliderNum.text = sliderUsing.GetComponent<Slider>().value.ToString();
            pSlidersValue[2] = (int)sliderUsing.GetComponent<Slider>().value;
        }
    }

    /// <summary>
    /// Saves current preset settings
    /// </summary>
    public void savePreset()
    {
        GameObject.Find("savedText").GetComponent<Text>().enabled = true;
        Invoke("disableSaveMsg", 2f);
        if (GameMaster._instance_.presetName=="Custom (1) Settings")//if custom 1
        {
            if (GameMaster._instance_.levelSelected == "1")//if level 1 difficulty setting
            {
                if (ZPlayerPrefs.custom1Settings.Count<1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom1Settings.Add(values);
                }
                ZPlayerPrefs.custom1Settings[0][0] = zSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom1ZHp", zSlidersValue[0]);
                ZPlayerPrefs.custom1Settings[0][1] = zSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom1ZSpd", zSlidersValue[1]);
                ZPlayerPrefs.custom1Settings[0][2] = zSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom1ZTBW", zSlidersValue[2]);
                ZPlayerPrefs.custom1Settings[0][3] = zSlidersValue[3];
                ZPlayerPrefs.SetInt("lvl1Custom1ZD", zSlidersValue[3]);
                ZPlayerPrefs.custom1Settings[0][4] = zSlidersValue[4];
                ZPlayerPrefs.SetInt("lvl1Custom1ZNInW", zSlidersValue[4]);
                ZPlayerPrefs.custom1Settings[0][5] = pSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom1PHp", pSlidersValue[0]);
                ZPlayerPrefs.custom1Settings[0][6] = pSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom1PSpd", pSlidersValue[1]);
                ZPlayerPrefs.custom1Settings[0][7] = pSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom1PD", pSlidersValue[2]);
            }
            //TODO:Add more ifs for each level
           
        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")//if custom 2
        {
            if (GameMaster._instance_.levelSelected == "1")//if level one difficulty settings
            {
                if (ZPlayerPrefs.custom2Settings.Count<1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom2Settings.Add(values);
                }
                ZPlayerPrefs.custom2Settings[0][0] = zSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom2ZHp", zSlidersValue[0]);
                ZPlayerPrefs.custom2Settings[0][1] = zSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom2ZSpd", zSlidersValue[1]);
                ZPlayerPrefs.custom2Settings[0][2] = zSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom2ZTBW", zSlidersValue[2]);
                ZPlayerPrefs.custom2Settings[0][3] = zSlidersValue[3];
                ZPlayerPrefs.SetInt("lvl1Custom2ZD", zSlidersValue[3]);
                ZPlayerPrefs.custom2Settings[0][4] = zSlidersValue[4];
                ZPlayerPrefs.SetInt("lvl1Custom2ZNInW", zSlidersValue[4]);
                ZPlayerPrefs.custom2Settings[0][5] = pSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom2PHp", pSlidersValue[0]);
                ZPlayerPrefs.custom2Settings[0][6] = pSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom2PSpd", pSlidersValue[1]);
                ZPlayerPrefs.custom2Settings[0][7] = pSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom2PD", pSlidersValue[2]);
            }
        }
        if (GameMaster._instance_.presetName == "Custom (3) Settings")//if custom 3
        {
            if (GameMaster._instance_.levelSelected == "1")//if level one difficulty settings 
            {
                if (ZPlayerPrefs.custom3Settings.Count<1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom3Settings.Add(values);
                }
                ZPlayerPrefs.custom3Settings[0][0] = zSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom3ZHp", zSlidersValue[0]);
                ZPlayerPrefs.custom3Settings[0][1] = zSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom3ZSpd", zSlidersValue[1]);
                ZPlayerPrefs.custom3Settings[0][2] = zSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom3ZTBW", zSlidersValue[2]);
                ZPlayerPrefs.custom3Settings[0][3] = zSlidersValue[3];
                ZPlayerPrefs.SetInt("lvl1Custom3ZD", zSlidersValue[3]);
                ZPlayerPrefs.custom3Settings[0][4] = zSlidersValue[4];
                ZPlayerPrefs.SetInt("lvl1Custom3ZNInW", zSlidersValue[4]);
                ZPlayerPrefs.custom3Settings[0][5] = pSlidersValue[0];
                ZPlayerPrefs.SetInt("lvl1Custom3PHp", pSlidersValue[0]);
                ZPlayerPrefs.custom3Settings[0][6] = pSlidersValue[1];
                ZPlayerPrefs.SetInt("lvl1Custom3PSpd", pSlidersValue[1]);
                ZPlayerPrefs.custom3Settings[0][7] = pSlidersValue[2];
                ZPlayerPrefs.SetInt("lvl1Custom3PD", pSlidersValue[2]);
            }
        }
    }

    private void loadPreset()//called in start to load preset information
    {
        //TODO:Add more ifs for future levels
        Debug.Log(GameMaster._instance_.presetName + " level " + GameMaster._instance_.levelSelected);
        if (GameMaster._instance_.presetName == "Custom (1) Settings" && GameMaster._instance_.levelSelected == "1")//if level 1 and custom 1 preset selected load settings
        {
            //zombie custom 1 level 1 settings
            if (zombieSettings.activeInHierarchy == true&&zombieLoaded==false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom1Settings[0][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom1Settings[0][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];
                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] = ZPlayerPrefs.custom1Settings[0][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom1Settings[0][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom1Settings[0][4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }

            //player custom 1 level 1 settings
            if (playerSettings.activeInHierarchy == true&&playerLoaded==false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom1Settings[0][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];
                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();
                pSlidersValue[1] = ZPlayerPrefs.custom1Settings[0][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];
                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom1Settings[0][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];
                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }


        }
      
        if (GameMaster._instance_.presetName == "Custom (2) Settings" && GameMaster._instance_.levelSelected == "1")//if level 1 and custom 2 preset selected load settings
        {
            //zombie custom 2 level 1 settings
            if (zombieSettings.activeInHierarchy == true && zombieLoaded == false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom2Settings[0][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom2Settings[0][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];
                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] =ZPlayerPrefs.custom2Settings[0][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom2Settings[0][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom2Settings[0][4];
                GameObject.Find("zombieNumSlider").GetComponent<Slider>().value = zSlidersValue[4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }
            //player custom 2 level 1 settings
            if (zombieSettings.activeInHierarchy == true && playerLoaded == false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom2Settings[0][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];
                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();
                pSlidersValue[1] = ZPlayerPrefs.custom2Settings[0][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];
                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom2Settings[0][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];
                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }
        }

        if (GameMaster._instance_.presetName == "Custom (3) Settings" && GameMaster._instance_.levelSelected == "1")//if level 1 and custom 3 preset selected load settings
        {
            //zombie custom 3 level 1 settings
            if (zombieSettings.activeInHierarchy == true && zombieLoaded == false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom3Settings[0][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom3Settings[0][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];
                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] = ZPlayerPrefs.custom3Settings[0][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom3Settings[0][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom3Settings[0][4];
                GameObject.Find("zombieNumSlider").GetComponent<Slider>().value = zSlidersValue[4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }


            //player custom 3 level 1 settings
            if (zombieSettings.activeInHierarchy == true && playerLoaded == false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom3Settings[0][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];
                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();
                pSlidersValue[1] = ZPlayerPrefs.custom3Settings[0][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];
                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom3Settings[0][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];
                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }
        }
       
    }
}

     


