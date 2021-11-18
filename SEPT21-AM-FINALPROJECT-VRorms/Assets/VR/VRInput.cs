using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInput : MonoBehaviour
{
    /// <summary>
    /// Check if this script is on the left hand
    /// </summary>
    [HideInInspector]
    public bool isLeftHand;

    /// <summary>
    /// Choose the hand
    /// </summary>
    public Hand hand = Hand.Left;

    /// <summary>
    /// The trigger value of the controller
    /// </summary>
    public float triggerValue;

    /// <summary>
    /// The grip value of the controller
    /// </summary>
    public float gripValue;

    /// <summary>
    /// The bool to show if the thumbstick is pressed
    /// </summary>
    public bool isThumbStickPressed = false;
    
    /// <summary>
    /// The velocity of the controller
    /// </summary>
    public Vector3 velocity;

    /// <summary>
    /// The angular velocity of the controller
    /// </summary>
    public Vector3 angularVelocity;

    /// <summary>
    /// The value in the X and Y of the thumbstick
    /// </summary>
    public Vector2 Thumbstick;

    #region Using UnityEvents

    public UnityEvent OnGripDown;
    public UnityEvent OnGripUpdated;
    public UnityEvent OnGripUp;
    public UnityEvent OnTriggerDown;
    public UnityEvent OnTriggerUpdated;
    public UnityEvent OnTriggerUp;
    public UnityEvent OnThumbstickDown;
    public UnityEvent OnThumbstickUpdated;
    public UnityEvent OnThumbstickUp;

    #endregion

    #region Using Actions

    public UnityAction OnGripD;
    public UnityAction OnGripUD;
    public UnityAction OnGripU;
    public UnityAction OnTriggerD;
    public UnityAction OnTriggerUD;
    public UnityAction OnTriggerU;
    public UnityAction OnThumbstickD;
    public UnityAction OnThumbstickUD;
    public UnityAction OnThumbstickU;

    #endregion

    private Vector3 previousPosition;
    private Vector3 previousAngularRotation;

    public string triggerButton;
    private string triggerAxis;
    private string gripAxis;
    private string gripButton;
    private string thumbstickButton;
    private string thumbstickX;
    private string thumbstickY;


    void Start()
    {

        triggerAxis = $"XRI_{hand}_Trigger";
        gripAxis = $"XRI_{hand}_Grip";
        thumbstickButton = $"XRI_{hand}_Primary2DAxisClick";
        thumbstickX = $"XRI_{hand}_Primary2DAxis_Horizontal";
        thumbstickY = $"XRI_{hand}_Primary2DAxis_Vertical";
        triggerButton = $"XRI_{hand}_TriggerButton";
        gripButton = $"XRI_{hand}_GripButton";

    }

    
    void Update()
    {
        triggerValue = Input.GetAxis(triggerAxis);        
        gripValue = Input.GetAxis(gripAxis);

        // OR: Thumbstick = new Vector2(thumbstickXValue, thumbstickYValue);
        Thumbstick = new Vector2(Input.GetAxis(thumbstickX), Input.GetAxis(thumbstickY));

        if (Input.GetButtonDown(gripButton))
        {
            OnGripDown?.Invoke();
            OnGripD?.Invoke();
        }
        if (Input.GetButton(gripButton))
        {
            OnGripUpdated?.Invoke();
            OnGripUD?.Invoke();
        }

        if (Input.GetButtonUp(gripButton))
        {
            OnGripUp?.Invoke();
            OnGripU?.Invoke();
        }

        if (Input.GetButtonDown(triggerButton))
        {
            OnTriggerDown?.Invoke();
            OnTriggerD?.Invoke();
        }
        
        if (Input.GetButton(triggerButton))
        {
            isThumbStickPressed = true;
            OnTriggerUpdated?.Invoke();
            OnTriggerUD?.Invoke();
        }

        if (Input.GetButtonUp(triggerButton))
        {
            isThumbStickPressed = false;
            OnTriggerUp?.Invoke();
            OnTriggerU?.Invoke();
        }

        if (Input.GetButtonDown(thumbstickButton))
        {
            OnThumbstickDown?.Invoke();
            OnThumbstickD?.Invoke();
        }
        if (Input.GetButton(thumbstickButton))
        {
            OnThumbstickUpdated?.Invoke();
            OnThumbstickUD?.Invoke();
        }
        if (Input.GetButtonUp(thumbstickButton))
        {
            OnThumbstickUp?.Invoke();
            OnThumbstickU?.Invoke();
        }

        velocity = (this.transform.position - previousPosition) / Time.deltaTime;
        previousPosition = this.transform.position;

        angularVelocity = (this.transform.eulerAngles - previousAngularRotation) / Time.deltaTime;
        previousAngularRotation = this.transform.eulerAngles;
    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}
