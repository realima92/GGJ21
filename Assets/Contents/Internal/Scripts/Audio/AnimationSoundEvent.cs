using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundEvent : MonoBehaviour
{
    public Transform audioParent;
    public List<StudioEventEmitter> emmiters;

    public void AudioStepEvent(int index)
    {
        emmiters[index].Play();
    }
}
