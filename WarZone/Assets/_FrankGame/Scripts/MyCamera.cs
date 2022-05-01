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

    //��������С�ߴ�
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

        if (tanks.Length > 1) //˫��ģʽ
        {
            
            targetCameraPos = sumPos / tanks.Length;
            targetCameraPos.y = cameraParent.transform.position.y;

            //���������Χ�����޶�
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

            cameraParent.transform.position = Vector3.SmoothDamp(cameraParent.transform.position, targetCameraPos, ref currentVelocity, smoothTime, maxSmoothSpeed);//ƽ���ƶ�
        }
        else if(tanks.Length > 0) //����ģʽ
        {
            targetCameraPos = sumPos;
            targetCameraPos.y = cameraParent.transform.position.y;

            cameraParent.transform.position = Vector3.SmoothDamp(cameraParent.transform.position, targetCameraPos, ref currentVelocity, smoothTime, maxSmoothSpeed);//ƽ���ƶ�

        }
        

    }


    void ResetCameraSize()
    {
        //ͨ������̹����������ĵľ����ƶ������size
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
            mainCamera.orthographicSize = Mathf.Min(Mathf.Max(size, minSize), maxSize); //����������ߴ�
        }
        else if(tanks.Length > 0 )
        {
            mainCamera.orthographicSize = single_camera_size; //����������ߴ�
        }
        

        
    }
}
