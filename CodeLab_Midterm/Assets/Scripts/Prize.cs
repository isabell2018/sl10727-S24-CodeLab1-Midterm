using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit!");
            GameManager.instance.Score++;
            transform.position = new Vector3 (-50, -50, 0);
        }
    }
    
}
