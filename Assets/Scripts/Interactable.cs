using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] public InteractableData data;

    [SerializeField] Rigidbody2D rb2D;
    

    public virtual void OnPick()
    {
        if(rb2D)
        {
            rb2D.simulated = false;
        }
    }
    public virtual void OnRelease()
    {

        if (rb2D)
        {
            rb2D.simulated = true;
            rb2D.velocity = Vector2.zero;
            rb2D.angularVelocity =0;
        }
    }

    public void OnConsumed()
    {
        GameObject.Destroy(this.gameObject);
    }
}
