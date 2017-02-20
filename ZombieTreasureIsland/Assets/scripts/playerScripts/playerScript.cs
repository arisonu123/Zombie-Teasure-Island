
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class playerScript : MonoBehaviour {
    private float curHealth;
#pragma warning disable 649
    [SerializeField]
    [Header("Player settings")]
    private float maxHealth;
    private float speed=2f;
    [SerializeField]
    private float defaultSpeed = 2f;
    [SerializeField]
    private float damage=2;
    private Transform dir = null;
    [SerializeField]
    private float throwForce=20;//force pellet is shot at
    [SerializeField]
    private float shellTimeOut = 30;//used to let program know time to destroy pellet
    private Vector3 playerPos;
    [SerializeField]
    private Rigidbody shellMove;//used to apply force to shell when shot
    [SerializeField]
    private Transform playerGraphic;
    [SerializeField]
    [Header("Player Weapon Prefabs")]
    private GameObject sword;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject ammo;

    [Header("Projectile weapon settings")]
    [SerializeField]
    private Transform projectileWeaponContainer;
    [SerializeField]
    private float projectileWeaponAimSpeed = 1f;

    [Header("Melee weapon settings")]
    [SerializeField]
    private Transform meleeWeaponContainer;
    [SerializeField]
    private float meleeWeaponAimSpeed = 1f;

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
        playerPos = this.gameObject.transform.position;
        //Make speed,damage,and hp/health the percent of levels base set in difficulty settings    
    }

    // Update is called once per frame
    private void Update()
    {
        if (projectileWeaponContainer)
        {
            projectileWeaponContainer.rotation = Quaternion.Slerp(projectileWeaponContainer.rotation, transform.rotation, projectileWeaponAimSpeed * Time.deltaTime);
        }

        if (meleeWeaponContainer)
        {
            meleeWeaponContainer.rotation = Quaternion.Slerp(meleeWeaponContainer.rotation, transform.rotation, meleeWeaponAimSpeed * Time.deltaTime);
        }

        Movement();
        //TODO:Refactor this to use buttons from the input manager so the player can change it if they wish
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            controller.SetBool("walk", true);
        }
        else
        {
            controller.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.O))//attack with sword
        {
            StartCoroutine("swordAttack");
        }
        if (Input.GetKeyDown(KeyCode.P))//attack with gun
        {
            StartCoroutine("gunAttack");
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
            UnityEngine.Debug.LogError("horMovement is: " + horMovement);
            UnityEngine.Debug.LogError("vertMovement is: " + vertMovement);

        }
        else
        {
            speed = defaultSpeed;

        }
        // transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
        // transform.Translate(transform.forward * vertMovement * Time.deltaTime * speed);

        //Player graphic rotation
        Vector3 moveDirection = new Vector3(horMovement, 0, vertMovement);
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            playerGraphic.transform.rotation = Quaternion.Slerp(playerGraphic.transform.rotation, newRotation, Time.deltaTime * 8);

        }
    }

    private IEnumerator swordAttack()//sword attack coroutine
    {   
        //sword.SetActive(true);
        sword.GetComponent<BoxCollider>().enabled = true;
        controller.SetTrigger("attack1");
        yield return new WaitForSeconds(.30f);
        sword.GetComponent<BoxCollider>().enabled = false;
        
        //sword.SetActive(false);
    }

    private IEnumerator gunAttack()//gun attack coroutine
    {
        gun.SetActive(true);
        Vector3 bulletPos = gun.transform.position -(gun.transform.forward*0.3f);
        GameObject bullet=Instantiate(ammo, bulletPos, gun.transform.rotation)as GameObject;
        bullet.SetActive(true);
        yield return new WaitForSeconds(5f);
        gun.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider Other)//if player collides with zombie, players takes damage
    {
        if (Other.gameObject.CompareTag("enemy"))
        {
            if (Other.gameObject.GetComponent<Collider>().isTrigger == true)
            {
                curHealth -= Other.gameObject.GetComponent<zombieBehavior>().zombieDamageAmt;
                GameObject.Find("playerHealth").GetComponent<Text>().text = curHealth + "/" + maxHealth;
                if (curHealth <= 0)//play player death animation and load game over
                {
                    controller.SetBool("dead", true);
                    Destroy(GameObject.Find("ScurvyPirate"), 1f);
                    SceneManager.LoadScene("gameOver");
                }
            }
           
        }

    }

    private void OnAnimatorIK()
    {
      

        if (gun.transform.Find("leftHandIKTarget")!=null)//left hand ik
        {

            controller.SetIKPosition(AvatarIKGoal.LeftHand, gun.transform.Find("leftHandIKTarget").transform.position);
            controller.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            controller.SetIKRotation(AvatarIKGoal.LeftHand, gun.transform.Find("leftHandIKTarget").transform.rotation);
            controller.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            controller.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            controller.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }

        if (gun.transform.Find("leftElbowIKHint")!=null)//left elbow IK
        {

            controller.SetIKHintPosition(AvatarIKHint.LeftElbow, gun.transform.Find("leftElbowIKHint").transform.position);
            controller.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);

        }
        else
        {
            controller.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);
        }

        if (sword.transform.Find("rightHandIKTarget")!=null)//right hand ik
        {
            controller.SetIKPosition(AvatarIKGoal.RightHand, sword.transform.Find("rightHandIKTarget").transform.position);
            controller.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            controller.SetIKRotation(AvatarIKGoal.RightHand, sword.transform.Find("rightHandIKTarget").transform.rotation);
            controller.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            controller.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);

        }

        if (sword.transform.Find("rightElbowIKHint")!=null)//right elbow IK
        {

            controller.SetIKHintPosition(AvatarIKHint.LeftElbow, sword.transform.Find("rightElbowIKHint").transform.position);
            controller.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);

        }
        else
        {
            controller.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
        }

        if (transform.Find("headLookAtPosition")!=null)//head ik
        {
            controller.SetLookAtPosition(transform.Find("headLookAtPosition").transform.position);
            controller.SetLookAtWeight(1f);
        }

        else
        {
            controller.SetLookAtWeight(0);
        }


    }
}
