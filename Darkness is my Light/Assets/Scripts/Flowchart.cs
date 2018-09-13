﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowchart : MonoBehaviour {

    public static Flowchart Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool awaken = false;
    public bool bedroomGetUp = false;
    public bool bedroomLightsOn = false;
    public bool pickupGlasses = false;
    public bool bathroomWashUp = false;
    public bool bottleTaken = false;
    public bool exitBedroom = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
