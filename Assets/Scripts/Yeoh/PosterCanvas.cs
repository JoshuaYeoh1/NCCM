using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterCanvas : MonoBehaviour
{
    public void ClosePoster()
    {
        Singleton.Current.ViewCooldown();
        
        EventManager.Current.OnToggleFirstPerson(true);

        Time.timeScale=1;
        Destroy(gameObject);
    }
}
