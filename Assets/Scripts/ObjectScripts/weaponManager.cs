using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour {
    public GameObject Cube_cell;

    void OnMouseDown()
    {
        if (Cube_cell)
        {
            Destroy(gameObject);
        }   
    }
}

