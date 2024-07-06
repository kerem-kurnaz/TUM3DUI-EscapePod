using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionRaycast : MonoBehaviour
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
    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        // position offset avoids self collision with non centered colliders
        bool intersects = Physics.Raycast(transform.position + transform.up * 0.1f, transform.up, out hitInfo);
        if (intersects)
        {
            var newSelection = hitInfo.transform.gameObject.GetComponent<MySelectable>();
            // Only activate new selectable if previous one was deselected
            if(lastSelection != null && newSelection != lastSelection)
            {
                lastSelection.DeSelect();
                lastSelection = null;
            }
            if (newSelection != null)
            {
                newSelection.Select();
                lastSelection = newSelection;
            }
        }
        else
        {
            if (lastSelection != null)
            {
                lastSelection.DeSelect();
                lastSelection = null;
            }   

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.up * 0.1f, transform.up);
    }
}
