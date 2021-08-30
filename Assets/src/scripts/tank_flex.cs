using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class tank_flex : MonoBehaviour

{
    public float rotation = 0.0f; // �������� ��������
    public float coefStopping = 1.5f; // � �������� �������� ���� ������������ �������
    private bool needToUp = false; // ������������� ��������� � �������� ��������� ��� ��������� �������
    private bool needToDown = false; // ������������� ��������� � �������� ��������� ��� �������� �������
    private float trackCoef = 0.3f; // �������� ��������� ����, ��� �������� ����. ��������� ������ ��������
    public float speedRotation = 0.1f; // ��������� ��������
    public float accuracy = 0.3f; // �������� ������ �������. ����� ����� � ������, ����� ��� �� ���� �� ��������� ����
    Vector3 normaliz = new Vector3(); // ������, ���������� ���������� ��� ����������� � �������� ���������
    Quaternion qnorm; // ����������, � ������� �������� ��������� � transform.rotation

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flexing();
    }

    public void Flexing()
    {
        // ��������� �������
        if ((changeProp.isDisaccelerate == true))
        {
            rotation = speedRotation * -1; // ������ ����
            needToUp = true;
        }

        // ����������� ������� � �������� ���������
        if ((changeProp.isDisaccelerate == false) && needToUp == true)
        {
            rotation = speedRotation * coefStopping; // ������ �����
            // ���� ���������� � �������� �����������, ��������� � �������� ���������
            if (Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z - 360) < accuracy || Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z) < accuracy) // ��������� ����������� ������ ����� ������. �� ������ ������� �� ����������� �� �����
            {
                normaliz = new Vector3(changeProp.center.transform.rotation.eulerAngles.x, changeProp.center.transform.rotation.eulerAngles.y, changeProp.center.transform.rotation.eulerAngles.z); // ��������� ������ �������
                qnorm.eulerAngles = normaliz;
                // ���� ���������� � �������� �����������, ��������� � �������� ���������
                changeProp.tower.transform.rotation = qnorm; 
                changeProp.body.transform.rotation = qnorm;
                changeProp.leftTrack.transform.rotation = qnorm;
                changeProp.rightTrack.transform.rotation = qnorm;
                // ������ �� ���� ������ ������������
                needToUp = false;
                rotation = 0;
            }
        }

        // ������� �������
        if ((changeProp.isAccelerate == true))
        {
            rotation = speedRotation; // ������ �����
            needToDown = true;
        }

        // ����������� ������� � �������� ���������, �� �� �� �����, ��� � � disaccelerate
        if ((changeProp.isAccelerate == false) && needToDown == true)
        {
            rotation = speedRotation * -coefStopping; // ������ ����
            if (Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z - 360) < accuracy || Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z) < accuracy)
            {
                normaliz = new Vector3(changeProp.center.transform.rotation.eulerAngles.x, changeProp.center.transform.rotation.eulerAngles.y, changeProp.center.transform.rotation.eulerAngles.z);
                qnorm.eulerAngles = normaliz;
                changeProp.tower.transform.rotation = qnorm;
                changeProp.body.transform.rotation = qnorm;
                changeProp.leftTrack.transform.rotation = qnorm;
                changeProp.rightTrack.transform.rotation = qnorm;
                needToDown = false;
                rotation = 0;
            }
        }

        Vector3 rotate = new Vector3(0.0f, 0.0f, rotation);
        changeProp.tower.transform.Rotate(rotate * Time.fixedDeltaTime);
        changeProp.body.transform.Rotate(rotate * Time.fixedDeltaTime);
        changeProp.rightTrack.transform.Rotate(rotate * trackCoef * Time.fixedDeltaTime);
        changeProp.leftTrack.transform.Rotate(rotate * trackCoef * Time.fixedDeltaTime);
    }
}
