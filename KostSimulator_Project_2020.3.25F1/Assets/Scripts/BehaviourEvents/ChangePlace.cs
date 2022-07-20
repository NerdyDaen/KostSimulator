using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlace : MonoBehaviour
{
    public Transform Player;
    public Transform Camera;

    [Header("Player New Position")]
    public float x;
    public float y;
    public float z;

    [Header("Player New Rotate")]
    public float Rotate_x;
    public float Rotate_y;
    public float Rotate_z;

    [Header("Player New Position")]
    public float Cam_x;
    public float Cam_y;
    public float Cam_z;

    [Header("Player New Rotate")]
    public float Cam_Rotate_x;
    public float Cam_Rotate_y;
    public float Cam_Rotate_z;

    public void ChangePosition()
    {
        Player.position = new Vector3(x, y, z);
    }

    public void ChangeRotation()
    {
        Player.rotation = Quaternion.Euler(Rotate_x, Rotate_y, Rotate_z);
    }

    public void CameraPosition()
    {
        Camera.position = new Vector3(Cam_x, Cam_y, Cam_z);
    }

    public void CameraRotation()
    {
        Camera.rotation = Quaternion.Euler(Cam_Rotate_x, Cam_Rotate_y, Cam_Rotate_z);
    }
}
