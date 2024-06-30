using System;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;
public class AudioManager : MonoBehaviour
{
   public CCAudioManager[] customClassForAudio;

void Awake()
{
    foreach (CCAudioManager audioManager in customClassForAudio)
    {
        audioManager.audioSource = gameObject.AddComponent<AudioSource>();
        audioManager.audioSource.clip = audioManager.audioClip;
        audioManager.audioSource.volume = audioManager.Volume;
        audioManager.audioSource.loop = audioManager.loop;
        audioManager.audioSource.priority = audioManager.priorityOverSounds;
        audioManager.audioSource.pitch = audioManager.pitchOrSpeed;
        audioManager.audioSource.playOnAwake = audioManager.playOnAwake;
        audioManager.audioSource.spatialBlend = audioManager.spatialBlend;
    }
}

public void PlaySound(string soundName)
{
    CCAudioManager s = Array.Find(customClassForAudio, s=>s.audioName == soundName);
    s?.audioSource.Play();
    if (s == null)
    {
        Debug.Log("Sound "+ soundName + "not found");
    }
}

}
