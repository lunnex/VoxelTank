using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class changeProp
{
    public static bool isDisaccelerate;
    public static bool isAccelerate;
    public static GameObject tower;
    public static GameObject body;
    public static GameObject rightTrack;
    public static GameObject leftTrack;
    public static GameObject center;
    public static GameObject cam;
    public static GameObject camChildren;
    public static GameObject tank;
    public static GameObject bullet;
    public static GameObject shootPoint;
    public static float camSensOY;
    public static float camSensOX;
}

public class tank_motion : MonoBehaviour
{
    public GameObject obj;
    private GameObject tank;
    public float speed = 5.0f; // Коэффициент, регулирующий скорость движения по горизонтали
    public  float maxSpeed = 1.5f; // Максимальная скорость
    public float moveHorizontal = 0f; // Результирующий вектор движения по горизонтали
    private float moveVertical = 0f; // Результирующий вектор движения по вертикали
    public float moveRotation = 0f; // Вектор поворота
    public float maxRotateSpeed = 5.0f; // Максимальная скорость поворота
    private float rotateForce = 1.0f; // Сила поворота
    public float accelerationHorizontal = 0.3f; // Ускорение при движении по горизонтали
    public float accelerationRotation = 0.3f; // Ускорение при повороте
    public float maxAcceleration = 0.1f; // Коэффициент для ускорения
    public float rotationSpeed = 5.0f; // Скорость поворота
    public float stopCoef = 5.0f; // Коэффициент к ускорению при останове
    public float stopCoefRotation = 10.0f; // Коэффициент к ускорению при останове во время поворота
    public bool complexBackMoving; // Делает поведение танка особенным во время движения назад-влево и назад-вправо

