using System.Collections;
using UnityEngine;

public class AutoTracking: MonoBehaviour
{
    public GameObject target;
    public GameObject plane;
    public double speed = 10;    
    public float angleStandard = 45.0f;  
    private bool move = true;
    private bool hasLunched = false;
    public float rotateSpeed = 10f;

    private PlayerInputAction playerInputActions;

    void Start()
    {

        //distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        playerInputActions = new PlayerInputAction();
        //playerInputActions.Plane.Enable();
        //playerInputActions.Plane.Jump.performed += Jump;
        playerInputActions.Plane.Enable();
        
    }

    private void Update()
    {
        float first = playerInputActions.Plane.Fire.ReadValue<float>();
        if (first == 1 && !hasLunched)
        {
            StartCoroutine(StartShoot());
            hasLunched = true;       
        }
    }

    private void LateUpdate()
    {
        if (!hasLunched)
        {
            this.transform.position = plane.transform.position;
            this.transform.rotation = plane.transform.rotation;
        }

    }
    IEnumerator StartShoot()
    {

        while (move)
        {
            Vector3 targetPos = target.transform.position;
            if (speed <= 50)
                speed = speed + 1;
  
            rotateSpeed += 1f;

            Quaternion rotTarget = Quaternion.LookRotation(target.transform.position - transform.position);
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotateSpeed * Time.deltaTime);
            
            //this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
            float currentDist = Vector3.Distance(this.transform.position, target.transform.position);
            if (currentDist < 0.5f)
            {
                move = false;
                Debug.Log("destroy");
                Destroy(this.gameObject);
                yield break;
                
            }

            this.transform.Translate(Vector3.forward * Mathf.Min((float)(speed * Time.deltaTime), currentDist));
            yield return null;
        }
    }


}