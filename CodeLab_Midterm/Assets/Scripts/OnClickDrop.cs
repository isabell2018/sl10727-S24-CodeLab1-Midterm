using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDrop : MonoBehaviour
{
    private void OnMouseDown()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
