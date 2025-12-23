using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private bool on = false;
    private bool buttonHit = false;
    private GameObject button;

    private float buttonDownDistance = 0.025f;
    private float buttonReturnSpeed = 0.001f;
    private float buttonOriginalY;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = transform.GetChild(0).gameObject;
        buttonOriginalY = button.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHit)
        {
            buttonHit = false;
            on = !on;
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y - buttonDownDistance, button.transform.position.z);
        }

        if (button.transform.position.y < buttonOriginalY)
        {
            button.transform.position += new Vector3(0, buttonReturnSpeed, 0);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Name: " + other.gameObject.name);
        Debug.Log(other.gameObject.tag);
        if(other.CompareTag("Hand"))
        {
            buttonHit = true;
        }
    }

}
// using UnityEngine;

// public class ButtonControl : MonoBehaviour
// {
//     private bool on = false;
//     private bool buttonHit = false;
//     private GameObject button;

//     private float buttonDownDistance = 0.025f;
//     private float buttonReturnSpeed = 0.001f;
//     private float buttonOriginalY;

//     private float buttonHitAgainTime = 0.5f;
//     private float canHitAgain;


//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         button = transform.GetChild(0).gameObject;
//         buttonOriginalY = button.transform.position.y;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (buttonHit)
//         {
//             buttonHit = false;
//             on = !on;
//             button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y - buttonDownDistance, button.transform.position.z);
//         }

//         if (button.transform.position.y < buttonOriginalY)
//         {
//             button.transform.position += new Vector3(0, buttonReturnSpeed, 0);

//         }
//     }

//     private void OnTriggerStay(Collider other)
//     {
//         Debug.Log("Name: " + other.gameObject.name);
//         Debug.Log(other.gameObject.tag);
//         if(other.CompareTag("Hand") && canHitAgain < Time.time)
//         {
//             buttonHit = true;
//             canHitAgain = Time.time + buttonHitAgainTime;
//         }
//     }

// }
