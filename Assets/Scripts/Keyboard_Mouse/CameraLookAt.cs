using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public float sensitivity = 5f;
    public Vector2 xClamp = new Vector2(-360f, 360f);
    public Vector2 yClamp = new Vector2(-60f, 60f);

    Quaternion originalRotation;
    Vector2 input;

    // Use this for initialization
    void Start()
    {
        originalRotation = transform.localRotation;
        input = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        input.x += Input.GetAxis("Mouse X") * sensitivity;
        input.y += Input.GetAxis("Mouse Y") * sensitivity;
        input.x = ClampAngle(input.x, xClamp.x, xClamp.y);
        input.y = ClampAngle(input.y, yClamp.x, yClamp.y);
        Quaternion xQuat = Quaternion.AngleAxis(input.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(input.y, -Vector3.right);

        transform.localRotation = originalRotation * xQuat * yQuat;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
