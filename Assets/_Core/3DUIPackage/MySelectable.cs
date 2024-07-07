using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelectable : MonoBehaviour {

    public Material highlightMaterial;
    private Material defaultMaterial;

    private bool activeState = false;
    // Use this for initialization
    void Start () {
        defaultMaterial = new Material(GetComponent<Renderer>().material);
    }
    
    // Update is called once per frame
    void Update () {
        if( activeState && Input.GetKeyDown(KeyCode.Mouse0))
        {
            // remove using System; entry that is auto generated
            defaultMaterial.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    internal void Select()
    {
        activeState = true;
        GetComponent<Renderer>().material = highlightMaterial;
    }

    internal void DeSelect()
    {
        activeState = false;
        GetComponent<Renderer>().material = defaultMaterial ;
    }

    public bool IsSelected()
    {
        return activeState;
    }
}
