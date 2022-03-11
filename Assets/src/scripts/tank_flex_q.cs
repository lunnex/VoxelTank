using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tank_flex_q : MonoBehaviour
{

    public float angle = 2.0f; // Максимально допуспимый угол
    public float speedRotation = 0.03f; // Скорость поворота
    public float stopCoef = 0.25f; // Коэффициент к скорости повората при торжможении
    public bool needToUp = false; // Необходимость вернуться в исходное состояние при опущенном корпусе
    public bool needToDown = false; // Необходимость вернуться в исходное состояние при поднятом корпусе
    public float finalSpeedRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Controller(float angle, float speedRotation, float stopCoef)
    {
        finalSpeedRotation = speedRotation * stopCoef;
        changeProp.tower.transform.rotation = Quaternion.Slerp(changeProp.tower.transform.rotation, Quaternion.Euler(changeProp.tower.transform.rotation.eulerAngles.x, changeProp.tower.transform.rotation.eulerAngles.y, angle), finalSpeedRotation);
        changeProp.body.transform.rotation = Quaternion.Slerp(changeProp.body.transform.rotation, Quaternion.Euler(changeProp.body.transform.rotation.eulerAngles.x, changeProp.body.transform.rotation.eulerAngles.y, angle), finalSpeedRotation);
        changeProp.rightTrack.transform.rotation = Quaternion.Slerp(changeProp.rightTrack.transform.rotation, Quaternion.Euler(changeProp.rightTrack.transform.rotation.eulerAngles.x, changeProp.rightTrack.transform.rotation.eulerAngles.y, angle / 3), finalSpeedRotation);
        changeProp.leftTrack.transform.rotation = Quaternion.Slerp(changeProp.leftTrack.transform.rotation, Quaternion.Euler(changeProp.leftTrack.transform.rotation.eulerAngles.x, changeProp.leftTrack.transform.rotation.eulerAngles.y, angle / 3), finalSpeedRotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flexing();
    }

    public void Flexing()
    {
        // Опускание корпуса
        if ((changeProp.isDisaccelerate == true))
        {
            needToUp = true;
            Controller(-angle, speedRotation, 1);
            
        }

        // Поднятие корпуса
        if ((changeProp.isDisaccelerate == false) && needToUp == true)
        {
            needToUp = false;
            Controller(0, speedRotation, stopCoef);
        }

        // Опускание корпуса
        if ((changeProp.isAccelerate == true))
        {
            needToDown = true;
            Controller(angle, speedRotation, 1);
        }

        // Поднятие корпуса
        if ((changeProp.isAccelerate == false) && needToDown == true)
        {
            needToDown = false;
            Controller(0, speedRotation, stopCoef);
        }
    }
}