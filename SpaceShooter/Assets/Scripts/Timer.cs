using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float Time;

	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
