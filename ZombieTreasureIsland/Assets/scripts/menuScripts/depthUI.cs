using UnityEngine;
using System.Collections;


public class depthUI : MonoBehaviour {
    private float depth;
    private CanvasGroup canvasGroup;

    /// <summary>
    /// Gets/sets the depth
    /// </summary>
    /// <value>The depth of this depthUI/health bar gameObject</value>
    public float depthAmount
    {
        get { return depth; }
        set { depth = value; }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Sets the alpha value for this gameobject
    /// </summary>
    /// <param name="alpha">The alpha amount</param>
    public void SetAlpha(float alpha)
    {//modify alpha values, disable if object is not visable
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        canvasGroup.alpha = alpha;

        if (alpha <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
