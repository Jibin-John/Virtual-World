using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Animator anim;
    public VariableJoystick variableJoystick;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /*float x_hor = Input.GetAxis("Horizontal");
        float z_ver = Input.GetAxis("Vertical");

        Vector3 x_move = transform.right * x_hor;
        Vector3 z_move = transform.forward * z_ver;

        Vector3 motion_velocity = (x_move + z_move).normalized * speed*/

        //mobile input

        Vector3 motion_velocity = Vector3.right * variableJoystick.Horizontal + Vector3.left * variableJoystick.Vertical;

        Move(motion_velocity);
        
    }

    private void Move(Vector3 motion_velocity)
    {
        velocity = motion_velocity;
    }
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.S))
        {
            rb.transform.Rotate(0f, 5f, 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(0f, -5f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(0f, 5f, 0f);
        }

        if (velocity != Vector3.zero)
        {
            Debug.Log("Success");
            //anim.SetFloat("velocity", 0.2f);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            //rb.AddForce(velocity * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            //anim.SetInteger("condition", 1);
        }
        else
        {
            anim.SetFloat("velocity", 0.0f);
        }

    }
    void Testing()
    {
        Debug.Log("Working");
    }
}
