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
                pickedInteractable.transform.parent = pickedParent;
                Debug.Log(touch.deltaPosition);
                pickedInteractable.OnRelease(touch.deltaPosition / Time.deltaTime * 0.25f);

                Collider2D[] hits = Physics2D.OverlapPointAll(position, characterLayer);
                foreach (Collider2D hit in hits)
                {
                    if(hit.gameObject == pickedInteractable.gameObject)
                    {
                        continue;
                    }
                    Interactable interactable = hit.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        if (interactable.OnInteract(pickedInteractable))
                        {
                            break;
                        }
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
