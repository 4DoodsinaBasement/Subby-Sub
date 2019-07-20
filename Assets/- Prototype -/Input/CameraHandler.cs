using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CameraHandler : MonoBehaviour
{
    // 1 Local Player
    CameraInfo camInfo_1of1 = new CameraInfo(0.0f, 0.0f, 1.0f, 1.0f);
    // 2 Local Players
    CameraInfo camInfo_1of2 = new CameraInfo(0.0f, 0.5f, 1.0f, 0.5f);
    CameraInfo camInfo_2of2 = new CameraInfo(0.0f, 0.0f, 1.0f, 0.5f);
    // 3 Local Players
    CameraInfo camInfo_1of3 = new CameraInfo(0.0f, 0.5f, 1.0f, 0.5f);
    CameraInfo camInfo_2of3 = new CameraInfo(0.0f, 0.0f, 0.5f, 0.5f);
    CameraInfo camInfo_3of3 = new CameraInfo(0.5f, 0.0f, 0.5f, 0.5f);
    // 4 Local Players
    CameraInfo camInfo_1of4 = new CameraInfo(0.0f, 0.5f, 0.5f, 0.5f);
    CameraInfo camInfo_2of4 = new CameraInfo(0.5f, 0.5f, 0.5f, 0.5f);
    CameraInfo camInfo_3of4 = new CameraInfo(0.0f, 0.0f, 0.5f, 0.5f);
    CameraInfo camInfo_4of4 = new CameraInfo(0.5f, 0.0f, 0.5f, 0.5f);
    
    private int _localPlayers; public int localPlayers
    { 
        get { return _localPlayers; }
        set { _localPlayers = value; SetCameraInfos(); }
    }
    
    public Camera cam1, cam2, cam3, cam4;

    
    void Start() { localPlayers = 3; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) { localPlayers = 0; }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) { localPlayers = 1; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { localPlayers = 2; }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { localPlayers = 3; }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { localPlayers = 4; }
    }


    void SetCameraInfos()
    {
        switch (localPlayers)
        {
            case 0:
                cam1.enabled = false;
                cam2.enabled = false;
                cam3.enabled = false;
                cam4.enabled = false;
                break;
            case 1:
                EditCameraInfo(cam1, camInfo_1of1);
                cam2.enabled = false;
                cam3.enabled = false;
                cam4.enabled = false;
                break;
            case 2:
                EditCameraInfo(cam1, camInfo_1of2);
                EditCameraInfo(cam2, camInfo_2of2);
                cam3.enabled = false;
                cam4.enabled = false;
                break;
            case 3:
                EditCameraInfo(cam1, camInfo_1of3);
                EditCameraInfo(cam2, camInfo_2of3);
                EditCameraInfo(cam3, camInfo_3of3);
                cam4.enabled = false;
                break;
            case 4:
                EditCameraInfo(cam1, camInfo_1of4);
                EditCameraInfo(cam2, camInfo_2of4);
                EditCameraInfo(cam3, camInfo_3of4);
                EditCameraInfo(cam4, camInfo_4of4);
                break;
        }
    }

    void EditCameraInfo(Camera camera, CameraInfo info)
    {
        camera.enabled = true;
        camera.rect = new Rect(info.x, info.y, info.w, info.h);
    }
}


public struct CameraInfo
{
    public float x,y,w,h;
    
    public CameraInfo(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
}
