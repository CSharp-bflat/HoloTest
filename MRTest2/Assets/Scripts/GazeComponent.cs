using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GazeComponent : MonoBehaviour, IFocusable, IInputClickHandler
{

    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;

    private GazeComponentManager manager;


    private void EditManager(bool isActive)
    {
        if (manager == null)
        {
            manager = GameObject.FindObjectOfType<GazeComponentManager>();
            if (manager == null)
            {
                return;
            }
        }
        manager.SetComponentActive(this, isActive);
        print("!!");
    }
    private void OnEnable()
    {
        EditManager(true);
    }
    private void OnDisable()
    {
        EditManager(false);
    }
    private void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    void IFocusable.OnFocusEnter()
    {
        print("Gazing at"+gameObject.name);
        manager.EnableGaze(this);
    }

    void IFocusable.OnFocusExit()
    {
        manager.DisableGaze(this);
    }

    void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
    {
        // Play the audioSource feedback when we gaze and select a hologram.
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        
    }

    private void OnDestroy()
    {
    }
}
