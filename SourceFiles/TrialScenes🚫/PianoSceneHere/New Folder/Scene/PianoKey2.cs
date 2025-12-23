using UnityEngine;
using UnityEngine.Events;

public class PianoKey2 : MonoBehaviour
{
    public HingeJoint hinge; // Assign your piano key hinge in Inspector
    public float pressedThreshold = -4f; // Angle at which we consider it "pressed"

    private bool _isPressed = false;

    public AudioSource audioSource; // Assign AudioSource in Inspector
    public PianoKeyMap keyMap; // Assign PianoKeyMap ScriptableObject in Inspector
    public AudioClip keyClip; // Assign specific AudioClip for this key in Inspector

    public UnityEvent onPressed, onReleased;

    void Start()
    {

        hinge = GetComponent<HingeJoint>();
        audioSource = GetComponent<AudioSource>();
        
        string[] parts = gameObject.name.Split('.');
        if (parts.Length > 1)
        {
            // Take the numeric part after the dot
            string numberPart = parts[1];

            // Parse it into an integer
            int keyIndex = int.Parse(numberPart);

            // Use it to select the clip (subtract 1 for zero-based index)
            keyClip = keyMap.clips[keyIndex - 1];
        }
        else
        {
            Debug.LogError("GameObject name format invalid: " + gameObject.name);
        }

        // audioSource.clip = keyClip;
    }

    void Update()
    {
        float angle = hinge.angle;

        // Check if the hinge has rotated past the threshold
        if (!_isPressed && angle <= pressedThreshold)
            Pressed(angle);

        // Reset when it goes back up
        if (_isPressed && angle > pressedThreshold)
            Released(angle);
    }


    private void Pressed(float angle)
    {
        _isPressed = true;
        audioSource.PlayOneShot(keyClip);
        onPressed.Invoke();
        Debug.Log("👆 Key Pressed! Angle: " + angle);
        Debug.Log("Key Pressed: " + gameObject.name);

    }
    
    private void Released(float angle)
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("🚩Key Released! Angle: " + angle);
    }
}
