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
    [SerializeField]
    [Tooltip("The buttons for purchasing items")]
    private Button[] purchaseButtons;
    
    private GameObject buttonPressed;
    private GameObject sliderUsing;

    [SerializeField]
    [Tooltip("Player difficulty settings UI")]
    private GameObject playerSettings;
    [SerializeField]
    [Tooltip("Zombie difficulty settings UI")]
    private GameObject zombieSettings;

    [Header("Difficulty settings sliders and labels GameObjects")]
    [SerializeField]
    private GameObject[] sliders;
    [SerializeField]
    private GameObject[] slidersText;

    private int[] zSlidersValue= new int[5];
    private int[] pSlidersValue= new int[3];

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
        
        if (coinsText != null)
        {
            coinsTextNum = coinsText.GetComponent<Text>();
            coinsTextNum.text = "Doubloons: "+GameMaster._instance_.playerDoubloons;
        }
        
       
    }

    public void selectLevel()
    {
        GameObject difSelect = GameObject.Find("currentSelection");
        Text selectText = difSelect.GetComponent<Text>();
        GameObject levelDropdown = GameObject.Find("LevelSelectDropdown");
        
        selectText.text = "Currently Selected: " + levelDropdown.GetComponent<Dropdown>().options[levelDropdown.GetComponent<Dropdown>().value].text;
        GameMaster._instance_.levelSelected = levelDropdown.GetComponent<Dropdown>().options[levelDropdown.GetComponent<Dropdown>().value].text;
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
                int level;
                int.TryParse(GameMaster._instance_.levelSelected, out level);

                Debug.Log("setting variables");
                if (ZPlayerPrefs.custom1Settings.Count >=level && level != 0)//if previously saved preset for custom 1 for this level
                {
                    
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
                
                else//if no save data load to allow player to set settings
                {
                    loadDifSettings();
                }
            }
            else if (presetButton.gameObject.name == "Custom2" && presetButton != null)
            {
                int level;
                int.TryParse(GameMaster._instance_.levelSelected, out level);
                if (ZPlayerPrefs.custom2Settings.Count >= level && level != 0)//if previously saved preset for custom 2 for this level
                {
                    
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
                int level;
                int.TryParse(GameMaster._instance_.levelSelected, out level);
                if (ZPlayerPrefs.custom3Settings.Count >= level && level != 0)//if previously saved preset for custom 3 for this level
                {
                    
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
        if(SceneManager.GetActiveScene().name == "difficultySelection")
        {
            string DifChosenText= GameObject.Find("selectedDifLevel").GetComponent<Text>().text;
            if(DifChosenText.Length <= (DifChosenText.Substring(DifChosenText.IndexOf("Currently"), DifChosenText.IndexOf(":")).Length))
            {
                GameMaster._instance_.presetName = "Normal Settings";//if player presses start without making a selection, default to normal
                for (int i = 0; i < 8; i++)//all variables 100 percent of base
                {
                    GameMaster._instance_.settingNumbers[i] = 1f;
                }

            }
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
        int level;
        int.TryParse(GameMaster._instance_.levelSelected, out level);
        if (level != 0)
        {
            level--;
            GameMaster._instance_.AreSliderValuesSet[level] = false;
        }
        SceneManager.LoadScene("difficultySelection");

    }

    private void loadDifSettings()//loads difficulty settings menu
    {
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
            itemInfo[item3].GetComponent<Text>().enabled = false;
            items[item3].GetComponent<Image>().enabled = false;

            //make sure purchase buttons are right
            items[item2].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[2];
            items[item1].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[1];
            items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[0];

            //item 2 becomes item 3
            itemInfo[item2].transform.position = item3InfoPos;
            items[item2].transform.position = item3Pos;
           // items[item2].GetComponent<itemInfo>().buttonForPurchasing = items[item3].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item2].GetComponent<itemInfo>().isSold)
            {
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }
            //item 1 becomes item 2
            itemInfo[item1].transform.position = item2InfoPos;
            items[item1].transform.position = item2Pos;
            //items[item1].GetComponent<itemInfo>().buttonForPurchasing = items[item2].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item1].GetComponent<itemInfo>().isSold)
            {
                items[item1].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item1].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }
            //item 1 -1 becomes item 1
            items[item1 - 1].GetComponent<Image>().enabled = true;
            itemInfo[item1 - 1].GetComponent<Text>().enabled = true;
            //items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing = items[item1].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item1 -1].GetComponent<itemInfo>().isSold)
            {
                items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item1 - 1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }

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

            //make sure purchase buttons are right
            items[item2].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[0];
            items[item3].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[1];
            items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing = purchaseButtons[2];

            //move item 2 to item 1 position
            items[item2].transform.position = item1Pos;
            itemInfo[item2].transform.position = item1InfoPos;
           // items[item2].GetComponent<itemInfo>().buttonForPurchasing = items[item1].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item2].GetComponent<itemInfo>().isSold)
            {
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item2].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }
            //move 3 to item 2 position
            itemInfo[item3].transform.position = item2InfoPos;
            items[item3].transform.position = item2Pos;
           // items[item3].GetComponent<itemInfo>().buttonForPurchasing = items[item2].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item3].GetComponent<itemInfo>().isSold)
            {
                items[item3].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item3].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item3].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item3].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }
            //enable item 3+1 in item 3's origional spot
            items[item3 + 1].GetComponent<Image>().enabled = true;
            itemInfo[item3 + 1].GetComponent<Text>().enabled = true;
          //  items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing = items[item3].GetComponent<itemInfo>().buttonForPurchasing;
            if (items[item3 + 1].GetComponent<itemInfo>().isSold)
            {
                items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing.enabled = false;
                items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Sold";
            }
            else
            {
                items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing.enabled = true;
                items[item3 + 1].GetComponent<itemInfo>().buttonForPurchasing.GetComponentInChildren<Text>().text = "Buy";
            }
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
                if (GameMaster._instance_.playerDoubloons >= items[item1].GetComponent<itemInfo>().itemCost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item1].GetComponent<itemInfo>().itemCost;
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
                if (GameMaster._instance_.playerDoubloons >= items[item2].GetComponent<itemInfo>().itemCost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item2].GetComponent<itemInfo>().itemCost;
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
                if (GameMaster._instance_.playerDoubloons >= items[item3].GetComponent<itemInfo>().itemCost)
                {
                    GameMaster._instance_.playerDoubloons = GameMaster._instance_.playerDoubloons - items[item3].GetComponent<itemInfo>().itemCost;
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
    public void enableSettings()
    {
        buttonPressed=EventSystem.current.currentSelectedGameObject;//if mouse is hitting object
        if (SceneManager.GetActiveScene().name == "difficultySettings")
        {
            int levelNum;
            int.TryParse(GameMaster._instance_.levelSelected, out levelNum);
            levelNum--;

            if (!GameMaster._instance_.AreSliderValuesSet[levelNum])
            {
                //set min and max of sliders
                if (ZPlayerPrefs.minZombieHP.Length >= levelNum + 1)
                {
                    sliders[0].GetComponent<Slider>().minValue -= ZPlayerPrefs.minZombieHP[levelNum];
                }
                if (ZPlayerPrefs.maxZombieHP.Length >= levelNum + 1)
                {
                    sliders[0].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieHP[levelNum];
                }

                if (ZPlayerPrefs.minTimeBetweenWaves.Length >= levelNum + 1)
                {
                    sliders[2].GetComponent<Slider>().minValue -= ZPlayerPrefs.minTimeBetweenWaves[levelNum];
                }
                if (ZPlayerPrefs.maxTimeBetweenWaves.Length >= levelNum + 1)
                {
                    sliders[2].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxTimeBetweenWaves[levelNum];
                }

                if (ZPlayerPrefs.minZombieDamage.Length >= levelNum + 1)
                {
                    sliders[3].GetComponent<Slider>().minValue -= ZPlayerPrefs.minZombieDamage[levelNum];
                }
                if (ZPlayerPrefs.maxZombieDamage.Length >= levelNum + 1)
                {
                    sliders[3].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieDamage[levelNum];
                }

                if (ZPlayerPrefs.minZombieNum.Length >= levelNum + 1)
                {
                    sliders[4].GetComponent<Slider>().minValue -= ZPlayerPrefs.minZombieNum[levelNum];
                }
                if (ZPlayerPrefs.maxZombieHP.Length >= levelNum + 1)
                {
                    sliders[4].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxZombieNum[levelNum];
                }


                if (ZPlayerPrefs.minPlayerHP.Length >= levelNum + 1)
                {
                    sliders[5].GetComponent<Slider>().minValue -= ZPlayerPrefs.minPlayerHP[levelNum];
                }
                if (ZPlayerPrefs.maxPlayerHP.Length >= levelNum + 1)
                {
                    sliders[5].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxPlayerHP[levelNum];
                }
                if (ZPlayerPrefs.minPlayerSpeed.Length >= levelNum + 1)
                {
                    sliders[6].GetComponent<Slider>().minValue -= ZPlayerPrefs.minPlayerSpeed[levelNum];
                }
                if (ZPlayerPrefs.maxPlayerSpeed.Length >= levelNum + 1)
                {
                    sliders[6].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxPlayerSpeed[levelNum];
                }
                if (ZPlayerPrefs.minPlayerDamage.Length >= levelNum + 1)
                {
                    sliders[7].GetComponent<Slider>().minValue -= ZPlayerPrefs.minPlayerDamage[levelNum];
                }
                if (ZPlayerPrefs.maxPlayerDamage.Length >= levelNum + 1)
                {
                    sliders[7].GetComponent<Slider>().maxValue += ZPlayerPrefs.maxPlayerDamage[levelNum];
                }

                GameMaster._instance_.AreSliderValuesSet[levelNum] = true;
            }


            if (buttonPressed.transform.gameObject.name == "zombieButton")
            {
                
                Debug.Log(levelNum + " level");
                Debug.Log(GameMaster._instance_.levelSelected+" is selected level");

                playerSettings.SetActive(false);

                zombieSettings.SetActive(true);
                


                if (GameMaster._instance_.presetName == "Custom (1) Settings" &&
                    ZPlayerPrefs.custom1Settings.Count > levelNum &&
                    ZPlayerPrefs.custom1Settings[levelNum]
                       != null
                    )//load custom 1 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (2) Settings" &&
                    ZPlayerPrefs.custom2Settings.Count > levelNum &&
                     ZPlayerPrefs.custom2Settings[levelNum]
                      != null)//load custom 2 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (3) Settings" &&
                   ZPlayerPrefs.custom3Settings.Count > levelNum &&
                    ZPlayerPrefs.custom3Settings[levelNum]
                     != null)//load custom 3 settings for level if they exist
                {
                    loadPreset();
                }
               
            }
           
            else if (buttonPressed.transform.gameObject.name == "playerButton")
            {               
                zombieSettings.SetActive(false);

                playerSettings.SetActive(true);            

                if (GameMaster._instance_.presetName == "Custom (1) Settings" &&
                    ZPlayerPrefs.custom1Settings.Count > levelNum &&
                    ZPlayerPrefs.custom1Settings[levelNum]
                       != null
                    )//load custom 1 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (2) Settings" &&
                    ZPlayerPrefs.custom2Settings.Count > levelNum &&
                     ZPlayerPrefs.custom2Settings[levelNum]
                      != null)//load custom 2 settings for level if they exist
                {
                    loadPreset();
                }
                else if (GameMaster._instance_.presetName == "Custom (3) Settings" &&
                    ZPlayerPrefs.custom3Settings.Count > levelNum &&
                    ZPlayerPrefs.custom3Settings[levelNum]
                     != null)//load custom 3 settings for level if they exist
                {
                    loadPreset();
                }
               
            }
        }
          
    }


    /// <summary>
    /// Changes sliders number text as player uses the slider
    /// </summary>
    public void sliderNumbers()
    {
        sliderUsing = EventSystem.current.currentSelectedGameObject;
      
        if (sliderUsing != null)
        {
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
    }

    /// <summary>
    /// Saves current preset settings
    /// </summary>
    public void savePreset()
    {
        GameObject.Find("savedText").GetComponent<Text>().enabled = true;
        Invoke("disableSaveMsg", 2f);
        if (GameMaster._instance_.presetName == "Custom (1) Settings")//if custom 1
        {
            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;
            if (ZPlayerPrefs.custom1Settings.Count < level + 1)
            {
                while (ZPlayerPrefs.custom1Settings.Count < level + 1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom1Settings.Add(values);
                }
            }

            //Make sure that unchanged slider values get set to 100
            for(int i = 0; i < zSlidersValue.Length; i++)
            {
                if (zSlidersValue[i] == 0)
                {
                    zSlidersValue[i] = 100;
                }
            }
            for (int i = 0; i < pSlidersValue.Length; i++)
            {
                if (pSlidersValue[i] == 0)
                {
                    pSlidersValue[i] = 100;
                }
            }

            ZPlayerPrefs.custom1Settings[level][0] = zSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1ZHp", zSlidersValue[0]);
            ZPlayerPrefs.custom1Settings[level][1] = zSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1ZSpd", zSlidersValue[1]);
            ZPlayerPrefs.custom1Settings[level][2] = zSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1ZTBW", zSlidersValue[2]);
            ZPlayerPrefs.custom1Settings[level][3] = zSlidersValue[3];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1ZD", zSlidersValue[3]);
            ZPlayerPrefs.custom1Settings[level][4] = zSlidersValue[4];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1ZNInW", zSlidersValue[4]);
            ZPlayerPrefs.custom1Settings[level][5] = pSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1PHp", pSlidersValue[0]);
            ZPlayerPrefs.custom1Settings[level][6] = pSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1PSpd", pSlidersValue[1]);
            ZPlayerPrefs.custom1Settings[level][7] = pSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom1PD", pSlidersValue[2]);


        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")//if custom 2
        {
            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;
            if (ZPlayerPrefs.custom2Settings.Count < level + 1)
            {
                while (ZPlayerPrefs.custom2Settings.Count < level + 1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom2Settings.Add(values);
                }
            }


            //Make sure that unchanged slider values get set to 100
            for (int i = 0; i < zSlidersValue.Length; i++)
            {
                if (zSlidersValue[i] == 0)
                {
                    zSlidersValue[i] = 100;
                }
            }
            for (int i = 0; i < pSlidersValue.Length; i++)
            {
                if (pSlidersValue[i] == 0)
                {
                    pSlidersValue[i] = 100;
                }
            }

            ZPlayerPrefs.custom2Settings[level][0] = zSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2ZHp", zSlidersValue[0]);
            ZPlayerPrefs.custom2Settings[level][1] = zSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2ZSpd", zSlidersValue[1]);
            ZPlayerPrefs.custom2Settings[level][2] = zSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2ZTBW", zSlidersValue[2]);
            ZPlayerPrefs.custom2Settings[level][3] = zSlidersValue[3];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2ZD", zSlidersValue[3]);
            ZPlayerPrefs.custom2Settings[level][4] = zSlidersValue[4];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2ZNInW", zSlidersValue[4]);
            ZPlayerPrefs.custom2Settings[level][5] = pSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2PHp", pSlidersValue[0]);
            ZPlayerPrefs.custom2Settings[level][6] = pSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2PSpd", pSlidersValue[1]);
            ZPlayerPrefs.custom2Settings[level][7] = pSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom2PD", pSlidersValue[2]);

        }
        else if (GameMaster._instance_.presetName == "Custom (3) Settings")//if custom 3
        {
            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;
            if (ZPlayerPrefs.custom3Settings.Count < level + 1)
            {
                while (ZPlayerPrefs.custom3Settings.Count < level + 1)
                {
                    int[] values = new int[8];
                    ZPlayerPrefs.custom3Settings.Add(values);
                }
            }


            //Make sure that unchanged slider values get set to 100
            for (int i = 0; i < zSlidersValue.Length; i++)
            {
                if (zSlidersValue[i] == 0)
                {
                    zSlidersValue[i] = 100;
                }
            }
            for (int i = 0; i < pSlidersValue.Length; i++)
            {
                if (pSlidersValue[i] == 0)
                {
                    pSlidersValue[i] = 100;
                }
            }

            ZPlayerPrefs.custom3Settings[level][0] = zSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3ZHp", zSlidersValue[0]);
            ZPlayerPrefs.custom3Settings[level][1] = zSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3ZSpd", zSlidersValue[1]);
            ZPlayerPrefs.custom3Settings[level][2] = zSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3ZTBW", zSlidersValue[2]);
            ZPlayerPrefs.custom3Settings[level][3] = zSlidersValue[3];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3ZD", zSlidersValue[3]);
            ZPlayerPrefs.custom3Settings[level][4] = zSlidersValue[4];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3ZNInW", zSlidersValue[4]);
            ZPlayerPrefs.custom3Settings[0][5] = pSlidersValue[0];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3PHp", pSlidersValue[0]);
            ZPlayerPrefs.custom3Settings[level][6] = pSlidersValue[1];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3PSpd", pSlidersValue[1]);
            ZPlayerPrefs.custom3Settings[level][7] = pSlidersValue[2];
            ZPlayerPrefs.SetInt("lvl" + (level + 1) + "Custom3PD", pSlidersValue[2]);

        }
    }

    private void loadPreset()//called in start to load preset information
    {
 
        Debug.Log(GameMaster._instance_.presetName + " level " + GameMaster._instance_.levelSelected);
        if (GameMaster._instance_.presetName == "Custom (1) Settings" )//if custom 1 preset selected load settings
        {
            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;

            //zombie custom 1 level settings
            if (zombieSettings.activeInHierarchy == true&&zombieLoaded==false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom1Settings[level][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
               
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom1Settings[level][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];

                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] = ZPlayerPrefs.custom1Settings[level][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom1Settings[level][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
               
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom1Settings[level][4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
             
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }

            //player custom 1 level settings
            if (playerSettings.activeInHierarchy == true&&playerLoaded==false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom1Settings[level][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];
               
                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();
                pSlidersValue[1] = ZPlayerPrefs.custom1Settings[level][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];
               
                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom1Settings[level][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];
                
                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }


        }
      
        if (GameMaster._instance_.presetName == "Custom (2) Settings")//if custom 2 preset selected load settings
        {

            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;
            //zombie custom 2 level settings
            if (zombieSettings.activeInHierarchy == true && zombieLoaded == false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom2Settings[level][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
               
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom2Settings[level][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];
               
                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] = ZPlayerPrefs.custom2Settings[level][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom2Settings[level][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
               
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom2Settings[level][4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
                
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }

            //player custom 2 level settings
            if (playerSettings.activeInHierarchy == true && playerLoaded == false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom2Settings[level][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];

                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();

                pSlidersValue[1] = ZPlayerPrefs.custom2Settings[level][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];


                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom2Settings[level][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];

                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }


        }

        if (GameMaster._instance_.presetName == "Custom (3) Settings")//if custom 3 preset selected load settings
        {

            int level = -1;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            level--;

            //zombie custom 3 level settings
            if (zombieSettings.activeInHierarchy == true && zombieLoaded == false)
            {
                zSlidersValue[0] = ZPlayerPrefs.custom3Settings[level][0];
                sliders[0].GetComponent<Slider>().value = zSlidersValue[0];
                
                Text slider1Num = slidersText[0].gameObject.GetComponent<Text>();
                slider1Num.text = sliders[0].GetComponent<Slider>().value.ToString();
                zSlidersValue[1] = ZPlayerPrefs.custom3Settings[level][1];
                sliders[1].GetComponent<Slider>().value = zSlidersValue[1];
              
                Text slider2Num = slidersText[1].gameObject.GetComponent<Text>();
                slider2Num.text = sliders[1].GetComponent<Slider>().value.ToString();
                zSlidersValue[2] = ZPlayerPrefs.custom3Settings[level][2];
                sliders[2].GetComponent<Slider>().value = zSlidersValue[2];
                
                Text slider3Num = slidersText[2].gameObject.GetComponent<Text>();
                slider3Num.text = sliders[2].GetComponent<Slider>().value.ToString();
                zSlidersValue[3] = ZPlayerPrefs.custom3Settings[level][3];
                sliders[3].GetComponent<Slider>().value = zSlidersValue[3];
               
                Text slider4Num = slidersText[3].gameObject.GetComponent<Text>();
                slider4Num.text = sliders[3].GetComponent<Slider>().value.ToString();
                zSlidersValue[4] = ZPlayerPrefs.custom3Settings[level][4];
                sliders[4].GetComponent<Slider>().value = zSlidersValue[4];
                
                Text slider5Num = slidersText[4].gameObject.GetComponent<Text>();
                slider5Num.text = sliders[4].GetComponent<Slider>().value.ToString();
                zombieLoaded = true;
            }

            //player custom 3 level settings
            if (playerSettings.activeInHierarchy == true && playerLoaded == false)
            {
                pSlidersValue[0] = ZPlayerPrefs.custom3Settings[level][5];
                sliders[5].GetComponent<Slider>().value = pSlidersValue[0];
              
                Text slider6Num = slidersText[5].gameObject.GetComponent<Text>();
                slider6Num.text = sliders[5].GetComponent<Slider>().value.ToString();
                pSlidersValue[1] = ZPlayerPrefs.custom3Settings[level][6];
                sliders[6].GetComponent<Slider>().value = pSlidersValue[1];
                
                Text slider7Num = slidersText[6].gameObject.GetComponent<Text>();
                slider7Num.text = sliders[6].GetComponent<Slider>().value.ToString();
                pSlidersValue[2] = ZPlayerPrefs.custom3Settings[level][7];
                sliders[7].GetComponent<Slider>().value = pSlidersValue[2];
               
                Text slider8Num = slidersText[7].gameObject.GetComponent<Text>();
                slider8Num.text = sliders[7].GetComponent<Slider>().value.ToString();
                playerLoaded = true;
            }


        }

    }
}

     


