using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrolling : MonoBehaviour {
    private Renderer rand;
    private Material mat;
    public float ScrollSpeed;

    

	// Use this for initialization
	void Start () {
        rand = GetComponent<Renderer>();
        mat = rand.material;
	}
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset += new Vector2(0, ScrollSpeed * Time.deltaTime);
	}
}
