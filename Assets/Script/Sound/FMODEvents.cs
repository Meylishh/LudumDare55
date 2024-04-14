using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")] 
    [field: SerializeField] public EventReference defaultMusic { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference skypeMessageSound { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than FMOD Events found");
        }
        instance = this;
    }
}
