using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class InteractableData : ScriptableObject
{
    public enum InteractableType
    {
        food, heal, energy, toy
    }
    public new string name;
    public InteractableType type;
    public int amount = 1;

    [Header("Sound")]
    public AudioClip pickClip;
    public AudioClip dropClip;
    public AudioClip consumeClip;


#if UNITY_EDITOR
    [MenuItem("Assets/Create/Inventory/" + "Interactable", priority = 1)]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<InteractableData>();
    }
#endif
}
