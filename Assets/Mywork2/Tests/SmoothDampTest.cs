using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampTest : MonoBehaviour
{
    public Transform target;
    Vector3 velo = Vector3.zero;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velo, 5f);
        Debug.Log(velo);
    }
}
