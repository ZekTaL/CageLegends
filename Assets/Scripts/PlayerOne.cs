using System.Collections;
using UnityEngine;

using FraWork.Mobile;

/// <summary>
/// Class that contains the player controller 
/// </summary>
public class PlayerOne : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private Rigidbody rb;

    public bool canPlayerMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // if the game is running on Android, iOS, and not in UnityEditor,
        // Initialise the Joystick Mobile Input as the default player controller
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (!MobileInput.Initialised)
           MobileInput.Initialise();
#endif
    }

    private void FixedUpdate()
    {
        if (!canPlayerMove)
            return;

        // Switch between Mobile and PC Input
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        // Mobile Inputs
        float moveHorizontal = MobileInput.GetJoystickAxis(JoystickAxis.Horizontal);
        float moveVertical = MobileInput.GetJoystickAxis(JoystickAxis.Vertical);
#else
        // PC Inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
#endif

        // calculate movement for both inputs
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = (movement) * speed;
    }
}
