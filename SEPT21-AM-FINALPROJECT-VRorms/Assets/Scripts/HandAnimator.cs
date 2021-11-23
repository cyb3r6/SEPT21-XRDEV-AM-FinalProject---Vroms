using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    private VRInput controller;
    public Animator handAnimator;
    
    void Start()
    {
        controller = GetComponent<VRInput>();
        handAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (controller)
        {
            if (handAnimator)
            {
                handAnimator.Play("FistClosing", 0, controller.gripValue);
            }
        }
    }
}
