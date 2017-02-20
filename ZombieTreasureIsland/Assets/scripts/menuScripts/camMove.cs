using UnityEngine;
using System.Collections;

public class camMove : MonoBehaviour {
#pragma warning disable 649
    private bool canMovLeft;
    private bool canMovRight;
    private bool canMovUp;
    private bool canMovDown;
    private float speed = 100f;
    [SerializeField]
    private float defaultSpeed = 100f;
    public enum dir
    {
        left,
        right,
        up,
        down
    }
    private dir movDir;
#pragma warning restore 649
    // Use this for initialization
    private void Start()
    {
        canMovLeft = true;
        canMovRight = true;
        canMovUp = true;
        canMovDown = true;


    }

    // Update is called once per frame
   private void Update () {
        Movement();
        /*if (Input.GetKeyDown(KeyCode.A) && canMovLeft == true)
       {
           Transform cam = this.gameObject.transform;
           Vector3 currentPos = cam.position;
           Vector3 updatePos = cam.position;
           updatePos.x = currentPos.x - 0.001f;
           updatePos.y = currentPos.y;
           cam.Translate(updatePos);
           movDir = dir.left;
           canMovLeft = true;
           canMovRight = true;
           canMovUp = true;
           canMovDown = true;
       }
       if (Input.GetKeyDown(KeyCode.D)&&canMovRight==true)
       {
           Transform cam = this.gameObject.transform;
           Vector3 currentPos = cam.position;
           Vector3 updatePos = cam.position;
           updatePos.x = currentPos.x +0.001f;
           updatePos.y = currentPos.y;
           cam.Translate(updatePos);
           movDir = dir.right;
           canMovLeft = true;
           canMovRight = true;
           canMovUp = true;
           canMovDown = true;
       }

       if (Input.GetKeyDown(KeyCode.W) && canMovUp == true)
       {
           Transform cam = this.gameObject.transform;
           Vector3 currentPos = cam.position;
           Vector3 updatePos = cam.position;
           updatePos.x = currentPos.x;
           updatePos.y = currentPos.y+0.001f;
           cam.Translate(updatePos);
           movDir = dir.up;
           canMovLeft = true;
           canMovRight = true;
           canMovUp = true;
           canMovDown = true;
       }

       if (Input.GetKeyDown(KeyCode.D)&&canMovDown==true)
       {
           Transform cam = this.gameObject.transform;
           Vector3 currentPos = cam.position;
           Vector3 updatePos = cam.position;
           updatePos.x = currentPos.x;
           updatePos.y = currentPos.y - 0.001f;
           cam.Translate(updatePos);
           movDir = dir.down;
           canMovLeft = true;
           canMovRight = true;
           canMovUp = true;
           canMovDown = true;
       }*/
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
        transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
        transform.Translate(transform.forward * vertMovement * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (movDir == dir.down)
        {
            canMovDown = false;
        }
        if (movDir == dir.up)
        {
            canMovUp = false;
        }
        if (movDir == dir.left)
        {
            canMovLeft = false;
        }
        if (movDir == dir.right)
        {
            canMovRight = false;
        }




    }
}
