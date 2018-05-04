using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Specialized;
//using System;
using System.Collections.Specialized;

public class ThumbstickLookP1_Script : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    private GameObject spine;
    private GameObject armLeft;
    private GameObject armRight;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public float maxAngleY = 90.0f;
    public float minAngleY = 0.0f;

    GameObject character;

    void Start()
    {
        character = this.transform.parent.gameObject;
        //spine = GameObject.FindGameObjectWithTag("spineRotator");
        //armLeft = GameObject.FindGameObjectWithTag ("armRotatorL");
        //armRight = GameObject.FindGameObjectWithTag ("armRotatorR");
        //Debug.Log(spine);
    }


    void FixedUpdate()
    {
        var md = new Vector2(Input.GetAxisRaw("P1GameLookHorizontal"), Input.GetAxisRaw("P1GameLookVertical"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        //spine.transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
        //armRight.transform.localRotation = Quaternion.AngleAxis (mouseLook.y+100, Vector3.right);
        //armLeft.transform.localRotation = Quaternion.AngleAxis (mouseLook.y+100, Vector3.right);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        mouseLook.y = Mathf.Clamp(mouseLook.y, minAngleY, maxAngleY);

        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}

