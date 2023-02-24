using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class FlightCamera : MonoBehaviour
{

    public GameObject plane;
    public Vector3 offset;
    private PlayerInput playerInput;
    private PlayerInputAction playerInputActions;
    private float smoothSpeed = 5f;
    [SerializeField] private Vector3 minValue, maxValue;


    void LateUpdate()
    {
        Vector3 desirePos = plane.transform.TransformPoint(offset);

        //Vector3 clampPosition = new Vector3(
        //    Mathf.Clamp(desirePos.x, minValue.x, maxValue.x),
        //    Mathf.Clamp(desirePos.y, minValue.y, maxValue.y),
        //    Mathf.Clamp(desirePos.z, minValue.z, maxValue.z)
        //);

        Vector3 smoothPos = Vector3.Lerp(
            transform.position,
            desirePos,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPos;
        transform.LookAt(plane.transform);
    }


}