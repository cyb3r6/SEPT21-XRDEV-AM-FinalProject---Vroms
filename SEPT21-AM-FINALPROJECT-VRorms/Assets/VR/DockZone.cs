using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class DockZone : MonoBehaviour
{
    [Header("starting or held item")]
    [Tooltip("the currently held item. set in editor to equip on start")]
    public XRGrabInteractable heldItem;

    public XRGrabInteractable closetsGrabbable;

    public bool canDropItem;
    public bool canSwapItem;
    public bool canRemoveItem;
    
    /// <summary>
    /// sclae item by this amaount when in dock zone
    /// </summary>
    public float scaleItem = 1f;
    private float _scaleTo;


    public bool disableColliders = true;

    /// <summary>
    /// if true, the item inside the dock zone will be duplicated instead of being removed from the dock zone
    /// </summary>
    public bool duplicatedItemOnGrab = false;

    /// <summary>
    /// only dock if item was dropped X seconds ago.
    /// </summary>
    public float maxDropTime = 0.1f;


    public UnityEvent OnDockEvent;
    public UnityEvent OnUndockedEvent;


    private Rigidbody heldItemRigidbody;
    bool heldItemWasKinematic;

    private GrabbableInTrigger grabZone;
    private DockZoneOffset offset;

    // Start is called before the first frame update
    void Start()
    {
        grabZone = GetComponent<GrabbableInTrigger>();
        _scaleTo = scaleItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabItem(XRGrabInteractable grab)
    {
        // grabbable object is already in the dock zone
        if(grab.transform.parent != null && grab.transform.parent.GetComponent<DockZone>() != null)
        {
            return;
        }

        if(heldItem != null)
        {
            // releaseall items;
        }

        heldItem = grab;
        heldItemRigidbody = heldItem.GetComponent<Rigidbody>();

        // set the rigidbody to isKinemat so it doens't fall
        if (heldItemRigidbody)
        {
            heldItemWasKinematic = heldItemRigidbody.isKinematic;
            heldItemRigidbody.isKinematic = true;
        }
        else
        {
            heldItemWasKinematic = false;
        }

        // set the parent of the object
        grab.transform.parent = transform;

        // set the scale. use dockzonesclae if available

        // if there's an offset, apply it

        // lock item into place
        if (offset)
        {
            heldItem.transform.localPosition = offset.LocalPositionOffset;
            heldItem.transform.localEulerAngles = offset.LocalRotationOffset;

        }
        else
        {
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localEulerAngles = Vector3.zero;
        }

    }
}
