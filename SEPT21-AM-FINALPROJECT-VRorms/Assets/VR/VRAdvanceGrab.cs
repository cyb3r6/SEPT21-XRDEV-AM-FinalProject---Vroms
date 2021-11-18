using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAdvanceGrab : MonoBehaviour
{
    /// <summary>
    /// Get the controller input
    /// </summary>
    private VRInput controller;

    /// <summary>
    /// What we're toughing
    /// </summary>
    public GrababbleObjectVR collidingObject;

    /// <summary>
    /// What we're holding
    /// </summary>
    public GrababbleObjectVR heldObject;

    /// <summary>
    /// Snap objects to this position when grabbing
    /// </summary>
    public Transform snapPosition;

    /// <summary>
    /// Check if using fixed joint method of grabbing
    /// </summary>
    public bool useAdvance;

    /// <summary>
    /// How strong throwing is
    /// </summary>
    public float throwForce;


    void Awake()
    {
        controller = GetComponent<VRInput>();

        if (useAdvance)
        {
            controller.OnGripD += AdvGrab;
            controller.OnGripU += AdvRelease;
            //controller.OnGripDown.AddListener(AdvGrab);
        }
        else
        {
            controller.OnGripD += Grab;
            controller.OnGripU += Release;
            //controller.OnGripDown.AddListener(Grab);
        }
    }
    private void OnDisable()
    {
        if (useAdvance)
        {
            controller.OnGripD -= AdvGrab;
            controller.OnGripU -= AdvRelease;
        }
        else
        {
            controller.OnGripD -= Grab;
            controller.OnGripU -= Release;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrababbleObjectVR>();
        if (grab)
        {
            collidingObject = grab;
            collidingObject.OnHoverStarted();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<GrababbleObjectVR>();
        if (grab == collidingObject)
        {
            collidingObject.OnHoverEnded();
            collidingObject = null;
        }
    }

    public void Grab()
    {
        if (collidingObject!=null && collidingObject.rigidBody)
        {
            heldObject = collidingObject;
            heldObject.Grab(controller);

            // add interactions
            controller.OnTriggerDown.AddListener(heldObject.OnInteraction);
            controller.OnTriggerUpdated.AddListener(heldObject.OnUpdatingInteraction);
            controller.OnTriggerUp.AddListener(heldObject.OnStopInteraction);
        }
    }
    public void Release()
    {
        if (heldObject) // if (heldObject!=null)
        {
            heldObject.Release(controller);

            // reset interactions
            controller.OnTriggerDown.RemoveListener(heldObject.OnInteraction);
            controller.OnTriggerUpdated.RemoveListener(heldObject.OnUpdatingInteraction);
            controller.OnTriggerUp.RemoveListener(heldObject.OnStopInteraction);

            // throw
            heldObject.rigidBody.velocity = controller.velocity* throwForce;
            heldObject.rigidBody.angularVelocity = controller.angularVelocity * throwForce;

            // reset held object
            heldObject.rigidBody.isKinematic = false;
            heldObject = null;
        }
    }

    public void AdvGrab()
    {
        if (collidingObject && collidingObject.rigidBody)
        {
            heldObject = collidingObject;
            heldObject.AdvGrab(controller);

            // add interactions
            controller.OnTriggerDown.AddListener(heldObject.OnInteraction);
            controller.OnTriggerUpdated.AddListener(heldObject.OnUpdatingInteraction);
            controller.OnTriggerUp.AddListener(heldObject.OnStopInteraction);
        }
    }

    public void AdvRelease()
    {
        if (heldObject)
        {
            heldObject.AdvRelease(controller);

            // reset interactions
            controller.OnTriggerDown.RemoveListener(heldObject.OnInteraction);
            controller.OnTriggerUpdated.RemoveListener(heldObject.OnUpdatingInteraction);
            controller.OnTriggerUp.RemoveListener(heldObject.OnStopInteraction);

            // throw
            heldObject.rigidBody.velocity = controller.velocity * throwForce;
            heldObject.rigidBody.angularVelocity = controller.angularVelocity * throwForce;

            // reset held object
            heldObject.rigidBody.isKinematic = false;
            heldObject = null;
            
        }
    }
}
