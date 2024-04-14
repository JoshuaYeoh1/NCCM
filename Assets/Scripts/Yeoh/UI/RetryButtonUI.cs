using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButtonUI : MonoBehaviour
{
    public void Retry()
    {
        ScenesManager.Current.TransitionTo(Scenes.Yeoh1);
    }
}
