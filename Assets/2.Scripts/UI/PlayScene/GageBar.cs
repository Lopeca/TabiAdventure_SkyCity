using System;
using UnityEngine;
using UnityEngine.UI;

public class GageBar : MonoBehaviour
{
    [SerializeField] Image gage;

    private void Awake()
    {
        gage = GetComponent<Image>();
    }

    public void SetGage(float value)
    {
        gage.fillAmount = value;
    }
}
