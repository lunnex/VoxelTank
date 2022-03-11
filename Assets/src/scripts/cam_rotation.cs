using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_rotation : MonoBehaviour
{
    //Vector3 mouseAx = new Vector3();
    private float mouseOx = 0.0f;
    private float mouseOy = 0.0f;
    public float camSensOXCR = 1.0f;
    public float camSensOYCR = 1.0f;
    private float rotationY = 0.0f;
    Vector3 normaliz = new Vector3();
    Quaternion qnorm;
    //Vector3 towerPos = new Vector3();
    //Quaternion qt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeProp.camSensOY = camSensOYCR;
        changeProp.camSensOX = camSensOXCR;
        mouseOx = Input.GetAxis("Mouse X");
        mouseOy = Input.GetAxis("Mouse Y");
        changeProp.cam.transform.Rotate(0, mouseOx * changeProp.camSensOX, mouseOy * changeProp.camSensOY);
        normaliz = new Vector3(0.0f, changeProp.cam.transform.rotation.eulerAngles.y, Mathf.Clamp(changeProp.cam.transform.rotation.eulerAngles.z, 20, 100));
        qnorm.eulerAngles = normaliz;
        changeProp.cam.transform.rotation = qnorm;
    }
}