    void Start()
    {
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    private void Move()
    {
        // Вначале не двигаемся и не тормозим
        changeProp.isDisaccelerate = false;
        changeProp.isAccelerate = false;
        complexBackMoving = false;

        // Едем назад и влево
        if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A)) && (Math.Abs(moveHorizontal) <= maxSpeed) && (Math.Abs(moveRotation) <= maxRotateSpeed))
        {
            if (moveHorizontal != maxSpeed) // Если разгоняемся, то перед корпуса немного опускается
            {
                changeProp.isAccelerate = true;
            }
            complexBackMoving = true;
            moveHorizontal += maxAcceleration * accelerationHorizontal;
            moveRotation += rotateForce * accelerationRotation;

        }

        // Едем назад и вправо
        if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.D)) && (Math.Abs(moveHorizontal) <= maxSpeed) && (Math.Abs(moveRotation) <= maxRotateSpeed))
        {
            if (moveHorizontal != maxSpeed) // Если разгоняемся, то перед корпуса немного опускается
            {
                changeProp.isAccelerate = true;
            }
            complexBackMoving = true;
            moveHorizontal += maxAcceleration * accelerationHorizontal;
            moveRotation -= rotateForce * accelerationRotation;

        }

        // Едем вперед
        if (Input.GetKey(KeyCode.W) && (Math.Abs(moveHorizontal) <= maxSpeed) && complexBackMoving == false)
        {
            if (moveHorizontal != maxSpeed * -1.0f) // Если разгоняемся, то перед корпуса немного поднимается
            {
                changeProp.isDisaccelerate = true;
            }
           // changeProp.isAccelerate = true;
            moveHorizontal -= maxAcceleration * accelerationHorizontal;

        }

        // Едем назад
        if (Input.GetKey(KeyCode.S) && (Math.Abs(moveHorizontal) <= maxSpeed) && complexBackMoving == false)
        {
            if (moveHorizontal != maxSpeed) // Если разгоняемся, то перед корпуса немного опускается
            {
                changeProp.isAccelerate = true;
            }
            moveHorizontal += maxAcceleration * accelerationHorizontal;
        }

        // Тормозим, если ехали назад
        if (!Input.GetKey(KeyCode.S) && complexBackMoving == false)
        {
            if (moveHorizontal > 0.0f)
            {
                moveHorizontal -= maxAcceleration * accelerationHorizontal * stopCoef;
                changeProp.isAccelerate = true;
            }

            // Если движение очень медленное, просто останавливаемся
            if ((Math.Abs(moveHorizontal) < 0.3) && (!Input.GetKey(KeyCode.W)))
            {
                moveHorizontal = 0.0f;
            }
        }

        // Тормозим, если ехали вперед
        if (!Input.GetKey(KeyCode.W) && complexBackMoving == false) {
            if (moveHorizontal < 0.0f)
            {
                moveHorizontal += maxAcceleration * accelerationHorizontal * stopCoef;
                changeProp.isDisaccelerate = true;
            }

            // Если движение очень медленное, просто останавливаемся
            if ((Math.Abs(moveHorizontal) < 0.3) && !Input.GetKey(KeyCode.S))
            {
                moveHorizontal = 0.0f;
            }
        }

        // Поворачиваем налево
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.S)) && (Math.Abs(moveRotation) <= maxRotateSpeed))
        {
            moveRotation -= rotateForce * accelerationRotation;
        }

        // Поворачиваем направо
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.S)) && (Math.Abs(moveRotation) <= maxRotateSpeed))
        {
            moveRotation += rotateForce * accelerationRotation;
            if (Input.GetKey(KeyCode.S))
            {
                moveRotation -= rotateForce * accelerationRotation;
            }
        }

         // Перестаем поворачивать, если поворачивали налево
        if (!Input.GetKey(KeyCode.A) && complexBackMoving == false)
        {
            if (moveRotation < 0.0f)
            {
                moveRotation += maxAcceleration * accelerationRotation * stopCoefRotation;
            }

            // Если движение очень медленное, просто останавливаемся
            if ((Math.Abs(moveRotation) < 0.3) && (!Input.GetKey(KeyCode.D)))
            {
                moveRotation = 0.0f;
            }
        }

        // Перестаем поворачивать, если поворачивали направо
        if (!Input.GetKey(KeyCode.D) && complexBackMoving == false)
        {
            if (moveRotation > 0.0f)
            {
                moveRotation -= maxAcceleration * accelerationRotation * stopCoefRotation;
            }

            // Если движение очень медленное, просто останавливаемся
            if ((Math.Abs(moveRotation) < 0.3) && (!Input.GetKey(KeyCode.A)))
            {
                moveRotation = 0.0f;
            }
        }
        
        // Ограничиваем максимальную скорость
        if (moveHorizontal > maxSpeed)
        {
            changeProp.isDisaccelerate = false;
            //moveHorizontal = maxSpeed - maxAcceleration * accelerationHorizontal;
            moveHorizontal = maxSpeed;
        }
        if (moveHorizontal < maxSpeed * -1.0f)
        {
            changeProp.isDisaccelerate = false;
            //moveHorizontal = maxSpeed * -1.0f + maxAcceleration * accelerationHorizontal;
            moveHorizontal = maxSpeed * -1.0f;
        }

        // Ограничиваем максимальную скорость поворота
        if (moveRotation > maxRotateSpeed)
        {
            moveRotation = maxRotateSpeed - maxAcceleration * accelerationHorizontal;
        }
        if (moveRotation < maxRotateSpeed * -1.0f)
        {
            moveRotation = maxRotateSpeed * -1.0f + maxAcceleration * accelerationHorizontal;
        }

        if (moveHorizontal == 0.0f)
        {
            changeProp.isAccelerate = false;
            changeProp.isDisaccelerate = false;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 rotate = new Vector3(0.0f, moveRotation, 0.0f);
        changeProp.tank.transform.Translate(movement * speed * Time.fixedDeltaTime);
        changeProp.tank.transform.Rotate(rotate * rotationSpeed * Time.fixedDeltaTime);
    }
}
