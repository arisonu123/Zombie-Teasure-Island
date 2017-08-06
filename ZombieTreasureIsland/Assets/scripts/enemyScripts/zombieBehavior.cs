using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class zombieBehavior : MonoBehaviour {
    private Transform player;
#pragma warning disable 649
    [SerializeField]
    private float speed=1;
    [SerializeField]
    private float damage=20;
    [SerializeField]
    private float maxHp=100;
    private float curHp;
    private float timeSpawn;

    private Animator controller;
    [SerializeField]
    [Header("Colliders for attack reconization")]
    private BoxCollider left;
    [SerializeField]
    private BoxCollider right;

    [SerializeField]
    [Header("Coin spawn chances")]
    private int outOf100ChanceForCoinSpawn = 60;
    [SerializeField]
    private int outOf100ChanceForCopper = 70;
    [SerializeField]
    private int outOf100ChanceForSilver = 25;
    [SerializeField]
    private int outOf100ChanceForGold = 5;
    [SerializeField]
    [Header("Coin prefabs")]
    private GameObject[] coins;
#pragma warning restore 649
    private GameObject drop;
    private Vector3 playerPos;
    private float targetDis;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;

    
    /// <summary>
    /// Gets the maximum zombie health
    /// </summary>
    /// <value>The maximum zombie health amount</value>
    public float maximumZombieHP
    {
        get { return maxHp; }
    }

    /// <summary>
    /// Gets/sets the current zombie health
    /// </summary>
    /// <value>The current zombie health amount</value>
    public float currentZombieHP
    {
        get { return curHp; }
        set { curHp = value; }
    }

    /// <summary>
    /// Gets the damage that the zombie does
    /// </summary>
    /// <value>The amount of damage that the zombie does to the player</value>
    public float zombieDamageAmt
    {
        get { return damage; }
    }

    /// <summary>
    /// Gets the zombie's animation controller
    /// </summary>
    /// <value>The zombie's animation controller</value>
    public Animator zombieAniController
    {
        get { return controller; }
    }

    // Use this for initialization
    private void Start () {
        timeSpawn = Time.time;
        player = GameObject.Find("ScurvyPirate").transform;
        playerPos = player.position;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.enabled = false;
        animator = GetComponent<Animator>();
        controller = this.gameObject.GetComponent<Animator>();
        left.enabled = false;
        right.enabled = false;
        //Make speed,damage,and hp/health the percent of levels base set in difficulty settings
        if (GameMaster._instance_.presetName == "Easy Settings" || GameMaster._instance_.presetName == "Normal Settings" ||
            GameMaster._instance_.presetName == "Hard Settings")//if default preset setting
        {
            maxHp =maxHp * GameMaster._instance_.settingNumbers[0];//set corresponding zombie settings
            curHp = maxHp;
            speed = speed * GameMaster._instance_.settingNumbers[1];
            damage = damage * GameMaster._instance_.settingNumbers[3];
        }
        else if (GameMaster._instance_.presetName == "Custom (1) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 1 settings for level to set health,speed and damage
                maxHp = maxHp * (ZPlayerPrefs.custom1Settings[level - 1][0] * 0.01f);
                curHp = maxHp;
                speed = speed * (ZPlayerPrefs.custom1Settings[level - 1][1] * 0.01f);
                damage = damage * (ZPlayerPrefs.custom1Settings[level - 1][3] * 0.01f);
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (2) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 2 settings for level to set health,speed and damage
                maxHp= maxHp* (ZPlayerPrefs.custom2Settings[level - 1][0] * 0.01f);
                curHp = maxHp;
                speed = speed* (ZPlayerPrefs.custom2Settings[level - 1][1] * 0.01f);
                damage = damage * (ZPlayerPrefs.custom2Settings[level - 1][3] * 0.01f);
            }

        }
        else if (GameMaster._instance_.presetName == "Custom (3) Settings")
        {
            int level;
            int.TryParse(GameMaster._instance_.levelSelected, out level);
            if (level != 0)
            {
                //use custom 3 settings for level to set health,speed and damage
                maxHp = maxHp * (ZPlayerPrefs.custom3Settings[level - 1][0] * 0.01f);
                curHp = maxHp;
                speed = speed * (ZPlayerPrefs.custom3Settings[level - 1][1] * 0.01f);
                damage = damage * (ZPlayerPrefs.custom3Settings[level - 1][3] * 0.01f);
            }

        }

        if (outOf100ChanceForCoinSpawn > 100)
        {//if chance number is >100
            outOf100ChanceForCoinSpawn = (int)outOf100ChanceForCoinSpawn / 100;//divide chance by 100
        }
        if (outOf100ChanceForCopper > 100)
        {
            outOf100ChanceForCopper = (int)outOf100ChanceForCopper / 100;
        }
        if (outOf100ChanceForSilver > 100)
        {
            outOf100ChanceForSilver = (int)outOf100ChanceForSilver / 100;
        }
        if (outOf100ChanceForGold > 100)
        {
            outOf100ChanceForGold = (int)outOf100ChanceForGold / 100;
        }
        if (outOf100ChanceForCopper + outOf100ChanceForSilver + outOf100ChanceForGold != 100)
        {
            outOf100ChanceForCopper = 70;
            outOf100ChanceForSilver = 25;
            outOf100ChanceForGold = 5;
        }
        setPercents(outOf100ChanceForCopper, outOf100ChanceForSilver);//used to set percent numbers for spawning coins
        navMeshAgent.speed = speed;
    }
	
	// Update is called once per frame
	private void Update () {
        if (player != null)
        {
            playerPos = player.position;
        }
        if (navMeshAgent.enabled)
        {
            if (player != null)
            {
                navMeshAgent.SetDestination(playerPos);
            }

        }

        if (Time.time>= timeSpawn+2f)//wait 2 seconds before moving after spawning
        {
            navMeshAgent.enabled = true;
            shouldAttack();
        }

    }

    private void setPercents(int copperPercent, int silverPercent)
    {//used to convert percents to proper numbers for generator
        outOf100ChanceForSilver = copperPercent + silverPercent + 1;


    }

    public void activateAtkCols()
    {
        right.enabled = !right.enabled;
        left.enabled = !left.enabled;
    }

    private void shouldAttack()//determine if AI should attack
    {

        targetDis = Vector3.Distance(this.transform.position, playerPos);
        if (targetDis > .5)//if problems return to 0.005
        {     
            controller.SetBool("walk", true);
            controller.SetBool("attack", false);
        }
        else
        {
            controller.SetBool("walk", false);
            controller.SetBool("attack", true);
           
        }
       
    }

    /// <summary>
    /// Chooses a random coin or ,perhaps nothing at all, to spawn at the location of the zombie
    /// </summary>
    public void spawnCoin()
    {
        UnityEngine.Random randNum = new UnityEngine.Random();
        int coinChoice = UnityEngine.Random.Range(0, 102);
        if (coinChoice <= outOf100ChanceForCoinSpawn)
        {//spawn a coin
            coinChoice = UnityEngine.Random.Range(0, 101);
            if (coinChoice >= 0 && coinChoice <= outOf100ChanceForCopper)
            {//spawn bronze coin
                drop=Instantiate(coins[0], new Vector3(this.transform.position.x,this.transform.position.y+0.05f,this.transform.position.z),this.transform.rotation) as GameObject;
            }
            else if (coinChoice >= (outOf100ChanceForCopper + 1) && coinChoice <= outOf100ChanceForSilver)
            {//spawn silver coin
                drop=Instantiate(coins[1], new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z), this.transform.rotation) as GameObject;
            }
            else if (coinChoice >= (outOf100ChanceForSilver + 1) && coinChoice <= 101)
            {//spawn gold coin
                drop=Instantiate(coins[2], new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z), this.transform.rotation) as GameObject;
            }
            else
            {
                //do nothing
            }
           
        }


    }


    private void OnTriggerEnter(Collider col)
    {
        if(player!=null && player.gameObject.GetComponentInChildren<Collider>() ==col)
        {
            player.parent.gameObject.GetComponent<playerScript>().currentPlayerHealth -= damage;
            if (player.parent.gameObject.GetComponent<playerScript>().currentPlayerHealth <= 0)
            {
                player.parent.gameObject.GetComponent<playerScript>().playerDeath();
            }
        }

    }

}
