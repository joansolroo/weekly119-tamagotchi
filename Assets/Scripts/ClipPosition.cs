using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = Clip(transform.parent.position);
    }

    public static Vector2 Clip(Vector2 worldPosition)
    {
        Vector2 clippedPosition = worldPosition;
        clippedPosition.x = (int)(clippedPosition.x);
        clippedPosition.y = (int)(clippedPosition.y);
        return clippedPosition;
    }
}
