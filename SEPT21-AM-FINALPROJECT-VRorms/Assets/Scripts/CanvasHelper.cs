using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class CanvasHelper : MonoBehaviour
{
    private float hoverStartAnimationDuration = 0.2f;
    private float hoverEndAnimationDuration = 0.1f;
    private float scaleAnimationSize = 0.5f;
    private Vector3 startScale;


    void Start()
    {
        startScale = transform.localScale;
    }

    public void DoScaleUp(HoverEnterEventArgs args)
    {
        transform.DOScale(scaleAnimationSize, hoverStartAnimationDuration);
    }

    public void DoScaleDown(HoverExitEventArgs args)
    {
        transform.DOScale(startScale, hoverEndAnimationDuration);
    }
}
