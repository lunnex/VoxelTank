using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_rotation : MonoBehaviour
{
    //Vector3 mouseAx = new Vector3();
    float mouseOx = 0.0f;
    float angle = 0.0f;
    //Vector3 towerPos = new Vector3();
    //Quaternion qt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseOx = Input.GetAxis("Mouse X");
        changeProp.tower.transform.Rotate(0, mouseOx * 10, 0);
    }
}
