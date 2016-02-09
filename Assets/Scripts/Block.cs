﻿using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {


    public bool burnt, frozen;
    public Material fire, ice;
    Renderer rend;
    Rigidbody2D rb;
    public float burnTime = 5f, burnStart;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if(burnt && Time.time - burnStart > burnTime) {
            Destroy(this.gameObject);
        }
	}

    Vector2 zero = new Vector2(0, 0);
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Fire")
        {
            rend.sharedMaterial = fire;
            rb.velocity = zero;
            rb.mass = 100000;
            burnt = true;
            burnStart = Time.time;
            frozen = false;
        }
        else if (col.gameObject.name == "Ice")
        {
            rend.sharedMaterial = ice;
            rb.mass = 0;
            frozen = true;
            burnt = false;
        }
    }
}
