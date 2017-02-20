using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class doubloonPickup : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
    private int coinWorth;
#pragma warning restore 649


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //set total player doubloons and doubloons text display
            GameMaster._instance_.playerDoubloons += coinWorth;
            GameObject.Find("player").GetComponent<playerScript>().doubloonsUI.GetComponent<Text>().text = "Doubloons: " + GameMaster._instance_.playerDoubloons;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
