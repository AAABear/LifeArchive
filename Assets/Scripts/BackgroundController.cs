using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public float speed = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = transform.localPosition;
        pos.x -= speed * Time.deltaTime;

        transform.localPosition = pos;
	}
}
