using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class screenSpaceCanvas : MonoBehaviour {

    private List<depthUI> panels = new List<depthUI>();

    private void Awake()
    {
        panels.Clear();
    }

    private void Update()
    {
        Sort();
    }

    /// <summary>
    /// Adds the gameobjects depthUI component to the list of panels
    /// </summary>
    /// <param name="objectToAdd">The GameObject whoms depthUI script you want to add to the list of depthUI scripts</param>
    public void AddToCanvas(GameObject objectToAdd)
    {
        panels.Add(objectToAdd.GetComponent<depthUI>());
    }

    private void Sort()
    {//sort panels based on depth
        panels.Sort((x, y) => x.depthAmount.CompareTo(y.depthAmount));
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i] != null)
            {
                panels[i].transform.SetSiblingIndex(i);
            }
        }
        
    }
    
}
