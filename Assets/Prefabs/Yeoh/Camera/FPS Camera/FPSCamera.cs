using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FPSCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject fpsCanvas;

    CinemachineVirtualCamera cineCam;
    CinemachinePOV cinePOV;

    void Awake()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
        cinePOV = cineCam.GetCinemachineComponent<CinemachinePOV>();
    }

    void OnEnable()
    {
        EventManager.Current.ToggleFirstPersonEvent += OnToggleFirstPerson;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleFirstPersonEvent -= OnToggleFirstPerson;
    }

    public bool firstPersonEnabled=true;

    void Start()
    {
        OnToggleFirstPerson(firstPersonEnabled);
    }

    void OnToggleFirstPerson(bool toggle)
    {
        firstPersonEnabled = toggle;

        fpsCanvas.SetActive(toggle); // crosshair

        cinePOV.m_HorizontalAxis.m_InputAxisName = toggle ? "Mouse X" : "";
        cinePOV.m_VerticalAxis.m_InputAxisName = toggle ? "Mouse Y" : "";

        // kill momentum
        cinePOV.m_HorizontalAxis.m_InputAxisValue = 0;
        cinePOV.m_VerticalAxis.m_InputAxisValue = 0;

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
        float horizontalAxisValue = cinePOV.m_HorizontalAxis.Value;

        player.transform.rotation = Quaternion.Euler(0f, horizontalAxisValue, 0f);
    }
}
