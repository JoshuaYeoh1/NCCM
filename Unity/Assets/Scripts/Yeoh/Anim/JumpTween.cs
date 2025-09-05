using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTween : MonoBehaviour
{
    Vector3 defPos;

    void Awake()
    {
        defPos = transform.localPosition;
    }

    bool isJumping;

    public void Jump(float yOffset, float jumpTime=.4f)
    {
        if(isJumping) return;

        isJumping=true;

        LeanTween.cancel(gameObject);

        LeanTween.moveLocalY(gameObject, defPos.y+yOffset, jumpTime*.5f).setEaseOutQuad();

        LeanTween.moveLocal(gameObject, defPos, jumpTime*.5f).setEaseInQuad().setDelay(jumpTime*.5f).setOnComplete(JumpDone);
    }

    void JumpDone()
    {
        isJumping=false;
    }
}
