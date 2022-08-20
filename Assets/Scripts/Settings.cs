using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class Settings : MonoBehaviour
{
    // Start is called before the first frame update

    // World Settings //
    [Range(2.5f, 3.5f)]
    public float LaneWidth = 3.0f;
    [Range(1, 5)]
    public int LaneCount = 5;
    [Range(2.5f, 3.5f)]
    public float FloorHeight = 3.0f;
    
    [Range(50.5f, 55.0f)]
    public float Height = 52;

    // Animation Settings //
    public bool AllowMovement = true;
    [Range(1.0f, 70.0f)]
    public float VerticalAngle = 20.0f;
    [Range(10.0f, 180.0f)]
    public float HorizontalAngle = 80.0f;
    [Range(1.0f, 20.0f)]
    public float BaseTransitionSpeed = 5.0f;

    // Spawn Settings //
    [Range(10.0f, 60.0f)]
    public float GapSize = 20.0f;
    public float HitRecoverTime = 2.0f;
    public float Shakeness = 1.0f;

    [Range(20.0f, 500.0f)]
    public float baseSpeed = 120.0f;

    [Range(0, 100)]
    public float PowerUpChance = 10.0f;
    
    [Range(1, 5)]
    public float MaxTimeMultiplier = 2.0f;

    [Range(0.001f, 0.1f)]
    public float MultiplierGrowth = 0.01f;

    [HideInInspector]
    public float TimeMultiplier = 1.0f;

    [HideInInspector]
    public float TransitionSpeed = 5.0f;


    [HideInInspector]
    public float Speed = 120.0f;

    void Start()
    {
        TransitionSpeed = BaseTransitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetSettings(Settings settings)
    {
        AllowMovement = settings.AllowMovement;
        VerticalAngle = settings.VerticalAngle;
        HorizontalAngle = settings.HorizontalAngle;
        GapSize = settings.GapSize;
        HitRecoverTime = settings.HitRecoverTime;
        Shakeness = settings.Shakeness;
        PowerUpChance = settings.PowerUpChance;
        TimeMultiplier = settings.TimeMultiplier;
        MaxTimeMultiplier = settings.MaxTimeMultiplier;
        baseSpeed = settings.baseSpeed;
        BaseTransitionSpeed = settings.BaseTransitionSpeed;
    }
}
