using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script that Lerp the color of a image depending of the scale of the transform
public class modifyHealthBarColor : MonoBehaviour
{
  
    public enum selectedAxis
    {
        xAxis,
        yAxis,
        zAxis
    }
    [SerializeField]
    private selectedAxis selectedXYZAxis = selectedAxis.xAxis;

    // Target
    [SerializeField]
    private Image image;

    // Parameters
    [SerializeField]
    private float minValue = 0.0f;
    [SerializeField]
    private float medValue = 0.005f;
    [SerializeField]
    private float maxValue = 0.01f;
    [SerializeField]
    private Color minColor = Color.red;
    [SerializeField]
    private Color medColor = new Color(0, 5, 3);
    [SerializeField]
    private Color maxColor = Color.green;
    [SerializeField]
    private GameObject healthBar;

 
    // The default image is the one in the gameObject
    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        

    }

    private void Update()
    {
     /*   switch (selectedXYZAxis)
        {
            case selectedAxis.xAxis:
                // Lerp color depending on the scale factor
           
                    image.color = Color.LerpUnclamped(minColor,
                                             medColor,
                                             Mathf.LerpUnclamped(minValue,
                                      medValue,
                                      transform.localScale.x));
                    image.color = Color.LerpUnclamped(medColor,
                                           maxColor,
                                           Mathf.LerpUnclamped(medValue,
                                    maxValue,
                                    transform.localScale.x));
                
                break;
            case selectedAxis.yAxis:
                // Lerp color depending on the scale factor
                image.color = Color.Lerp(minColor,
                                         medColor,
                                         Mathf.Lerp(minValue,
                                  medValue, transform.localScale.y));
                image.color = Color.Lerp(medColor,
                                    maxColor,
                                    Mathf.Lerp(medValue,
                             maxValue,
                             transform.localScale.y));
                break;
            case selectedAxis.zAxis:
                // Lerp color depending on the scale factor
                image.color = Color.Lerp(minColor,
                                         medColor,
                                         Mathf.Lerp(minValue,
                                  medValue,
                                  transform.localScale.z));
                image.color = Color.Lerp(medColor,
                                    maxColor,
                                    Mathf.Lerp(medValue,
                             maxValue,
                             transform.localScale.z));
                break;
        }*/
    }

    // Health between [0.0f,0.005f,0.01] == (currentHealth / totalHealth)
    /// <summary>
    /// Sets the health bar's color based on currently health amount
    /// </summary>
    /// <param name="healthNormalized">The current health amount normalized</param>
    /// <param name="color">The color to set it to</param>
    public void setHealthVisual(float healthNormalized,Color color)
    {

        /*healthBar.transform.localScale = new Vector3(healthNormalized,
         healthBar.transform.localScale.y,
        healthBar.transform.localScale.z);*/
        image.color = color;
    }

}
