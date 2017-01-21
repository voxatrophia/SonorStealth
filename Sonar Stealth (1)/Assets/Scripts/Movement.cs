using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float maxSpeed;
    Rigidbody subRB;

	// Use this for initialization
	void Start () {
        subRB = GetComponent<Rigidbody>();   	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ApplyTorqueAndForce();
	}

    void ApplyTorqueAndForce()
    {
        float turnRight = Input.GetAxis("LeftScrew") * maxSpeed * Time.deltaTime;
        float turnLeft = Input.GetAxis("RightScrew") * maxSpeed * Time.deltaTime;

        subRB.AddTorque(transform.up * (turnRight-turnLeft));
        subRB.AddForce(transform.forward * (turnRight + turnLeft));
    }
}
