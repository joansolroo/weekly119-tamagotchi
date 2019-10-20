using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimation", menuName = "2D/SpriteAnimation", order = 1)]
public class SpriteAnimation : ScriptableObject
{
    [SerializeField] new public string name;
    [SerializeField] public Sprite[] frames;
    [SerializeField] public float animationDuration;
    [SerializeField] public bool loop = true;
    [SerializeField] public float minDuration = 0;

    [SerializeField] public SoundClip[] audio;
}
