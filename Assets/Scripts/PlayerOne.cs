using System.Collections;
using UnityEngine;

using FraWork.Mobile;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private Rigidbody rb;

    public bool canPlayerMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        if (!MobileInput.Initialised)
           MobileInput.Initialise();
#endif
    }

    private void FixedUpdate()
    {
        if (!canPlayerMove)
            return;

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
