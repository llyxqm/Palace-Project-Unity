using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_tree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, 0);
    }
}
