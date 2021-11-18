using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for the interactable objects in VR
/// </summary>
public class GrababbleObjectVR : MonoBehaviour
{
    public VRInput controller;
    public Vector3 grabOffset;
    public Vector3 grabRotationOffset;
    public Rigidbody rigidBody;
    public Color hoverColor;
    public Color[] nonHoverColor;
    public Material[] materials;
    public Renderer[] childRenderers;

    private void Start()
    {
        int childCount = transform.childCount;

        childRenderers = GetAllChildrenRenderers();

        foreach(var rend in childRenderers)
        {
            materials = new Material[rend.materials.Length];
            materials = rend.materials;
            nonHoverColor = new Color[rend.materials.Length];
            for (int i = 0; i < materials.Length; i++)
            {

                nonHoverColor[i] = materials[i].color;
            }


        }
        

        rigidBody = GetComponent<Rigidbody>();
    }

    public Renderer[] GetAllChildrenRenderers()
    {
        return this.GetComponentsInChildren<Renderer>();
    }

    public void GetAllMaterialsInChildren()
    {

    }

    public void OnHoverStarted()
    {
        for (int i = 0; i < materials.Length; i++)
        {

            materials[i].color = hoverColor;

            foreach (var rend in childRenderers)
            {
                rend.materials[i].color = hoverColor;
            }
        }

        

    }
    public void OnHoverEnded()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = nonHoverColor[i];
            foreach (var rend in childRenderers)
            {
                rend.materials[i].color = nonHoverColor[i];
            }

        }
       
    }

    public void Grab(VRInput controller)
    {
        transform.SetParent(controller.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;

        // customizing grab offset
        transform.localPosition += grabOffset;
        transform.localEulerAngles += grabRotationOffset;

    }
    public void Release(VRInput controller)
    {
        transform.SetParent(null);
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;

    }

    public void AdvGrab(VRInput controller)
    {
        FixedJoint fx = controller.gameObject.AddComponent<FixedJoint>();
        transform.localPosition = controller.transform.position;
        transform.localRotation = Quaternion.identity;
        fx.connectedBody = rigidBody;
    }

    public void AdvRelease(VRInput controller)
    {
        FixedJoint fx = controller.GetComponent<FixedJoint>();
        Destroy(fx);

    }

    /// <summary>
    /// Using a virtual void rather than an abstract void lets you have the
    /// option of overriding the method, whereas an abstract void 
    /// forces you to override the method, and you must have an
    /// abstract class to use an abstract method.
    /// 
    /// it's worth noting that you can't drag and drop an abstract class
    /// onto a GameObject in the Inspector tab
    /// </summary>
    public virtual void OnInteraction()
    {

    }

    public virtual void OnUpdatingInteraction()
    {

    }

    public virtual void OnStopInteraction()
    {

    }
}
