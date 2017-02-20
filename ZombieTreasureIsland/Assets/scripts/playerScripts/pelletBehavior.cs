using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pelletBehavior : MonoBehaviour {

    private float shotForce;
    private float existTime;
    private float bulletTimeOut;
    private GameObject player;
    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("player");
        shotForce = player.gameObject.GetComponent<playerScript>().ThrowForce;
        bulletTimeOut = player.gameObject.GetComponent<playerScript>().bulletTimeOut;
        gameObject.GetComponent<Rigidbody>().velocity = -(transform.forward*0.3f) * shotForce;
        existTime = 0;

    }

    // Update is called once per frame
    private void Update()
    {

        existTime = existTime + Time.deltaTime;
        if (existTime >= bulletTimeOut)
        {//ensures shells are destroyed after timeout period
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider Other)//if bullet hits zombie, zombie takes damage
    {

        if (Other.gameObject.CompareTag("enemy"))
        {
            Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP -= player.GetComponent<playerScript>().playerDamageAmt;
            Other.gameObject.GetComponent<ZombieScreenSpaceUI>().enemyHealthText.text = Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP +
           "/" + Other.gameObject.GetComponent<zombieBehavior>().maximumZombieHP;
            /* if (Other.gameObject.GetComponent<zombieBehavior>().curHp <= (Other.gameObject.GetComponent<zombieBehavior>().maxHp / 2)
               && Other.gameObject.GetComponent<zombieBehavior>().curHp > Other.gameObject.GetComponent<zombieBehavior>().maxHp / 3)
             {//if health is at half or less but still greater than 1/3 make color of bar orange
                 //modify health bar color
                 float healthBarPercentage = 0.005f;
                 Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().setHealthVisual(healthBarPercentage,
                     Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().medColor);

             }
             if (Other.gameObject.GetComponent<zombieBehavior>().curHp <= Other.gameObject.GetComponent<zombieBehavior>().maxHp / 3)
             {//if health is at less than or equal to 1/3 of max make color of bar red
              //modify health bar color
                 float healthBarPercentage = 0.003f;
                 Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().setHealthVisual(healthBarPercentage,
                     Other.gameObject.GetComponentInChildren<modifyHealthBarColor>().minColor);
             }
             if (Other.gameObject.GetComponent<zombieBehavior>().curHp <= 0)//destroy zombie
             {
                 Destroy(Other.gameObject,0.1f);
             }
         }*/

        }
    }
}
