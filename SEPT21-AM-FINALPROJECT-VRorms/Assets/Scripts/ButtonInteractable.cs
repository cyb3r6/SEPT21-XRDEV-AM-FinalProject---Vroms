using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class ButtonInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float hoverStartAnimationDuration = 0.2f;
    private float hoverEndAnimationDuration = 0.1f;
    private float scaleAnimationSize = 1.08f;
    private Vector3 startScale;

    private Image image;
    private TMP_Text text;

    public Color startTextColor;
    public Color hoverTextColor;
    public Color startImageColor;
    public Color hoverImageColor;

    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip selectSound;

    public UnityEvent OnClick;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
        startScale = transform.localScale;
    }

    /// <summary>
    /// Hover
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // kill all animations first
        transform.DOKill();

        // hover animations
        transform.DOScale(scaleAnimationSize, hoverStartAnimationDuration);
        text.DOColor(hoverTextColor, hoverStartAnimationDuration);
        image.DOColor(hoverImageColor, hoverStartAnimationDuration);

        // hover sound
        SoundManager.instance.PlaySound(hoverSound, audioSource);
    }

    /// <summary>
    /// Exit hover
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // kill all animations first
        transform.DOKill();

        // exit hover animations
        transform.DOScale(startScale, hoverEndAnimationDuration);
        text.DOColor(startTextColor, hoverEndAnimationDuration);
        image.DOColor(startImageColor, hoverEndAnimationDuration);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UISystemProfilerApi.AddMarker("Button.onClick", this);

        // play a select sound
        SoundManager.instance.PlaySound(selectSound, audioSource);

        OnClick?.Invoke();
    }



}