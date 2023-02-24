using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class TestingInputAction : MonoBehaviour
{
    private GameObject plane;
    private PlayerInput playerInput;
    private PlayerInputAction playerInputActions;

    private float brustSpeed = 0f;
    private float _deltaBrustSpeed = 1f;
    public float brustLimit = 10f;

    public float rotateCurveSpeed = 0f;
    public float _deltaRotateCurveSpeed = 1f;
    public float rotateShiftSpeed = 0f;
    public float _deltaRotateShiftSpeed = 1f;
    public float rotateRollSpeed = 0f;
    public float _deltaRotateRollSpeed = 1f;
    public float rotateLimit = 85f;


    private void Awake()
    {
        plane = GameObject.Find("Awacs");
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputAction();
        //playerInputActions.Plane.Enable();
        //playerInputActions.Plane.Jump.performed += Jump;
        playerInputActions.EightKeys.Enable();

    }

    // Start is called before the first frame update
    void Start()
    {
        //playerInputActions.FourKeys.Up.performed += Up_performed;
        //playerInputActions.FourKeys.Down.performed += Down_performed;
        //playerInputActions.FourKeys.Left.performed += Left_performed;
        //playerInputActions.FourKeys.RIght.performed += Right_performed;
    }

    // Update is called once per frame
    void Update()
    {
        float brust = playerInputActions.EightKeys.Brust.ReadValue<float>();
        float breaks= playerInputActions.EightKeys.Break.ReadValue<float>();
        float leftShift = playerInputActions.EightKeys.LeftShift.ReadValue<float>();
        float rightShift = playerInputActions.EightKeys.RIghtShift.ReadValue<float>();
        float up = playerInputActions.EightKeys.Up.ReadValue<float>();
        float down = playerInputActions.EightKeys.Down.ReadValue<float>();
        float leftRoll = playerInputActions.EightKeys.LeftRoll.ReadValue<float>();
        float rightRoll = playerInputActions.EightKeys.RightRoll.ReadValue<float>();
        bool flag = true;
        if (brust == 1) {
            brustSpeed += _deltaBrustSpeed;
        }
        if (breaks == 1)
        {
            if (brustSpeed - _deltaBrustSpeed <= 0) {
                brustSpeed = 0;
            }
            else
                brustSpeed -= _deltaBrustSpeed;

        }
        if (leftShift == 1)
        {
            
            rotateShiftSpeed -= _deltaRotateShiftSpeed;
            if (rotateShiftSpeed > 0)
                rotateShiftSpeed = 0 - rotateShiftSpeed;
            flag = false;
        }
        if (rightShift == 1)
        {
            
            rotateShiftSpeed += _deltaRotateShiftSpeed;
            if (rotateShiftSpeed <= 0)
                rotateShiftSpeed = 0 - rotateShiftSpeed;
            flag = false;
        }

        if (up == 1)
        {
            
            rotateCurveSpeed += _deltaRotateCurveSpeed;
            if (rotateCurveSpeed <= 0)
                rotateCurveSpeed = 0 - rotateCurveSpeed;
            flag = false;
        }
        if (down == 1)
        {
            
            rotateCurveSpeed -= _deltaRotateCurveSpeed;
            if (rotateCurveSpeed > 0)
                rotateCurveSpeed = 0 - rotateCurveSpeed;
            flag = false;
        }
        if (leftRoll == 1)
        {
            
            rotateRollSpeed += _deltaRotateRollSpeed;
            if (rotateRollSpeed <= 0)
            {
                rotateRollSpeed = 0 - rotateRollSpeed;
            }
            flag = false;
        }
        if (rightRoll == 1)
        {
           
            rotateRollSpeed -= _deltaRotateRollSpeed;
            if (rotateRollSpeed > 0) {
                rotateRollSpeed = 0 - rotateRollSpeed;
            }
            flag = false;
        }


        if (flag)
        {
            rotateCurveSpeed = 0;
            rotateShiftSpeed = 0;
            rotateRollSpeed = 0;
            this.transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime, Space.Self);
        }
        else
        {
            if (Math.Abs(rotateCurveSpeed) >= rotateLimit)
                rotateCurveSpeed = (rotateCurveSpeed / Math.Abs(rotateCurveSpeed)) * rotateLimit;
            if (Math.Abs(rotateShiftSpeed) >= rotateLimit)
                rotateShiftSpeed = (rotateShiftSpeed / Math.Abs(rotateShiftSpeed)) * rotateLimit;
            if (Math.Abs(rotateRollSpeed) >= rotateLimit)
                rotateRollSpeed = (rotateRollSpeed / Math.Abs(rotateRollSpeed)) * rotateLimit;
            this.transform.Rotate(new Vector3(rotateCurveSpeed, rotateShiftSpeed, rotateRollSpeed) * Time.deltaTime, Space.Self);
            this.transform.Rotate(new Vector3(0, rotateShiftSpeed, 0) * Time.deltaTime, Space.World);
            this.transform.Rotate(new Vector3(rotateCurveSpeed, 0, rotateRollSpeed) * Time.deltaTime, Space.Self);
            this.transform.Rotate(new Vector3(0, rotateShiftSpeed, 0) * Time.deltaTime, Space.World);
        }
        if (Math.Abs(brustSpeed) >= brustLimit)
            brustSpeed = brustLimit;
        this.transform.Translate(new Vector3(0, 0, brustSpeed) * Time.deltaTime, Space.Self);


    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "speed: "+ brustSpeed);
    }

}
