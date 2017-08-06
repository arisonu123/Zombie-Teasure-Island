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

            if (Other.gameObject.GetComponent<zombieBehavior>().currentZombieHP <= 0)//destroy zombie
            {
                Other.gameObject.GetComponent<zombieBehavior>().zombieAniController.SetBool("death", true);
                Destroy(Other.gameObject.GetComponent<ZombieScreenSpaceUI>().zombieHealthUI, 0.75f);
                Destroy(Other.gameObject, 0.75f);
                Other.gameObject.GetComponent<zombieBehavior>().spawnCoin();
            }
           
        }
    }
}
