using UnityEngine;

[CreateAssetMenu(fileName = "PianoKeyMap", menuName = "Piano/KeyMap")]
public class PianoKeyMap : ScriptableObject
{
    public AudioClip[] clips; // index matches key number
}
