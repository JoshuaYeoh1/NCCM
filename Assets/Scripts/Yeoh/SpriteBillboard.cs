using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Transform faceCamera;
    public bool onlyY, fixedUpdate=true;

    void Update()
    {
        if(!faceCamera) faceCamera=Camera.main.transform;

        if(!fixedUpdate) Billboard();
    }
    
    void FixedUpdate()
    {
        if(fixedUpdate) Billboard();
    }

    void Billboard()
    {
        if(onlyY) transform.rotation = Quaternion.Euler(0, faceCamera.rotation.eulerAngles.y, 0);
        else transform.rotation = faceCamera.rotation;
    }
}
