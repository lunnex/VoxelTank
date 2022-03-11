using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public List<GameObject> obj = new List<GameObject>();
    //public Texture tex;
    private GameObject bullet;
    private int i = 0;

    //public Material mat1;
    public float force;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            bullet = Instantiate(obj[i], new Vector3(changeProp.shootPoint.transform.position.x, changeProp.shootPoint.transform.position.y, changeProp.shootPoint.transform.position.z), obj[i].transform.rotation);
            bullet.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            bullet.GetComponent<Rigidbody>().freezeRotation = true;
            bullet.GetComponent<Rigidbody>().AddForce(changeProp.shootPoint.transform.forward * -force, ForceMode.Impulse);
            var decol = bullet.transform.Find("Decal Projector").gameObject;
            decol.SetActive(false);
            //decol.GetComponent<Material>().SetTexture("",tex);
            Debug.Log(i);
            i++;

        }

        if(i >= obj.Count)
        {
            i = 0;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        print("a");
        //Destroy(bullet);
    }
}
