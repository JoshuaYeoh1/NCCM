using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterCanvas : MonoBehaviour
{
    public void ClosePoster()
    {
        LevelManager.Current.ViewCooldown();
        
        EventManager.Current.OnToggleFirstPerson(true);

        Time.timeScale=1;
        Destroy(gameObject);
    }
}
