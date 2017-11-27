using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleV2 : MonoBehaviour {

    public GameObject debrisPrefab;

    void OnMouseDown()
    {
        if (debrisPrefab)
        { 
        Instantiate(debrisPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
