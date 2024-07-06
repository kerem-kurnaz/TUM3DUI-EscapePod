using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionVirtualHand : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    internal MySelectable lastSelection = null;
    private void OnTriggerEnter(Collider other)
    {
        var newSelection = other.gameObject.GetComponent<MySelectable>();
        if (newSelection != null && (newSelection != lastSelection))
        {
            // Deactive previous object if we hit a new selectable object
            if (lastSelection != null && !newSelection.IsSelected())
                lastSelection.DeSelect();
            newSelection.Select();
            lastSelection = newSelection;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (lastSelection != null)
        {
            lastSelection.DeSelect();
            lastSelection = null;
        }
    }

}
