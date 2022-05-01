using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject[] tanks;
    public Vector3 targetCameraPos = Vector3.zero;
    public Camera mainCamera;
    public GameObject cameraParent;

    public Vector3 currentVelocity = Vector3.zero;
    public float smoothTime = 0.1f;
    public float maxSmoothSpeed = 2;
    public float sizeOffset = 4;

    //相机最大最小尺寸
    public float maxSize = 35; 
    public float minSize = 14;
    // Start is called before the first frame update

    private float single_camera_size = 20;
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        ResetCameraPos();
        ResetCameraSize();
    }

    void ResetCameraPos()
    {
        Vector3 sumPos = Vector3.zero;
        foreach (var tank in tanks)
        {
            sumPos += tank.transform.position;
        }

        if (tanks.Length > 1) //双人模式
        {
            
            targetCameraPos = sumPos / tanks.Length;
            targetCameraPos.y = cameraParent.transform.position.y;

            //对摄像机范围加以限定
            if (targetCameraPos.x < 0)
            {
                targetCameraPos.x = 0;
                if (targetCameraPos.x > 20)
                {
                    targetCameraPos.x = 20;
                }
            }
            if (targetCameraPos.y < 0)
            {
                targetCameraPos.y = 0;
                if (targetCameraPos.y > 20)
                {
                    targetCameraPos.y = 20;
                }
            }

            cameraParent.transform.position = Vector3.SmoothDamp(cameraParent.transform.position, targetCameraPos, ref currentVelocity, smoothTime, maxSmoothSpeed);//平滑移动
        }
        else if(tanks.Length > 0) //单人模式
        {
            targetCameraPos = sumPos;
            targetCameraPos.y = cameraParent.transform.position.y;

            cameraParent.transform.position = Vector3.SmoothDamp(cameraParent.transform.position, targetCameraPos, ref currentVelocity, smoothTime, maxSmoothSpeed);//平滑移动

        }
        

    }


    void ResetCameraSize()
    {
        //通过计算坦克与相机中心的距离推断相机的size
        float size = 0;
        if(tanks.Length > 1)
        {
            foreach (var tank in tanks)
            {
                Vector3 offsetPos = tank.transform.position - targetCameraPos;
                float z_Value = Mathf.Abs(offsetPos.z);
                size = Mathf.Max(size, z_Value);
                float x_Value = Mathf.Abs(offsetPos.x);
                //mainCamera.aspect : Width/Height
                size = Mathf.Max(size, x_Value / mainCamera.aspect);
            }
            size += sizeOffset;
            mainCamera.orthographicSize = Mathf.Min(Mathf.Max(size, minSize), maxSize); //限制相机最大尺寸
        }
        else if(tanks.Length > 0 )
        {
            mainCamera.orthographicSize = single_camera_size; //限制相机最大尺寸
        }
        

        
    }
}
