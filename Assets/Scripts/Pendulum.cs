using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Quaternion start, end;

    [SerializeField, Range(0.0f, 360f)]
    private float angle = 90.0f;
    [SerializeField, Range(0.0f, 5f)]
    private float speed = 2.0f;
    [SerializeField, Range(0.0f, 10.0f)]
    private float startRot = 0.0f;  //start position

    void Start()
    {
        start = PendulumRotation(angle);
        end = PendulumRotation(-angle);
    }

    private void FixedUpdate()
    {
        startRot += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startRot * speed + Mathf.PI / 2) + 1.0f) / 2.0f);

    }
    void RestTimer()
    {
        startRot = 0.0f;
    }

    Quaternion PendulumRotation(float angle)
    {
        var pendulumRotation = transform.rotation;
        var angleZ = pendulumRotation.eulerAngles.z + angle;

        if (angleZ > 180)
        {
            angleZ -= 360;
        }
        else if (angleZ < -180)
        {
            angleZ += 360;
        }
        pendulumRotation.eulerAngles = new Vector3(pendulumRotation.eulerAngles.x, pendulumRotation.eulerAngles.y, angleZ);
        return pendulumRotation;

    }
}
