using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public Rigidbody rb;
    public CharacterController2D controller;
    bool jump = false;
    float translation = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        translation = Input.GetAxis("Horizontal") * speed;
        //if (Input.GetKeyDown("Jump"))
        //{
        //    jump = true;
        //}

        translation *= Time.deltaTime;

        rb.transform.Translate(translation, 0, 0);

    }

    void FixedUpdate()
    {
        controller.Move(translation, false, jump);
        jump = false;

    }
}
