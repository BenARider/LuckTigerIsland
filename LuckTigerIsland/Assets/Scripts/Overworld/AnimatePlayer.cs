using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour {
    Animator animator;
    Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 compare = transform.position - lastPosition;

        if(compare.x > 0)
        {
            animator.SetInteger("X", 1);
            animator.SetInteger("Y", 0);
        }
        if (compare.x < 0)
        {
            animator.SetInteger("X", -1);
            animator.SetInteger("Y", 0);
        }
        if (compare.y > 0)
        {
            animator.SetInteger("X", 0);
            animator.SetInteger("Y", 1);
        }
        if (compare.x < 0)
        {
            animator.SetInteger("X", 0);
            animator.SetInteger("Y", -1);
        }

        lastPosition = transform.position;
    }
}
