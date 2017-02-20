using UnityEngine;
using System.Collections;

public class cameraFacingBillboard : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    private RectTransform rt;

    private void Awake()
    {
        rt = (RectTransform)this.transform;
    }

    private void Update()
    {
        /* transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
             m_Camera.transform.rotation * Vector3.up);*/
        /* Vector3 zomPos = Camera.main.WorldToScreenPoint(this.gameObject.GetComponentInParent<Transform>().position);
         zomPos.y = zomPos.y + 1.5f;

         this.gameObject.transform.position = zomPos;*/
        Vector2 pos = gameObject.transform.position;  // get the game object position
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        rt.anchorMin = viewportPoint;
        rt.anchorMax = viewportPoint;
    }
}