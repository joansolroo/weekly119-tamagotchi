using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Persistence : MonoBehaviour
{
    string keyLastExcecution = "lastTime";
    // Start is called before the first frame update
    void Start()
    {
        int h = PlayerPrefs.GetInt("hello");
        Debug.Log("Excecutions = " + h);
        PlayerPrefs.SetInt("hello",++h);
        DateTime now = DateTime.Now;


        string tsString = PlayerPrefs.GetString(keyLastExcecution);
        if (tsString != null && tsString.Length>0)
        {
            long timestamp = long.Parse(tsString);
            DateTime ts = DateTime.FromFileTime(timestamp);
            Debug.Log("Last excecution:" + ts);
        }
        PlayerPrefs.SetString(keyLastExcecution, DateTime.Now.ToFileTime().ToString());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
