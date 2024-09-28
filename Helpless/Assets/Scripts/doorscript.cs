using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen, isLocked;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isOpen) 
        {
            animator.SetBool("isOpened", true);
        }
    }

 
}
