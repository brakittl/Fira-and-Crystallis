﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public GameObject[] activates;
    public GameObject FireProj, IceProj;
    public Sprite spriteN, spriteF, spriteI;
    //public bool TargetIsFire = false;
    public float fireTime = 3f;
    public float speed = 3f;
    GameObject fire, ice;
    public int MaxHealth = 20;
    public int CurrentHealth = 20;
    Vector3 dest = Vector3.zero;
    public SpriteRenderer sr;
    // Use this for initialization
    void Start()
    { 
        fire = Fire.S.gameObject;
        ice = Ice.S.gameObject;
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        StartCoroutine(chooseDir());
        StartCoroutine(shoot());
        //StartCoroutine(Shoot());
    }
    bool inv = false;
    IEnumerator getHit()
    {
        --CurrentHealth;
        inv = true;
        for (int c = 0; c < 2; ++c)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        inv = false;
        PhaseChangeCheck();
        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    void PhaseChangeCheck()
    {
        if(CurrentHealth < 15)
        {
            isNeutral = false;
            isFire = true;
        }
        if(CurrentHealth < 10)
        {
            isNeutral = false;
            isFire = false;
        } 
        if(CurrentHealth < 5)
        {
            isNeutral = true;
            isFire = true;
        }
        if (isNeutral)
        {
            sr.sprite = spriteN;
        } else
        {
            if (isFire)
            {
                sr.sprite = spriteF;
            } else
            {
                sr.sprite = spriteI;
            }
        }
    }
    void Die()
    {
        for (int c = 0; c < activates.Length; ++c)
        {
            activates[c].SetActive(true);
        }
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "IceProj" || collision.tag == "FireProj")
        {
            if (!inv)
            {
                if (isNeutral)
                {
                    StartCoroutine(getHit());
                } else
                {
                    if (isFire)
                    {
                        if(collision.tag == "FireProj")
                        {

                            StartCoroutine(getHit());
                        }
                    }
                    if (!isFire)
                    {
                        if(collision.tag == "IceProj")
                        {

                            StartCoroutine(getHit());
                        }
                    }
                }
            }
        }
        if (collision.tag == "Player")
        {
            Level.S.KillCharacter(collision.gameObject);
        }
    }

    bool isNeutral = true;
    bool isFire = false;

    IEnumerator chooseDir()
    {
        while (true)
        {
            float x = Random.Range(-4f, 4f);
            float y = Random.Range(-4f, 4f);
            dest = new Vector3(x, y, 0);
            yield return new WaitForSeconds(5f);
        }
    }


    public void ShootAtFire()
    {

        Vector3 dir = Vector3.up;
        if (fire != null)
        {
            dir = fire.transform.position - transform.position;
        }
        dir = dir.normalized;
        GameObject g = Instantiate(IceProj, transform.position, transform.rotation) as GameObject;
        g.GetComponent<Rigidbody2D>().velocity = dir;
    }

    public void ShootAtIce()
    {
        Vector3 dir = Vector3.up;
        if (ice != null)
        {
            dir = ice.transform.position - transform.position;
        }
        dir = dir.normalized;
        GameObject g = Instantiate(FireProj, transform.position, transform.rotation) as GameObject;
        g.GetComponent<Rigidbody2D>().velocity = dir;
    }

    public void TakeDamage()
    {

    }

    IEnumerator shoot()
    {

        while (true)
        {
            if (isNeutral)
            {
                ShootAtFire();
                yield return new WaitForSeconds(0.6f);
                ShootAtIce();
                yield return new WaitForSeconds(0.6f);
                ShootAtFire();
                yield return new WaitForSeconds(0.6f);
                ShootAtIce();
                yield return new WaitForSeconds(0.6f);
            } else
            {
                if (isFire)
                {
                    ShootAtIce();
                    yield return new WaitForSeconds(0.1f);
                } else
                {
                    ShootAtFire();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    IEnumerator animate()
    {
        while (true)
        {
            for (int c = 1; c < 10; ++c)
            {
                transform.position += Vector3.up * Time.deltaTime / c;
                yield return new WaitForFixedUpdate();
            }
            for (int c = 1; c < 10; ++c)
            {
                transform.position -= Vector3.up * Time.deltaTime / c;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (dest != null)
        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * speed);
        }
    }
}
