using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Vector3 direction = Vector3.zero;
    private Animator anim;
    GameObject controller;
    private void Start()
    {
        variableJoystick = GameObject.FindWithTag("Controller").GetComponent<VariableJoystick>();
        

    }
    public void FixedUpdate()
    {
        direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        if(direction!=Vector3.zero)
        {
            anim = GetComponent<Animator>();
            rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
            anim.SetFloat("velocity", 0.2f);
        }
        else
        {
            anim = GetComponent<Animator>();
            anim.SetFloat("velocity", 0.0f);
        }
           
    }
}