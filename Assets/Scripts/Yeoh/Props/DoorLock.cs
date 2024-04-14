using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;
    }

    void Start()
    {
        ToggleLockIcon(LevelManager.Current.doorLocked);

        if(LevelManager.Current.doorLocked)
        {
            LockDoor(doorLockTime);
        }
        else UnlockDoor();
    }
    
    public float doorLockTime=5;

    void OnClick(GameObject target)
    {
        if(target!=gameObject) return;
        
        if(LevelManager.Current.doorLocked)
        {
            UnlockDoor();
        }
        else LockDoor(doorLockTime);
    }

    public void LockDoor(float time)
    {
        LevelManager.Current.doorLocked=true;

        ToggleLockIcon(true);

        CancelInvoke(nameof(UnlockDoor));

        Invoke(nameof(UnlockDoor), time);
    }
    void UnlockDoor()
    {
        CancelInvoke(nameof(UnlockDoor));

        LevelManager.Current.doorLocked=false;

        ToggleLockIcon(false);
    }

    public GameObject unlockIcon;
    public GameObject lockIcon;

    void ToggleLockIcon(bool toggle)
    {
        unlockIcon.SetActive(!toggle);
        lockIcon.SetActive(toggle);

        AudioManager.Current.PlaySFX(SFXManager.Current.sfxDoor, transform.position);
    }
}
