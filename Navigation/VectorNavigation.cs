using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorNavigation : MonoBehaviour
{
    public Vector3 heading;

	private Rigidbody rb;

	private float thrust = 1f;

	void Start() {
		heading = Vector3.zero;
		rb = GetComponentInParent<Rigidbody>();
	}

	void Update() {
        
		rb.AddForce( thrust * heading );

        ResetHeading();
	}

	public Vector3 GetHeading() {
		return heading;
	}

	public void SetHeading( Vector3 newHeading ) {
		heading = newHeading;
	}

	public void SetHeading( float x, float y, float z ) {
		heading.x = x;
		heading.y = y;
		heading.z = z;
	}

	public void ResetHeading() {
		heading = Vector3.zero;
	}

	public void AddHeading( Vector3 additionalHeading ) {
		heading += additionalHeading;
	}

	public void AddHeading( float x, float y, float z ) {
		heading.x += x;
		heading.y += y;
		heading.z += z;
	}
}
