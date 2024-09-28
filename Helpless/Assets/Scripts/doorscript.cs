using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open = false , locked = false;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (open) 
        {
            animator.SetTrigger("open");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
