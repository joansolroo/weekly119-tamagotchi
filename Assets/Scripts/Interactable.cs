using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public InteractableData data;

    [SerializeField] public Rigidbody2D rb2D;
    

    public virtual void OnPick()
    {
        if(rb2D)
        {
            rb2D.simulated = false;
        }
    }
    public virtual void OnRelease(Vector2 velocity)
    {

        if (rb2D)
        {
            rb2D.simulated = true;
            rb2D.velocity = velocity;
            rb2D.angularVelocity =0;
        }
    }

    public void OnConsumed()
    {
        GameObject.Destroy(this.gameObject);
    }
}
