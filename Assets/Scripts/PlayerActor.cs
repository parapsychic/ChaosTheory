using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    [SerializeField] float health = 100f;

    void TakeDamage(float damage)
    {
        health -= damage;

        //Add particle effects
        isDead();
    }

    void isDead()
    {
        if(health <= 0)
        {
            //Die animation
            print("dead");
            //Delete Sprite Renderer and remove  Movement
            //Wait for 3 secs
            //Change scene
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
            TakeDamage(collision.GetComponent<BulletBehaviour>().damage);
    }
}
