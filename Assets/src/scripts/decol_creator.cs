using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decol_creator : MonoBehaviour
{
    public GameObject decol;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        GameObject dec;
        var collQuater = collision.transform.rotation;
        collQuater.Normalize();
        //dec = Instantiate(decol, new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z), new Quaternion(0,1,0,1));
        print(collision.transform.rotation);
    }
}
