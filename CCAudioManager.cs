using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class CCAudioManager
{
    public string audioName;
   [Range(0f,1f)]
   public float Volume = 1.5f;
   public bool loop;
   [Range(0f,256f)]
   public int priorityOverSounds = 128;
   [Range(-3f,3f)]
   public float pitchOrSpeed = 1f;
   [Range(0f,1f)]
   public float spatialBlend = 3f;
   public bool playOnAwake;
   public AudioClip audioClip;
   [HideInInspector]
   public AudioSource audioSource;
}
