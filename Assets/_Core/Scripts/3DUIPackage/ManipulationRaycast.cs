using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulationRaycast : SelectionRaycast
{

    public GameObject wand;

    private Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {
        lastPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Hide wand if manipulation is in progress
        wand.SetActive(lastSelection == null || !lastSelection.IsSelected() || !(Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.T)));

        // perform manipulation
        if (lastSelection != null && lastSelection.IsSelected())
        {
            if (Input.GetKey(KeyCode.R))
            {
                lastSelection.gameObject.transform.rotation = this.transform.rotation;
            }

            if (Input.GetKey(KeyCode.T))
            {
                Vector3 deltaMovement = transform.position - lastPosition;
                lastSelection.gameObject.transform.position += deltaMovement;
            }
        }
        lastPosition = this.transform.position;
    }
}
