using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNPhysics : VectorNavigation
{

    public PID pitchPID;
    public PID rollPID;
    public PID yawPID;

    public float pitchTorque;
    public float rollTorque;
    public float yawTorque;

    private int errorBufferSize = 30;

    private float dtheta;
    private float dphi;
    private float dpsi;
    private float accuracy = 0.1f;
    private Vector3 torque;

    public override void Start() {
        base.Start();


        pitchPID = new PID( 1f, 0f, 10f, errorBufferSize );
        rollPID = new PID( 1f, 0f, 10f, errorBufferSize );
        yawPID = new PID( 1f, 0f, 10f, errorBufferSize );

        //pitchPID = new PID( errorBufferSize );
        //rollPID = new PID( errorBufferSize );
        //yawPID = new PID( errorBufferSize );

        rb.inertiaTensor = rb.mass * Vector3.one;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Yaw();
        if ( dtheta < accuracy ) {
            Pitch();
        }

        rb.AddForce( thrust * transform.forward );

        ResetHeading();
    }

    void Roll() {

        torque.x = 0f;
        torque.y = 0f;

        // Rolling does nothing to change the direction you face. Instead, roll so that the upward direction faces the target, to favor pulling up like a plane
        dpsi = Vector3.SignedAngle( transform.up, heading, transform.forward );
        torque.z = rollPID.Process( dpsi, 0, Time.deltaTime );
        if ( torque.z < 0 ) {
            torque.z = Mathf.Max( -rollTorque, torque.z );
        }
        else {
            torque.z = Mathf.Min( rollTorque, torque.z );
        }

        rb.AddRelativeTorque( torque );
    }

    void Pitch() {

        torque.y = 0f;
        torque.z = 0f;

        dphi = -Vector3.SignedAngle( transform.forward, heading, -transform.right );
        torque.x = pitchPID.Process( dphi, 0, Time.deltaTime );
        if ( torque.x < 0 ) {
            torque.x = Mathf.Max( -pitchTorque, torque.x );
        }
        else {
            torque.x = Mathf.Min( pitchTorque, torque.x );
        }

        rb.AddRelativeTorque( torque );
    }

    void Yaw() {

        torque.x = 0f;
        torque.z = 0f;

        dtheta = Vector3.SignedAngle( transform.forward, heading, transform.up );
        torque.y = yawPID.Process( dtheta, 0, Time.deltaTime );
        if ( torque.y < 0 ) {
            torque.y = Mathf.Max( -yawTorque, torque.y );
        }
        else {
            torque.y = Mathf.Min( yawTorque, torque.y );
        }

        rb.AddRelativeTorque( torque );
    }
}
