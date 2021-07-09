using UnityEngine;
using System.Collections;

public class L_CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 쫒을 대상 즉 Player 
    public float smoothing = 5f; // 쫒아가는 속도
    private Camera cam;

    private Vector3 offset;

    // camera zoom    
    private float goalFieldOfView = 45f;
    // 카메라가 이 값만큼 줌 땡겨짐 
    public float cameraZoomOutAmount = 5f;
    // 카메라 줌이 땡겨질 최초 player 크기 
    private float baseSize = 2f;
    // 플레이어가 이 값 만큼 커지면 카메라 줌 땡겨짐 
    public float baseSizeAmout = 1f;
    private float velo = 0f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        // 현재 카메라와 타겟과의 물리적 거리
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        // 타겟이 움직였고, 기억해 둔 거리값을 이용해 카메라 새로운 위치값 계산
        Vector3 newPos = target.position + offset;
        // 카메라 위치 변경. Lerp 이용 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, newPos, smoothing * Time.deltaTime);

        
    }

    private void Update()
    {
        SmoothDampCameraZoom();
        
    }

    // SmoothDamp를 이용해 Player 크기가 일정 이상 커지면 camera의 zoom을 땡긴다 
    void SmoothDampCameraZoom()
    {
        float playerSize = target.transform.localScale.x;
        // player의 크기가 baseSize보다 커지면 
        if(playerSize >= baseSize)
        {
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, goalFieldOfView, ref velo, 0.3f);
        }
        // cam.fieldofView가 목표 fieldofView에 가까워지면 
        if(cam.fieldOfView >= goalFieldOfView - 0.005f)
        {
            // filedOfView를 목표치로 수정 (SmoothDamp로 목표치에 가까워지지만 정확히 도달하지는 않기때문)
            cam.fieldOfView = goalFieldOfView;
            // player 기준 크기 증가시킴 
            baseSize += baseSizeAmout;
            // 줌을 더 땡길수 있도록 goalFieldOfView 증가 
            goalFieldOfView += cameraZoomOutAmount;
        }
    }

    /*
    void ZoomOutCam()
    {
        Debug.Log(Time.deltaTime * 5f);
        float playerSize = target.transform.localScale.x;
        // Player의 크기가 기준이되는 SIze를 넘어서면 
        if (playerSize >= baseSize)
        {
            zoomTrigger = true; // zoomOut 실행 
        }
        // 목표 field of view 도달하면 zoomOutCam 실행중지 
        if (cam.fieldOfView >= goalFieldOfView - 0.05f)
        {
            zoomTrigger = false;
            goalFieldOfView += 5f;
            baseSize += 1f;
            Debug.Log(goalFieldOfView);
            Debug.Log(baseSize);
        }

        if (zoomTrigger)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, goalFieldOfView, Time.deltaTime * zoomSmoothing);
        } 
    }
    */

}
