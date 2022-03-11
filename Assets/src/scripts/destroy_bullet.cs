using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_bullet : MonoBehaviour
{
    public GameObject bullet;
    private void OnCollisionEnter(Collision collision)
    {
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().freezeRotation = true;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        var decol = bullet.transform.Find("Decal Projector").gameObject;
        decol.SetActive(true);
        bullet.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("del");
        Destroy(bullet, 2f);

        
    }
}
