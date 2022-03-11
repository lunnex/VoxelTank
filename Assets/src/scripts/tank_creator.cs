using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_creator : MonoBehaviour
{
    public GameObject obj;
    private GameObject tank;
    // Start is called before the first frame update
    void Start()
    {
        tank = Instantiate(obj, new Vector3(0.0f, 0.0f, -19f), obj.transform.rotation) as GameObject;
        changeProp.tank = tank;
        changeProp.cam = GameObject.Find("Center_cam");
        changeProp.camChildren = GameObject.Find("Center_cam/Main Camera");
        changeProp.tower = GameObject.Find("Center_tower");
        changeProp.body = GameObject.Find("Center_body");
        changeProp.rightTrack = GameObject.Find("track_tank");
        changeProp.leftTrack = GameObject.Find("track_left_tank");
        changeProp.center = GameObject.Find("Center");
        changeProp.shootPoint = GameObject.Find("Center_tower/Shoot_point");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
