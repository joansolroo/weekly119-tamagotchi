using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TouchController touchController;
    [SerializeField] CursorController cursor;

    [SerializeField] LayerMask characterLayer;
    [Header("Status")]
    [SerializeField] Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        touchController.OnTouch += HandleTouch;
        touchController.OnHover += HandleHover;
    }

    Interactable pickedInteractable;
    Transform pickedParent;

    public void HandleTouch(TouchController.SimpleTouch touch)
    {

        position = ClipPosition.Clip(touch.position);
        cursor.transform.position = position;
        if (touch.phase == TouchController.SimpleTouch.TouchPhase.Began)
        {
            Collider2D hit = Physics2D.OverlapPoint(position);
            if (hit)
            {
                pickedInteractable = hit.transform.gameObject.GetComponent<Interactable>();
                if (pickedInteractable)
                {
                    pickedParent = pickedInteractable.transform.parent;
                    pickedInteractable.transform.parent = cursor.transform;

                    pickedInteractable.OnPick();
                }
            }
        }
        else if (touch.phase == TouchController.SimpleTouch.TouchPhase.Ended)
        {

            if (pickedInteractable)
            {
                Vector2 pos = pickedInteractable.transform.position;
                pickedInteractable.transform.parent = pickedParent;

                pickedInteractable.OnRelease();

                Collider2D hit = Physics2D.OverlapPoint(position, characterLayer);
                if(hit != null)
                {
                    Creature creature = hit.GetComponent<Creature>();
                    if(creature!=null)
                    {
                        creature.DoInteract(pickedInteractable);
                    }
                }
            }

        }
    }
    public void HandleHover(TouchController.SimpleTouch touch)
    {
        position = ClipPosition.Clip(touch.position);
        cursor.transform.position = position;
    }
}
