using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundEvent : MonoBehaviour
{
    public Transform audioParent;
    public List<StudioEventEmitter> emmiters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioStepEvent(int index)
    {
        Debug.Log($"Opa! Animou legal. Bora tocar passos na posição {index}");
        emmiters[index].Play();
    }
}
