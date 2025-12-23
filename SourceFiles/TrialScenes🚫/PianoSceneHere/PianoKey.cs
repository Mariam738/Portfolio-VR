using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PianoKey : MonoBehaviour
{
    [Header("Key Movement Settings")]
    public float pressDepth = 0.05f;   // how far the key moves down
    public float pressSpeed = 10f;     // how fast the key moves

    private Vector3 startPos;          // original position
    private bool isPressed = false;    // pressed state

    [Header("Audio Settings")]
    public AudioSource audioSource;    // auto-get in Start
    public PianoKeyMap keyMap;         // ScriptableObject with clips
    public AudioClip keyClip;          // specific clip for this key

    [Header("Events")]
    public UnityEvent onPressed, onReleased;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startPos = transform.localPosition;

        // Assign clip based on key name index (like original script)
        int keyIndex = int.Parse(gameObject.name);
        keyClip = keyMap.clips[keyIndex - 1];

        Debug.Log($"[INIT] PianoKey {gameObject.name} initialized at {startPos} with clip {keyClip.name}");
    }

    void Update()
    {
        // Smoothly move key down if pressed, else return to start
        Vector3 targetPos = isPressed ? startPos + Vector3.down * pressDepth : startPos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * pressSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.name.Contains("Hand"))
        {
            if (!isPressed) // only trigger once per press
            {
                isPressed = true;
                PlayNote();
                onPressed.Invoke();
                Debug.Log($"[PRESSED] {gameObject.name} pressed by {collision.gameObject.name}");
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.name.Contains("Hand"))
        {
            isPressed = false;
            onReleased.Invoke();
            Debug.Log($"[RELEASED] {gameObject.name} released by {collision.gameObject.name}");
        }
    }

    void PlayNote()
    {
        // Use PlayOneShot with mapped clip (same audio logic as original script)
        audioSource.PlayOneShot(keyClip);
        Debug.Log($"[AUDIO] Playing clip {keyClip.name} for {gameObject.name}");
    }
}


// using UnityEngine;
// using UnityEngine.Events;

// public class PianoKey : MonoBehaviour
// {
//     public HingeJoint hinge; // Assign your piano key hinge in Inspector
//     public float pressedThreshold = -4f; // Angle at which we consider it "pressed"

//     private bool _isPressed = false;

//     public AudioSource audioSource; // Assign AudioSource in Inspector
//     public PianoKeyMap keyMap; // Assign PianoKeyMap ScriptableObject in Inspector
//     public AudioClip keyClip; // Assign specific AudioClip for this key in Inspector

//     public UnityEvent onPressed, onReleased;

//     void Start()
//     {

//         hinge = GetComponent<HingeJoint>();
//         audioSource = GetComponent<AudioSource>();
//         int keyIndex = int.Parse(gameObject.name);
//         keyClip = keyMap.clips[keyIndex - 1];
//         // audioSource.clip = keyClip;
//     }

//     void Update()
//     {
//         float angle = hinge.angle;

//         // Check if the hinge has rotated past the threshold
//         if (!_isPressed && angle <= pressedThreshold)
//             Pressed(angle);

//         // Reset when it goes back up
//         if (_isPressed && angle > pressedThreshold)
//             Released(angle);
//     }


//     private void Pressed(float angle)
//     {
//         _isPressed = true;
//         audioSource.PlayOneShot(keyClip);
//         onPressed.Invoke();
//         Debug.Log("👆 Key Pressed! Angle: " + angle);
//         Debug.Log("Key Pressed: " + gameObject.name);

//     }
    
//     private void Released(float angle)
//     {
//         _isPressed = false;
//         onReleased.Invoke();
//         Debug.Log("🚩Key Released! Angle: " + angle);
//     }
// }
