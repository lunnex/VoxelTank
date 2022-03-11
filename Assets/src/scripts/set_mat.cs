using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_mat : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
