using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all character game objects
public abstract class Character : MonoBehaviour
{
    public GeneralFunctions generalFunctions;
    public float moveSpeed{get; set;}
    public bool alive{get; set;}
    public int maxHealth{get; set;}
    public int health{get; set;}
    protected  Rigidbody2D rb;

    
    protected Character(){
        generalFunctions = new GeneralFunctions();  
        alive = true;
    }

    public virtual void takeDamage(int damage)
    {

        health -= damage;
        if(health <= 0)
        {
            alive = false;
            die();
        }        
    }

    public virtual void heal(int restoreAmount)
    { /**/ }

    public virtual void die()
    { /**/ }

    public virtual void shoot(GameObject bulletPrefab, GameObject firePoint, int damage, float speed, Vector3 scale){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().speed = speed;
        bullet.GetComponent<Bullet>().scale = scale;
    }

}