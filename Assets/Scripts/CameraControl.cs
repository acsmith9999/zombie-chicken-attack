using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    //Not currently attached to any gameobject, this script is for future use

    //public Transform followTransform;

    private Vector3 targetPos;

    public Vector2 maxPosition;
    public Vector2 minPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        //this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);


    }
}
