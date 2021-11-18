using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestVRButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    private int numClicks = 0;

    public void OnVRClick()
    {
        numClicks++;
        text.text = $"number of clicks {numClicks}";
    }
}
