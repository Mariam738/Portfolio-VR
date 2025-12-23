
using UnityEngine;

public class PlayNoteNumber : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;    // auto-get in Start
    public PianoKeyMap keyMap;         // ScriptableObject with clips
    public AudioClip keyClip;          // specific clip for this key

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Assign clip based on key name index (like original script)
        int keyIndex = int.Parse(gameObject.name);
        keyClip = keyMap.clips[keyIndex - 1];

    }

    public void PlayNote()
    {
        // Use PlayOneShot with mapped clip (same audio logic as original script)
        audioSource.PlayOneShot(keyClip);
        Debug.Log($"[AUDIO] Playing clip {keyClip.name} for {gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
