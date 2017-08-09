
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class playerScript : MonoBehaviour {
    private float curHealth;
#pragma warning disable 649
    [SerializeField]
    [Header("Player settings")]
    private float maxHealth=500;
    [SerializeField]
    public float speed=2f;
    [SerializeField]
    private float defaultSpeed = 2f;
    [SerializeField]
    private float damage=25;
    [SerializeField]
    private float throwForce=20;//force pellet is shot at
    [SerializeField]
    private float shellTimeOut = 30;//used to let program know time to destroy pellet
    [SerializeField]
    private Transform playerGraphic;
    [SerializeField]
    [Header("Player Weapon Prefabs")]
    private GameObject sword;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject ammo;

    [SerializeField]
    [Header("Player Weapon Audio")]
    private AudioClip swordSwoosh;
    [SerializeField]
    private AudioClip gunShot;

    private Animator controller;

    [SerializeField]
    [Header("Player Doubloons Amount UI")]
    private GameObject doubloonsText;
#pragma warning restore 649
    /// <summary>
    /// Gets/sets the player's currentHealth
    /// </summary>
    /// <value>The current health of the player</value>
    public float currentPlayerHealth
    {
        get { return curHealth; }
        set { curHealth = value; }
    }

    /// <summary>
    /// Gets/sets the player's maxHealth
    /// </summary>
    /// <value>The max health of the player</value>
    public float maximumPlayerHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    /// <summary>
    /// Gets the doubloonsText gameObject
    /// </summary>
    /// <value>The doubloonsText gameobject which has a Text component that displays the current total of doubloons that the player has</value>
    public GameObject doubloonsUI
    {
        get { return doubloonsText; }
    }

    /// <summary>
    /// Gets the damage that the player's weapon does
    /// </summary>
    /// <value>The amount of damage that the player's weapon does to zombie enemies</value>
    public float playerDamageAmt
    {
        get { return damage; }
    }

    /// <summary>
    /// Gets the throwForce that the player's gun shoots at
    /// </summary>
    /// <value>The force that the player's gun shoots bullets out at</value>
    public float ThrowForce
    {
        get { return throwForce; }
    }

    /// <summary>
    /// Gets the shellTimeOut that the player's bullets/shells destroy after
    /// </summary>
    /// <value>The amount of time after shot that it takes before a player's bullet/shell destroys if it does not impact an enemy beforehand</value>
    public float bulletTimeOut
    {
        get { return shellTimeOut; }
    }
	// Use this for initialization
	private void Start () {
        transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        controller = this.gameObject.GetComponent<Animator>();
        //Make speed,damage,and hp/health the percent of levels base set in difficulty settings
        Debug.Log("player settings is " + GameMaster._instance_.presetName);
        if (GameMaster._instance_.presetName=="Easy Settings"|| GameMaster._instance_.presetName == "Normal Settings" ||
            GameMaster._instance_.presetName == "Hard Settings")//if default preset setting
        {
            maxHealth = maxHealth*GameMaster._instance_.settingNumbers[5];//set corresponding player settings
            curHealth = maxHealth;
            defaultSpeed = defaultSpeed * GameMaster._instance_.settingNumbers[6];
            speed = defaultSpeed;
            damage = damage * GameMaster._instance_.settingNumbers[7];
        }
        else if(GameMaster._instance_.presetName == "Custom (1) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 1 settings for level to set health,speed and damage
                maxHealth= maxHealth * (ZPlayerPrefs.custom1Settings[level - 1][5]*0.01f);
                curHealth = maxHealth;
                defaultSpeed = defaultSpeed * (ZPlayerPrefs.custom1Settings[level - 1][6] * 0.01f);
                speed = defaultSpeed;
                damage = damage * (ZPlayerPrefs.custom1Settings[level - 1][7] * 0.01f);
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 2 settings for level to set health,speed and damage
                maxHealth = maxHealth * (ZPlayerPrefs.custom2Settings[level - 1][5] * 0.01f);
                curHealth = maxHealth;
                defaultSpeed = defaultSpeed * (ZPlayerPrefs.custom2Settings[level - 1][6] * 0.01f);
                speed = defaultSpeed;
                damage = damage * (ZPlayerPrefs.custom2Settings[level - 1][7] * 0.01f);
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (3) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 3 settings for level to set health,speed and damage
                maxHealth = maxHealth* (ZPlayerPrefs.custom3Settings[level - 1][5] * 0.01f);
                curHealth = maxHealth;
                defaultSpeed = defaultSpeed * (ZPlayerPrefs.custom3Settings[level - 1][6] * 0.01f);
                speed = defaultSpeed;
                damage = damage * (ZPlayerPrefs.custom3Settings[level - 1][7] * 0.01f);
            }

        }
        doubloonsText.GetComponent<Text>().text = "Doubloons: "+GameMaster._instance_.playerDoubloons;
        //Make speed,damage,and hp/health the percent of levels base set in difficulty settings    
    }

    // Update is called once per frame
    private void Update()
    {
        if (!controller.GetBool("dead"))
        {
            Movement();
           
            if (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0)
            {
               controller.SetBool("walk", true);
            }
            else
            {
                controller.SetBool("walk", false);
            }

            if (Input.GetButtonDown("Fire1"))//attack with sword
            {
                StartCoroutine("swordAttack");
            }
            if (Input.GetButtonDown("Fire2"))//attack with gun
            {
                gunAttack();
            }
        }
    }

    private void Movement()
    {

        //Player object movement
        float horMovement = Input.GetAxisRaw("Horizontal");
        float vertMovement = Input.GetAxisRaw("Vertical");


        if (horMovement != 0 && vertMovement != 0)
        {
            speed = defaultSpeed - .5f;
                       

        }
        else
        {
            speed = defaultSpeed;

        }
        Vector3 moveDirection = new Vector3(horMovement, 0,vertMovement);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        
        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
        
        //Player graphic rotation
        Vector3 rotateDirection = new Vector3(horMovement, 0, vertMovement);
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(rotateDirection);
            playerGraphic.transform.rotation = Quaternion.Slerp(playerGraphic.transform.rotation, newRotation, Time.deltaTime * 8);

        }
    }

    private IEnumerator swordAttack()//sword attack coroutine
    {   
        sword.GetComponent<BoxCollider>().enabled = true;
        controller.SetTrigger("attack1");
        yield return new WaitForSeconds(.30f);
        sword.GetComponent<BoxCollider>().enabled = false;
             
    }

    /// <summary>
    /// Plays the sword attack sound
    /// </summary>
    public void playSwordAtkSnd()
    {
        AudioSource.PlayClipAtPoint(swordSwoosh, sword.transform.position);
    }

    private void gunAttack()//gun attack coroutine
    {
        Vector3 bulletPos = gun.transform.position -(gun.transform.forward*0.3f);
        GameObject bullet=Instantiate(ammo, bulletPos, gun.transform.rotation)as GameObject;
        AudioSource.PlayClipAtPoint(gunShot, gun.transform.position);
        bullet.SetActive(true);  
    }

    /// <summary>
    /// Play player death animation and load game over
    /// </summary>
    public void playerDeath()
    {
        controller.SetBool("dead", true);
        Destroy(GameObject.Find("ScurvyPirate"), 5f);
        StartCoroutine(LoadGameover());
    }

    private IEnumerator LoadGameover()
    {
        yield return new WaitForSeconds(5f);
        foreach(GameObject zombie in GameObject.FindGameObjectsWithTag("enemy"))
        {//destroy all zombies on gameover
            Destroy(zombie);
        }
        SceneManager.LoadScene("gameOver");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("in controller hit");
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

}
