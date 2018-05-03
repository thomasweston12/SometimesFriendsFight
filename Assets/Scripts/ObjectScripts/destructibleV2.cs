using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleV2 : MonoBehaviour
{

    public GameObject debrisPrefab; //prefab reference for broken object
    public float objectHealth; //Health of object
    public ThrowObjectG incomingDmg; //damage value taken from throw object taken from the incoming object
    public GameObject incomingObject; //bring reference for object that is going to collide
    public Rigidbody rb;  //reference for rigidbody to use with ThrowObjectG.throwforce
    float collisionTime = 0;
    private float cooldown = 0.1f;

    void CollisionTimer()
    {
        collisionTime += Time.deltaTime;
        if (collisionTime > cooldown)
        {
            return;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Time");
        Debug.Log(Time.time);

        CollisionTimer();
        if (collision.gameObject.tag == "PhysicsObject") // will look for the incoming Physics Object before carrying on with steps
        {
            GameObject incomingObject = collision.gameObject; //finds the incoming object
            ThrowObjectG incomingDmg = incomingObject.GetComponent<ThrowObjectG>();
            rb = incomingObject.gameObject.GetComponent<BoxCollider>().attachedRigidbody;
            rb.AddForce(5, 5, 5);
            Debug.Log("Before Health");
            Debug.Log(objectHealth);

            objectHealth = objectHealth - incomingDmg.totaldmg; //takes object health and will subtract the total from incoming damage. Should help with balancing object's damage when breaking
                                                           //objects and damaging players
            Debug.Log("After Health");
            Debug.Log(objectHealth);

            if (objectHealth <= 0) //when object is dead
            {
                if (debrisPrefab)
                {
                    Instantiate(debrisPrefab, transform.position, transform.rotation); //spawns broken version of object
                }
                Destroy(gameObject); //deletes the original object
            }
        }
    }
}


