using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleV2 : MonoBehaviour
{

    public GameObject debrisPrefab; //prefab reference for broken object
    public int objectHealth; //Health of object
    public ThrowObjectG incomingDmg; //damage value taken from throw object taken from the incoming object
    public GameObject incomingObject; //bring reference for object that is going to collide
    public Rigidbody rb; //reference for rigidbody to use with ThrowObjectG.throwforce



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "brush") // will look for the object named "Brush" before carrying on with steps
        {
            GameObject incomingObject = GameObject.Find("brush"); //finds the incoming object (version 1 - This should find objects of type)
            ThrowObjectG incomingDmg = incomingObject.GetComponent<ThrowObjectG>();

            objectHealth = objectHealth - incomingDmg.dmg; //takes object health and will subtract the total from incoming damage. Should help with balancing object's damage when breaking
                                                           //objects and damaging players
            if (objectHealth == 0) //when object is dead
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


