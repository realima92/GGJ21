using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Rigidbody animatedBody;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Velocity", animatedBody.velocity.sqrMagnitude);
        //Debug.Log(animatedBody.velocity.sqrMagnitude);
    }
}
