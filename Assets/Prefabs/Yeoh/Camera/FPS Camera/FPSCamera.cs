using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FPSCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject fpsCanvas;

    CinemachineVirtualCamera cineCam;

    void Awake()
    {
        cineCam = GetComponent<CinemachineVirtualCamera>();
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
        cineCam.enabled = toggle;
        
        firstPersonEnabled = toggle;

        fpsCanvas.SetActive(toggle);

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
