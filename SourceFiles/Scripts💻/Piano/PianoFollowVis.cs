using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PianoFollowVis : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5f;
    public float followAngleThreshold = 45f;

    private bool freeze = false;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    private bool isFollowing = false;

    // Audio components
    private AudioSource audioSource;
    private AudioClip keyClip;
    public PianoKeyMap keyMap;

    void Start()
    {
        initialLocalPos = visualTarget.localPosition;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);

        audioSource = GetComponent<AudioSource>();
        int keyIndex = int.Parse(gameObject.name.Replace("Interactable", ""));
        keyClip = keyMap.clips[keyIndex - 1];

    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor interactor = (UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)hover.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            #if UNITY_EDITOR
                isFollowing = true;
                freeze = false;
            #else
            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
            // float pokeAngle = Vector3.Angle(interactor.transform.forward, visualTarget.TransformDirection(localAxis));

                if (pokeAngle < followAngleThreshold)
                {
                    isFollowing = true;
                    freeze = false;
                }
            #endif
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRPokeInteractor)
        {
            freeze = true;
            audioSource.PlayOneShot(keyClip);
        }
    }
    
    void Update()
    {
        if (freeze) 
            return;

        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);


            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
