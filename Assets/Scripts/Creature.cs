using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Interactable
{
    [Header("Links")]
    [SerializeField] SpriteAnimator animator;
    [SerializeField] Clock clock;
    [SerializeField] AudioSource audio;

    [Header("Stats")]
    [SerializeField] Statistic hp;
    [SerializeField] Statistic hunger;
    [SerializeField] Statistic energy;
    [SerializeField] Statistic happiness;
    [SerializeField] Statistic discipline;
    [SerializeField] Statistic cleanliness;

    [Header("Emoji")]
    [SerializeField] SpriteRenderer sleepEmoji;

    [Header("Status")]
    [SerializeField] long age = 0;
    [SerializeField] bool sleeping = false;
    private void Start()
    {
        Clock.main.OnTick += Tick;
        audio = GetComponent<AudioSource>();
        if(!audio)
        {
            audio = gameObject.AddComponent<AudioSource>();
        }
    }

    int direction = 1;
    int movement = 0;


    public void Tick()
    {
        if (age > 0)
        {
            if (!sleeping)
            {
                if (age % 20 == 0)
                {
                    if (hunger.current > 0)
                    {
                        hunger.Reduce(1);
                    }
                    else
                    {
                        hp.Reduce(1);
                    }
                }
                if (age % 10 == 0)
                {
                    energy.Reduce(1);
                }
                if (age % 15 == 0)
                {
                    happiness.Reduce(1);
                }


                if (energy.current == 0)
                {
                    sleeping = true;
                }
            }
            else
            {
                if (energy.current == energy.max)
                {
                    sleeping = false;
                }
                else if (age % 5 == 0)
                {
                    energy.Increase(1);
                }
            }
        }
        if (!sleeping)
        {
            int newDirection = Random.Range(-1, 2);
            if (newDirection != 0)
            {
                direction = newDirection;
            }

            movement = newDirection;

        }
        else
        {
            movement = 0;
        }

        UpdateVisuals();

        ++age;

    }

    public bool DoFeed(int amount)
    {
        return hunger.Increase(amount);
    }
    public bool DoPlay()
    {
        return happiness.Increase(1);
    }
    public void DoInteract(Interactable interactable)
    {
        if (!sleeping)
        {
            if (interactable.data)
            {
                if (interactable.data.type == InteractableData.InteractableType.food)
                {
                    if (hunger.Increase(interactable.data.amount))
                    {
                        interactable.OnConsumed();
                        audio.PlayOneShot(interactable.data.consumeClip);
                    }
                }
                if (interactable.data.type == InteractableData.InteractableType.energy)
                {
                    if (energy.Increase(interactable.data.amount))
                    {
                        interactable.OnConsumed();
                        audio.PlayOneShot(interactable.data.consumeClip);
                    }
                }
                if (interactable.data.type == InteractableData.InteractableType.toy)
                {
                    if (happiness.Increase(interactable.data.amount))
                    {
                        audio.PlayOneShot(interactable.data.consumeClip);
                    }
                }
            }
        }
    }

    void UpdateVisuals()
    {
        sleepEmoji.gameObject.SetActive(sleeping);

        if (movement != 0)
        {
            this.transform.position += new Vector3(direction, 0);
            animator.transform.eulerAngles = new Vector3(0, direction < 0 ? 180 : 0, 0);
            animator.PlayAnimationIndexed("walk");
        }
        else
        {
            if (sleeping)
            {
                animator.PlayAnimationIndexed("sleep");
            }
            else
            {
                animator.PlayAnimationIndexed("idle");
            }
        }
    }

    public override void OnPick()
    {
        base.OnPick();
        this.sleeping = false;
    }
}
