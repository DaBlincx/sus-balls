using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FOVChanger : MonoBehaviour
{
    public Slider fovSlider;
    public Text CurrentFOV;

    void Start()
    {
        Camera.main.fieldOfView = 60.0f;
    }

    void Update()
    {
        CurrentFOV.text = "FOV: " + Mathf.Ceil(Camera.main.fieldOfView).ToString();
    }

    public void ChangeFOV()
    {
        Camera.main.fieldOfView = fovSlider.value;
    }
}