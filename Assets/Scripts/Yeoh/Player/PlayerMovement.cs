using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public bool canMove=true;
    [HideInInspector] public Vector3 input;
    public float sensitivityFactor = 1.5f;

    public float moveSpeed=5, acceleration=10, deceleration=10;

    [HideInInspector] public float defMoveSpeed;
    [HideInInspector] public float inputClamp=1; // for speed debuffs

    void Awake()
    {
        rb=GetComponent<Rigidbody>();

        defMoveSpeed = moveSpeed;
    }

    void OnEnable()
    {
        EventManager.Current.ToggleFirstPersonEvent += OnToggleFirstPerson;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleFirstPersonEvent -= OnToggleFirstPerson;
    }

    void OnToggleFirstPerson(bool toggle)
    {
        canMove = toggle;
    }

    void Update()
    {
        if(canMove) CheckInput();
        else NoInput();
    }

    void CheckInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * sensitivityFactor;

        if(input.magnitude>1) input.Normalize(); // never go past 1

        if(input.magnitude>inputClamp) input = input.normalized * inputClamp; // never go past the speed clamp
    }

    public void NoInput()
    {
        input = Vector3.zero;
    }

    public float velocity;

    void FixedUpdate()
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y=0;
        camRight.y=0;

        Move(input.z, moveSpeed, camForward.normalized);
        Move(input.x, moveSpeed, camRight.normalized);

        velocity = Round(rb.velocity.magnitude, 2);
    }

    void Move(float mult, float magnitude, Vector3 direction)
    {
        float targetSpeed = mult * magnitude;

        float accelRate = Mathf.Abs(targetSpeed)>0 ? acceleration:deceleration; // use decelerate value if no input, and vice versa
    
        float speedDif = targetSpeed - Vector3.Dot(direction, rb.velocity); // difference between current and target speed

        float movement = Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif); // slow down or speed up depending on speed difference

        rb.AddForce(direction * movement);
    }

    float Round(float num, int decimalPlaces)
    {
        int factor=1;

        for(int i=0; i<decimalPlaces; i++)
        {
            factor *= 10;
        }

        return Mathf.Round(num * factor) / (float)factor;
    }

    int tweenInputClampLt=0;
    public void TweenInputClamp(float to, float time=.25f)
    {
        LeanTween.cancel(tweenInputClampLt);

        if(time>0)
        {
            tweenInputClampLt = LeanTween.value(inputClamp, to, time)
                .setEaseInOutSine()
                .setOnUpdate( (float value)=>{inputClamp=value;} )
                .id;
        }
        else inputClamp=to;
    }

    int tweenSpeedLt=0;
    public void TweenSpeed(float to, float time=.25f)
    {
        LeanTween.cancel(tweenSpeedLt);

        if(time>0)
        {
            tweenSpeedLt = LeanTween.value(moveSpeed, to, time)
                .setEaseInOutSine()
                .setOnUpdate( (float value)=>{moveSpeed=value;} )
                .id;
        }
        else moveSpeed=to;
    }

    public void Push(float force, Vector3 direction)
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        rb.AddForce(direction*force, ForceMode.Impulse);
    }
}
