using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashbin : Interactable
{
    [SerializeField] new AudioSource audio;
    [SerializeField] AudioClip destroyclip;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        if (!audio)
        {
            audio = gameObject.AddComponent<AudioSource>();
        }
    }
    public override bool OnInteract(Interactable other)
    {
        other.OnConsumed();
        audio.PlayOneShot(destroyclip);
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Interactable other = collision.gameObject.GetComponent<Interactable>();
        if(other!=null)
        {
            OnInteract(other);
        }
    }
}
