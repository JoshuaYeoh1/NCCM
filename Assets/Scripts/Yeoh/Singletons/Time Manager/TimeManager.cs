using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Current;

    void Awake()
    {
        if(!Current) Current=this;        
    }
        
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public int hour=10;
    public string meridiem="pm";
    public float realHourInterval=45;

    void Start()
    {
        StartCoroutine(TimeRunning());
    }

    IEnumerator TimeRunning()
    {
        while(true)
        {
            yield return new WaitForSeconds(realHourInterval);

            hour++;

            if(hour>=13)
            {
                hour=1;

                meridiem = meridiem=="pm" ? "am" : "pm";
            }
        }
    }
}
