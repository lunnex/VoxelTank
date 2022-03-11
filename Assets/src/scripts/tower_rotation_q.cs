using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_rotation_q : MonoBehaviour
{
    float mouseOx = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        mouseOx = Input.GetAxis("Mouse X");
        changeProp.tower.transform.rotation = Quaternion.RotateTowards(changeProp.tower.transform.rotation, Quaternion.Euler(0, changeProp.cam.transform.rotation.eulerAngles.y, 0), 1f);
    }
}
