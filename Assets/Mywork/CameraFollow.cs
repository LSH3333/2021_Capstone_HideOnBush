using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; // 카메라가 쫒을 대상
    [SerializeField]
    private float smoothing = 5f; // 쫒아가는 속도

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // 타겟이 움직였고, 기억해 둔 거리값을 이용해 카메라의 새로운 위치값 계산
        Vector3 newPos = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position, newPos, smoothing * Time.deltaTime);

    }
    
}
