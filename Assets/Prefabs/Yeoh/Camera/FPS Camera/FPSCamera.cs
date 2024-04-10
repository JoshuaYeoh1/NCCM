using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FPSCamera : MonoBehaviour
{
    public GameObject player;

    public bool firstPersonEnabled=true;

    CinemachineVirtualCamera cineCam;

    void Awake()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        ToggleFirstPerson(firstPersonEnabled);
    }

    void ToggleFirstPerson(bool toggle)
    {
        firstPersonEnabled = toggle;

        MouseManager.Current.LockMouse(toggle);
    }

    void Update()
    {
        if(firstPersonEnabled)
        {
            RotatePlayerWithCamera();
        }
    }

    void RotatePlayerWithCamera()
    {
        CinemachinePOV cinePOV = cineCam.GetCinemachineComponent<CinemachinePOV>();

        float horizontalAxisValue = cinePOV.m_HorizontalAxis.Value;

        player.transform.rotation = Quaternion.Euler(0f, horizontalAxisValue, 0f);
    }
}
