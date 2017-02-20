using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ZombieScreenSpaceUI : MonoBehaviour {

    private zombieBehavior zombieScript;

    private Canvas canvas;
#pragma warning disable 649
    [SerializeField]
    [Header("Zombie health bar prefab settings")]
    private GameObject healthPrefab;
    [SerializeField]
    private float healthPanelOffset = 0.35f;
#pragma warning restore 649
    private GameObject healthPanel;
    private Text enemyHealth;
    private Slider healthSlider;
    private depthUI depthUIScript;
    private Renderer selfRenderer;
    
    /// <summary>
    /// Gets the enemyHealth Text component
    /// </summary>
    /// <value>The enemy health text display</value>
    public Text enemyHealthText
    {
        get { return enemyHealth; }
    }

    /// <summary>
    /// Gets the zombie healthUI/ healthPanel gameObject
    /// </summary>
    /// <value>The zombie healthUI/ healthPanel gameObject/ zombie health display UI</value>
    public GameObject zombieHealthUI
    {
        get { return healthPanel; }
    }

    private void Start()
    {
        selfRenderer = GetComponentInChildren<Renderer>();
        zombieScript = GetComponent<zombieBehavior>();
        canvas = GameObject.FindGameObjectWithTag("enemyCanvas").GetComponent<Canvas>();
        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        enemyHealth = healthPanel.GetComponentInChildren<Text>();
        enemyHealth.text = zombieScript.maximumZombieHP.ToString();
        healthSlider = healthPanel.GetComponentInChildren<Slider>();

        depthUIScript = healthPanel.GetComponent<depthUI>();
        
        canvas.GetComponent<screenSpaceCanvas>().AddToCanvas(healthPanel);
        healthPanel.SetActive(false);
       
    }
   
    private void Update()
    {
        if (healthPanel != null&&this.gameObject!=null)
        {
            healthSlider.value = zombieScript.currentZombieHP / zombieScript.maximumZombieHP;
            enemyHealth.text = zombieScript.currentZombieHP.ToString() + "/" + zombieScript.maximumZombieHP.ToString();
            Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);//convert world pos to screen pos
            healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

            //get distance of UI from camera
            float distance = Vector3.Distance(worldPos, Camera.main.transform.position);

            depthUIScript.depthAmount = -distance;

            float alpha = 6 - distance;
            depthUIScript.SetAlpha(alpha);

            if (selfRenderer.isVisible && alpha > 0)
            {
                healthPanel.SetActive(true);
            }
            else
            {
                healthPanel.SetActive(false);
            }
        }
    }
}
