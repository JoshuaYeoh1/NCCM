using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBarManager))]

public class ShutterHPBar : MonoBehaviour
{
    UIBarManager bar;

    void Awake()
    {
        bar=GetComponent<UIBarManager>();
    }

    void Start()
    {
        bar.owner = LevelManager.Current.gameObject;
    }
}
