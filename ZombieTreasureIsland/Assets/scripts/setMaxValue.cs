using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class setMaxValue : MonoBehaviour {
    private float maxNumValue=100;
  
    private void Start()
    {

        //Make speed,damage,and hp/health the percent of levels base set in difficulty settings
        if (GameMaster._instance_.presetName == "Easy Settings" || GameMaster._instance_.presetName == "Normal Settings" ||
            GameMaster._instance_.presetName == "Hard Settings")//if default preset setting
        {
            maxNumValue = maxNumValue * GameMaster._instance_.settingNumbers[0];//set corresponding max slider value for zombie health
      
        }
        else if (GameMaster._instance_.presetName == "Custom (1) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 1 settings for level to set max health display
                maxNumValue = maxNumValue * (ZPlayerPrefs.custom1Settings[level - 1][0] * 0.01f);
              
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 2 settings for level to set max health display
                maxNumValue = maxNumValue * (ZPlayerPrefs.custom2Settings[level - 1][0] * 0.01f);
               
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (3) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 3 settings for level to set max health display
                maxNumValue = maxNumValue * (ZPlayerPrefs.custom3Settings[level - 1][0] * 0.01f);
               
            }

        }

        this.gameObject.GetComponent<Slider>().maxValue = maxNumValue;
        this.gameObject.GetComponent<Slider>().value = maxNumValue;
    }
}
