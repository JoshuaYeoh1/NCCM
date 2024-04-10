using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;
    }

    void OnClick(GameObject target)
    {
        if(target!=gameObject) return;

        if(Singleton.Current.canView)
        {
            ShowPoster();
        }
    }

    public GameObject posterCanvasPrefab;

    void ShowPoster()
    {
        EventManager.Current.OnToggleFirstPerson(false);

        Time.timeScale=0;

        Instantiate(posterCanvasPrefab, transform.position, Quaternion.identity);
    }
}
