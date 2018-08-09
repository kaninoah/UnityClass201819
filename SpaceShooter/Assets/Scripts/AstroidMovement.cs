using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidMovement : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField]
    private float angularSpeed, 
                            Speed;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnEnable()
    {
        rb.angularVelocity = Random.onUnitSphere * angularSpeed;
        rb.velocity = Vector3.back * Speed;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
