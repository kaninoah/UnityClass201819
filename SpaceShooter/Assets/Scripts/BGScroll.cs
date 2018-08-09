using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour {
    private Rigidbody rb;
    public float Speed;
    private Vector3 rollbackAmount;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        rollbackAmount = new Vector3(0, 0, 40.96f);
	}

    public void StopScroll()
    {
        rb.velocity = Vector3.zero;
    }

    public void StartScroll()
    {
        rb.velocity = new Vector3(0, 0, -Speed);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BGBumper"))
        {
            rb.position += rollbackAmount;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
