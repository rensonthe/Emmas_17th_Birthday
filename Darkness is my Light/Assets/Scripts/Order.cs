using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Order : MonoBehaviour {

    Flowchart flowchart;
    public GameObject panel;
    public Canvas sayDialog;
    GameObject panelClone;

    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {
		
	}

    void MessageSent()
    {
        panelClone = Instantiate(panel, transform.position, Quaternion.identity);
        panelClone.transform.SetParent(sayDialog.transform);
        panelClone.transform.localScale = new Vector3(1, 1, 1);
        panelClone.transform.position += new Vector3(0, 170, 0);
    }

    void MessageMove()
    {
        GameObject[] myInstantiatedObjects = GameObject.FindGameObjectsWithTag("Panel");
        if (myInstantiatedObjects.Length > 1)
        {
            panelClone.transform.position += new Vector3(0, 170, 0);
        }
    }

    void MessageDelete()
    {
        Destroy(panelClone);
    }
}
