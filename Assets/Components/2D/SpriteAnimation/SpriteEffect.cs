using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffect : MonoBehaviour
{
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] [Range(1,255)]int posterize = 1;
    public delegate void SpriteEffectEvent();
    public SpriteEffectEvent OnEffectStart;
    public SpriteEffectEvent OnEffectDone;
    bool animating = false;

    private void Start()
    {
        if (renderer == null) renderer = GetComponent<SpriteRenderer>();
    }
    public void Fade(float time)
    {
        if (!animating)
        {
            animating = true;
            StartCoroutine(DoFade(time));
        }
            
    }
    public void Blink(float time)
    {
        if (!animating)
        {
            animating = true;
            StartCoroutine(DoBlink(time));
        }
       
    }
    public void Flash(float time, Color color)
    {
      if (!animating)
        {
            StartCoroutine(DoFlash(time, color));
        }
      
    }
    public bool IsAnimating()
    {
        return animating;
    }
    IEnumerator DoFade(float time)
    {
        //if (!animating)
        {
            OnEffectStart?.Invoke();

            animating = true;
            Color initialColor = renderer.color;
            Color targetColor = new Color(0, 0, 0, 0);
            float t = 0;
            while (t < time)
            {
                renderer.color = Interpolate(initialColor, targetColor, t / time);
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;
            }
            renderer.color = targetColor;

            animating = false;
            OnEffectDone?.Invoke();
        }
    }
    IEnumerator DoBlink(float time)
    {
        //if (!animating)
        {
            OnEffectStart?.Invoke();

            animating = true;
            Color initialColor = renderer.color;
            Color currentColor = initialColor;
            float t = 0;
            while (animating && t < time)
            {
                currentColor.a = ((int)((Mathf.Cos(t * 360 * 5f) * 0.5f + 0.5f) * 10)) / 10f;
                renderer.color = currentColor;
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;
            }
            renderer.color = initialColor;
            animating = false;

            OnEffectDone?.Invoke();
        }
    }
    IEnumerator DoFlash(float time, Color color)
    {
        if (!animating)
        {
            OnEffectStart?.Invoke();
           
            animating = true;
            Color initialColor = renderer.color;
            Color currentColor = initialColor;
            Color targetColor = color;
            float t = 0;
            while (animating && t < time)
            {
                float nTime = t / time;
                if (nTime < 0.5f)
                    currentColor = Interpolate(initialColor, targetColor, nTime * 2);
                else
                    currentColor = Interpolate(targetColor, initialColor,nTime * 2-1);
                renderer.color = currentColor;
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;
            }
            renderer.color = initialColor;
            animating = false;

            OnEffectDone?.Invoke();
        }
    }

    Color Interpolate(Color a, Color b, float t)
    {
        float tPos =  (Mathf.Round(t * posterize)) / posterize;
        return Color.Lerp(a, b, tPos);
    }
}
