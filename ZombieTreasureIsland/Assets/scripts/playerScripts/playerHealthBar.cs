using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour {
    private GameObject player;
    private playerScript playerScript;

    [SerializeField]
    [Header("Player Health Bar UI")]
    private Slider healthSlider;
    [SerializeField]
    private Text playerHealth;
	// Use this for initialization
	private void Start () {
        player = GameObject.Find("player");
        playerScript = player.GetComponent<playerScript>();
        healthSlider = this.gameObject.GetComponentInChildren<Slider>();
        playerHealth = GameObject.Find("playerHealth").GetComponent<Text>();
	}
	
	// Update is called once per frame
	private void Update () {
        healthSlider.value = playerScript.currentPlayerHealth / playerScript.maximumPlayerHealth;
        playerHealth.text = playerScript.currentPlayerHealth.ToString() + "/" + playerScript.maximumPlayerHealth.ToString();
    }
}
