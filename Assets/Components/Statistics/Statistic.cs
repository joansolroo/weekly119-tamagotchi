using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    #region Fields
    [SerializeField] public int max;
    [SerializeField] public int current;
    #endregion
    #region Events
    public delegate void StatEvent(int previous, int current);
    public StatEvent OnChange;
    public StatEvent OnDamage;
    public StatEvent OnIncrease;
    public StatEvent OnZero;
    #endregion

    public virtual bool Change(int delta)
    {
        if (delta > 0)
        {
            return Increase(delta);
        }
        else if (delta < 0)
        {
            return Reduce(delta);
        }
        else return false;
    }
    public virtual bool Reduce(int amount)
    {
        if (current > 0)
        {
            int previous = current;
            current -= amount;
            OnChange?.Invoke(previous, current);
            OnDamage?.Invoke(previous, current);
            if (current <= 0)
            {
                current = 0;
                OnZero?.Invoke(previous, current);
            }
            return true;
        }
        return false;
    }
    public virtual bool Increase(int amount)
    {
        if (current < max)
        {
            int previous = current;
            current += amount;
            OnChange?.Invoke(previous, current);
            OnIncrease?.Invoke(previous, current);
            if (current > max)
            {
                current = max;
            }
            return true;
        }
        return false;
    }
}
