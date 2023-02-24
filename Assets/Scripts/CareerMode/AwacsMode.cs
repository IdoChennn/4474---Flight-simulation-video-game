using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// https://gamedevbeginner.com/input-in-unity-made-easy-complete-guide-to-the-new-system/#input_action_assets
// new input system guideline
public class AwacsMode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aimingObj;
    public GameObject clone;
    private PlayerInputAction playerInputActions;
    bool hasLunched;
    private float lunchedTime;

    public InputAction planeControl;
    void Start()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Plane.Enable();
        hasLunched = false;
        lunchedTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        float first = playerInputActions.Plane.Fire.ReadValue<float>();
        //if (Time.time - lunchedTime >= 5f)
        //    hasLunched = false;
        //else
        //    hasLunched = true;

        //if (first == 1 && !hasLunched)
        //{
        //    lunchedTime = Time.time;
        //    Instantiate(clone, this.transform.position, new Quaternion(0,0,0,0), this.transform);
        //}


    }

    private void LateUpdate()
    {
        //if (!hasLunched)
        //    aimingObj.transform.position = transform.position;
    }

    private void OnEnable()
    {
        planeControl.Enable();
    }

    private void OnDisable()
    {
        planeControl.Disable();
    }


    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 100, 20), "hasLunched: " + hasLunched);
    //}

}
