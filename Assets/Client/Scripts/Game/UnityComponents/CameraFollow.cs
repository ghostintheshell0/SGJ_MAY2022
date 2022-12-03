using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Camera Camera;

    public Vector3 OffsetPosition;
    public float PositionLerp;

    [Space(20)]
    public float Angle;
    public float RotationSpeed;
    public float AngleLerp;
    public float XAngle;
    public float MinXAngle;
    public float MaxXAngle;

    [Space(20)]
    public float Distance;
    public float MinDistance;
    public float MaxDistance;
    public float DistanceLerp;
    public float ScrollSpeed;

    private Vector3 PreviousMousePosition;


    private void Update()
    {
        var pos = Camera.transform.localPosition;
        if(Input.GetMouseButtonDown(1))
        {
            PreviousMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(1))
        {
            var mouseDelta = Input.mousePosition - PreviousMousePosition;

            Angle += mouseDelta.x * RotationSpeed;
            Angle = Angle % 360;
            XAngle += mouseDelta.y * RotationSpeed;

            XAngle = Mathf.Clamp(XAngle, MinXAngle, MaxXAngle);
            PreviousMousePosition = Input.mousePosition;
        }

        var scrollDelta = Input.mouseScrollDelta;
        if(scrollDelta.sqrMagnitude != 0)
        {
            Distance += scrollDelta.y * ScrollSpeed;
            Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
        }
/*
        if(Target)
        {
            var newPos = Vector3.Lerp(transform.position, Target.transform.position, Speed* Time.deltaTime);
            transform.position =  Offset + newPos;
        }
*/
        pos.z = Distance;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(XAngle, Angle, 0), AngleLerp);
        Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition, pos, DistanceLerp);
        if(Target != default)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position+OffsetPosition, PositionLerp);
        }
        
    }
}
