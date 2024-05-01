using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerAnimatror : MonoBehaviour
{
    public Animator animator;
    public Transform Ground;
    [SerializeField] private LayerMask layerMask;
    public bool isGround;
    public float GroundDistance;
    private int isGroundeCount = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(setStateIsGorund());
    }

    IEnumerator setStateIsGorund()
    {
        if (isGroundeCount == 0)
        {
            yield return new WaitForSeconds(3);
            if (isGrounded())
            {
                isGroundeCount = 1;
                isGround = true;
            }
        }
        else
        {
            isGround = false;
        }
        animator.SetBool("isGround", isGround);
    }

    public bool isGrounded()
    {
        if (Physics.CheckSphere(Ground.position, GroundDistance, layerMask)) return true;
        return false;
    }
}
