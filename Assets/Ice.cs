﻿using UnityEngine;
using System.Collections;

public class Ice : Character {

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fire")
        {
            Level.s.SomeoneDied();
        }
    }

    // Update is called once per frame
    void Update () {
        Move(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
	}
}
