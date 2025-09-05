using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareDeath : MonoBehaviour
{
    public void JumpscareDie()
    {
        ScenesManager.Current.TransitionTo(Scenes.LoseScene);
    }
}
