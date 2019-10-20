using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour

{
    Vector3 position;
    int t = 0;

    private void Start()
    {
        Clock.main.OnMayorTick += Tick;
        position = transform.position;
    }
    private void OnDisable()
    {
        Clock.main.OnMayorTick -= Tick;
    }

   
   
    void Tick()
    {
        if(t ==0 || t ==2)
         transform.position = position + new Vector3(0,0,0);
        else if (t ==1)
            transform.position = position + new Vector3(0, 1, 0);
        else if (t == 3)
            transform.position = position + new Vector3(0, -1, 0);

        t = (t+1)%4;
    }
}
