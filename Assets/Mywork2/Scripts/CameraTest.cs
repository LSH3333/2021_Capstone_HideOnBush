using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    int zoom = 10;
    int normal = 30;
    float smooth = 5f;

    private bool isZoomed = false;


    //
    private Transform target;
    private Camera cam;
    public float zoomSmoothing = 1f;
    float baseSize = 1f;


    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        ZoomOut();
    }

    void ZoomOut()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }

    void ZoomCamera()
    {
        // Player Scale 1 늘어날때마다 Camera Field of view 5씩 늘어남 
        // Player가 커진 크기 
        float offset = target.localScale.x - 1f;
        float zoom = cam.fieldOfView + 5;

        // Player가 커진크기가 1씩 증가할때마다 Camera of view는 5씩 증가하도록함
        if (offset > baseSize)
        {
            Debug.Log("IN");
            Debug.Log(zoom);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * zoomSmoothing);

            baseSize += 1f;
        }

    }
}
