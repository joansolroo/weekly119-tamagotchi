using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UICounterBar : MonoBehaviour
{
    [SerializeField] Statistic statistic;
    [SerializeField] Sprite sprite;

    [Header("Renderer")]
    [SerializeField] Transform progress;
    [SerializeField] SpriteRenderer icon;
    // Start is called before the first frame update
    void Start()
    {
        statistic.OnChange += OnChange;
        OnValidate();
    }

    private void OnValidate()
    {
        icon.sprite = sprite;
        this.name = "bar " + statistic.name;
    }
    // Update is called once per frame
    public void OnChange(int previous, int current)
    {
        progress.localScale = new Vector3(current / (float)statistic.max, 1, 1);
    }
}
