using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class DockZone : XRSocketInteractor
{
    [Header("starting or held item")]
    [Tooltip("the currently held item. set in editor to equip on start")]
    public XRGrabInteractable heldItem;

    public XRGrabInteractable closetsGrabbable;


    public UnityEvent OnDockEvent;
    public UnityEvent OnUndockedEvent;


    private Rigidbody heldItemRigidbody;
    private bool heldItemWasKinematic;

    private GrabbableInTrigger grabZone;
    private CanvasHelper canvashelper;

    private DockZoneOffset offset;

    void Start()
    {
        grabZone = GetComponent<GrabbableInTrigger>();
        canvashelper = GetComponentInChildren<CanvasHelper>();

        hoverEntered.AddListener(canvashelper.DoScaleUp);
        hoverExited.AddListener(canvashelper.DoScaleDown);

    }
   
}

