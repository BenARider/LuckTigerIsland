using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatePlayer : MonoBehaviour {
    public Animator animator;
    Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 compare = transform.position - lastPosition;
        if (compare.y < -0.01f)
        {
            animator.SetInteger("X", 0);
            animator.SetInteger("Y", -1);
        }else if (compare.y > 0.01f)
        {
            animator.SetInteger("X", 0);
            animator.SetInteger("Y", 1);
        }
        else if (compare.x < -0.01f)
        {
            animator.SetInteger("X", -1);
            animator.SetInteger("Y", 0);
        }
        else if (compare.x > 0.01f)
        {
            animator.SetInteger("X", 1);
            animator.SetInteger("Y", 0);
        }
        else
        {
            animator.SetInteger("X", 0);
            animator.SetInteger("Y", 0);

        }



        lastPosition = transform.position;
    }
}
