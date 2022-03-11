using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class tank_flex : MonoBehaviour

{
    public float rotation = 0.0f; // Скорость вращения
    public float coefStopping = 1.5f; // В исходное сотояние танк возвращается быстрее
    public bool needToUp = false; // Необходимость вернуться в исходное состояние при опущенном корпусе
    public bool needToDown = false; // Необходимость вернуться в исходное состояние при поднятом корпусе
    private float trackCoef = 0.3f; // Гусеницы реагируют хуже, чем основной танк. Симуляция работы подвески
    public float speedRotation = 0.1f; // Изменение вращения
    public float accuracy = 0.3f; // Точность работы скрипта. Ловит башню и корпус, чтобы они не ушли за допустимы углы
    Vector3 normaliz = new Vector3(); // Вектор, содержащий координаты для возвращения в исходное положение
    Quaternion qnorm; // Кватернион, с помощью коротого оперируем с transform.rotation

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flexing();
    }

    public void towerController()
    {
        // Для башни отдельно высчитаваем положение, поскольку она управляется мышкой
        normaliz = new Vector3(changeProp.center.transform.rotation.eulerAngles.x, changeProp.tower.transform.rotation.eulerAngles.y, changeProp.center.transform.rotation.eulerAngles.z); // Положение центра объекта
        qnorm.eulerAngles = normaliz;
        changeProp.tower.transform.rotation = qnorm;
    }

    public void bodyAndTracksController()
    {
        normaliz = new Vector3(changeProp.center.transform.rotation.eulerAngles.x, changeProp.center.transform.rotation.eulerAngles.y, changeProp.center.transform.rotation.eulerAngles.z); // Положение центра объекта
        qnorm.eulerAngles = normaliz;
        // Если отклонение в пределах погрешности, возращаем в исходное положение
        changeProp.body.transform.rotation = qnorm;
        changeProp.leftTrack.transform.rotation = qnorm;
        changeProp.rightTrack.transform.rotation = qnorm;
    }

    public void Flexing()
    {
        // Опускание корпуса
        if ((changeProp.isDisaccelerate == true))
        {
            rotation = speedRotation * -1; // Крутим вниз
            needToUp = true;
        }

        // Возвращение корпуса в исходное состояние
        if ((changeProp.isDisaccelerate == false) && needToUp == true)
        {
            rotation = speedRotation * coefStopping; // Крутим вверх
            // Если отклонение в пределах погрешности, возращаем в исходное положение
            if (Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z - 360) < accuracy || Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z) < accuracy) // Учитываем особенности работы углов Эйлера. Их отсчет зависит от направления их учета
            {
                bodyAndTracksController();
                // Считаем положение для башни отдельно
                towerController();
                // Больше не надо никуда возвращаться
                needToUp = false;
                rotation = 0;
            }
        }

        // Подняте корпуса
        if ((changeProp.isAccelerate == true))
        {
            rotation = speedRotation; // Крутим вверх
            needToDown = true;
        }

        // Возвращение корпуса в исходное состояние, всё то же самое, что и с disaccelerate
        if ((changeProp.isAccelerate == false) && needToDown == true)
        {
            rotation = speedRotation * -coefStopping; // Крутим вниз
            if (Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.localEulerAngles.z - 360) < accuracy || Math.Abs(changeProp.tower.transform.rotation.eulerAngles.z - changeProp.center.transform.rotation.eulerAngles.z) < accuracy)
            {
                bodyAndTracksController();
                // Считаем положение для башни отдельно
                towerController();
                needToDown = false;
                rotation = 0;
            }
        }

        if (changeProp.isAccelerate == false && changeProp.isDisaccelerate && needToUp == false && needToDown == false)
        {
            bodyAndTracksController();
            // Считаем положение для башни отдельно
            towerController();
            rotation = 0;
        }

        Vector3 rotate = new Vector3(0.0f, 0.0f, rotation);
        changeProp.tower.transform.Rotate(rotate * Time.fixedDeltaTime);
        changeProp.body.transform.Rotate(rotate * Time.fixedDeltaTime);
        changeProp.rightTrack.transform.Rotate(rotate * trackCoef * Time.fixedDeltaTime);
        changeProp.leftTrack.transform.Rotate(rotate * trackCoef * Time.fixedDeltaTime);
    }
 }
