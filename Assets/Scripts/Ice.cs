﻿using UnityEngine;
using System.Collections;

public class Ice : Character
{
    public static Ice S;
    void Awake()
    {
        S = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sa = GetComponent<SpriteAnimator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fire")
        {
            Level.S.SomeoneDied();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        if (!attacked && Input.GetKeyDown(KeyCode.LeftControl) && Abilities["IceProj"])
        {
            Attack();
        }
    }
}
