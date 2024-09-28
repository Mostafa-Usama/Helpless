using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen, isLocked;
    Animator animator;
    public AudioClip open;
    public AudioClip close;
    AudioSource door;
    void Start()
    {
        door = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (isOpen) 
        {
            animator.SetBool("isOpened", true);
        }
    }

    public void playsound()
    {
        if (isOpen)
        {
            door.PlayOneShot(close);
        }
        else 
        {
            door.PlayOneShot(open);
        }
    }
}
