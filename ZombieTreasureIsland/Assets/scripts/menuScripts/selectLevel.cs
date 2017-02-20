using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class selectLevel : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
    private string levelToLoad;
#pragma warning restore 649

    // Update is called once per frame
    private void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))//raycast to mouse position from camera
            {
                Debug.Log(hit);
                // whatever tag you are looking for on your game object
                if (hit.collider.tag=="button")//if this button was hit
                {
                    GameObject difSelect = GameObject.Find("currentSelection");
                    Text selectText = difSelect.GetComponent<Text>();
                    selectText.text = "Currently Selected: " + levelToLoad;
                    GameMaster._instance_.levelSelected = levelToLoad;
                   

                }
            }
        }
    }
   
}
