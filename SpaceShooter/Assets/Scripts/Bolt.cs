using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour {

    private Rigidbody rb;
    public float Speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * Speed;
	}

    public void Bomb()
    {
        gameObject.SetActive(false);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
