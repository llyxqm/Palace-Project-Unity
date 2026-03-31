using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut_tree : MonoBehaviour 
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            animator.SetBool("cut_tree", true);

        }
        else
        {
            animator.SetBool("cut_tree", false);
        }
    }
}
