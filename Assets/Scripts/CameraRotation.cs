using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform CameraAxisTransform;
    public float RotationSpeed;
    public float minAngle;
    public float maxAngle;
    void Update()
    {       
        var newAngleX = CameraAxisTransform.localEulerAngles.x + Time.deltaTime * RotationSpeed * Input.GetAxis("Mouse Y") * -1 / 1.77f;
        if (newAngleX > 180)
        {
            newAngleX = newAngleX - 360;
        }
        newAngleX = Mathf.Clamp(newAngleX, minAngle, maxAngle);

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + Time.deltaTime * RotationSpeed * Input.GetAxis("Mouse X"), 0);
        CameraAxisTransform.localEulerAngles = new Vector3(newAngleX, 0, 0);
    }
}
