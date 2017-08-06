using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class swordScript : MonoBehaviour {
    private playerScript player;
	// Use this for initialization
	private void Start () {
        player = GameObject.Find("player").GetComponent<playerScript>();
    }
	

  
    

    private void OnTriggerEnter(Collider Other)//if sword hits zombie, zombie takes damage
    {
       
        if (Other.gameObject.CompareTag("enemy"))
        {
            Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP -= player.playerDamageAmt;
            if (Other.gameObject.GetComponent<ZombieScreenSpaceUI>().enemyHealthText != null)
            {
                Other.gameObject.GetComponent<ZombieScreenSpaceUI>().enemyHealthText.text = Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP +
                    "/" + Other.gameObject.GetComponent<zombieBehavior>().maximumZombieHP;
            }
            if (Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP <= 0)//destroy zombie
            {
                Other.gameObject.GetComponent<zombieBehavior>().zombieAniController.SetBool("death", true);
                Destroy(Other.gameObject.GetComponent<ZombieScreenSpaceUI>().zombieHealthUI, 0.7f);
                Destroy(Other.gameObject, 0.75f);
                Other.gameObject.GetComponent<zombieBehavior>().spawnCoin();
            }
            /*if (Other.gameObject.GetComponent <zombieBehavior>().curHp <= (Other.gameObject.GetComponent<zombieBehavior>().maxHp / 2)
                &&Other.gameObject.GetComponent<zombieBehavior>().curHp> Other.gameObject.GetComponent<zombieBehavior>().maxHp/3)
            {//if health is at half or less but still greater than 1/3 make color of bar orange
                //modify health bar color
                float healthBarPercentage=0.005f;
                Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().setHealthVisual(healthBarPercentage,
                     Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().medColor);

            }
            if(Other.gameObject.GetComponent<zombieBehavior>().curHp<= Other.gameObject.GetComponent<zombieBehavior>().maxHp / 3)
            {//if health is at less than or equal to 1/3 of max make color of bar red
             //modify health bar color
                float healthBarPercentage = 0.003f;
                Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().setHealthVisual(healthBarPercentage,
                     Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().minColor);
            }
            if (Other.gameObject.GetComponent<zombieBehavior>().curHp <= 0)//destroy zombie
            {
                Destroy(Other.gameObject, 0.1f);
            }*/
        }

    }
}
