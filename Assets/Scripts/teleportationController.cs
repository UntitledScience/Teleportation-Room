﻿using UnityEngine;
using System.Collections;

public class teleportationController : MonoBehaviour {

    public GameObject playerGO;
    public float dropSpeed = 3f;
    public float transportOffsetX = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Floor")
        {
            playerGO.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
