using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulationVirtualHand : SelectionVirtualHand
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lastSelection != null && lastSelection.IsSelected() && Input.GetKey(KeyCode.R))
        {
            lastSelection.transform.rotation = this.transform.rotation;
        }
        if (lastSelection != null && lastSelection.IsSelected() && Input.GetKey(KeyCode.T))
        {
            lastSelection.transform.position = this.transform.position;
        }
    }
}
