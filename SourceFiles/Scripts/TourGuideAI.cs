using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TourGuideAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    public int waypointIndex = 0;
    Vector3 target;

    // audio 
    public AudioClip[] audioClips;
    AudioSource audioSource;
    bool isPlayingAudio = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    void Update()
    {
        // Check if agent has arrived and not already playing audio
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !isPlayingAudio && waypointIndex < waypoints.Length)
        {
            Quaternion targetRotation = waypoints[waypointIndex].rotation;
            float angleDiff = Quaternion.Angle(agent.transform.rotation, targetRotation);
            
            if (angleDiff > 1f) // still needs to rotate
                agent.transform.rotation = Quaternion.Slerp(
                    agent.transform.rotation,
                    targetRotation,
                    Time.deltaTime * 5f // rotation speed factor
                );

            // Play audio and wait before moving on
            else if (waypointIndex < audioClips.Length && audioClips[waypointIndex] != null)
                StartCoroutine(PlayAudioAndMove(audioClips[waypointIndex]));
            
        }
    }

    IEnumerator PlayAudioAndMove(AudioClip clip)
    {
        isPlayingAudio = true;

        audioSource.clip = clip;
        audioSource.Play();

        // Wait until audio finishes
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Advance to next waypoint
        waypointIndex++;
        if (waypointIndex < waypoints.Length)
        {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
        else
        {
            Debug.Log("Stopped");
            agent.isStopped = true; // stop after last waypoint
        }

        isPlayingAudio = false;
    }
}
