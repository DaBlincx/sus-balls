using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    
    public Settings Settings;

    public struct Movement
    {
        public float Delta;
        public float Position;
        public float RotationAngle;
        public float Min;
        public float Max;
        public float Current;
        public float AnimationTime;
        public int AnimationSign;
        public float Start;
        public Movement(float Delta, float Position, int Min, int Max, int Current, float rotation = 0.0f, float pos = 0.0f, float animTime = 0.0f, int animSign = -1, float Start = 0.0f)
        {
            this.Delta = Delta;
            this.Position = Position;
            this.RotationAngle = rotation;
            this.Min = Min;
            this.Max = Max;
            this.Current = Current;
            this.AnimationTime = animTime;
            this.AnimationSign = animSign;
            this.Start = Start;
        }
    }

    private int minFloor = 0;
    private int maxFloor = 2;
    public int currentFloor = 0;

    public float floor = 0.0f;
    private float upRotation;
    private float animationTimeY = 0.0f;
    private int animationSign = 1;
    private float startHeight;

    public Movement SideMovement = new Movement(3.0f, 0.0f, -1, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get Settings from the Settings Object
        SideMovement.Delta = Settings.LaneWidth;
        SideMovement.Min = - (Settings.LaneCount - 1.0f) / 2.0f;
        SideMovement.Max = (Settings.LaneCount - 1.0f) / 2.0f;
        // Animate rotation
        Quaternion rotation = Quaternion.AngleAxis(upRotation, Vector3.right);
        gameObject.transform.localRotation = rotation;
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.right, upRotation);
        gameObject.transform.RotateAround(gameObject.transform.position, gameObject.transform.forward, SideMovement.RotationAngle);
        gameObject.transform.localPosition = new Vector3(SideMovement.Position, floor, 0);
        gameObject.transform.localPosition += new Vector3(
            Random.Range(-Settings.Shakeness, Settings.Shakeness), 
            Random.Range(-Settings.Shakeness, Settings.Shakeness), 
            Random.Range(-Settings.Shakeness, Settings.Shakeness));
        if (Settings.AllowMovement)
            ProcessControls();
        PlayAnimations();
    }

    void Update()
    {
        
    }

    void PlayAnimations()
    {
        if (animationTimeY > 0.0f)
        {
            float t = 1 - animationTimeY * 2.0f;

            upRotation = (Mathf.Sin((animationTimeY * 2.0f - 0.5f) * Mathf.PI) + 1.0f) * (-animationSign) * Settings.VerticalAngle / 2.0f;
            animationTimeY -= Settings.TransitionSpeed * 0.01f;


            t *= 3.0f;
            float sigError = Sigmoid(-3.0f); // t ist -3.0, also minimum am Anfang
            floor = startHeight + FixedSigmoid(t, sigError) * animationSign * Settings.FloorHeight;
        }
        else
        {
            animationTimeY = 0.0f;
        }

        if (SideMovement.AnimationTime > 0.0f)
        {
            float t = 1 - SideMovement.AnimationTime * 2.0f;

            SideMovement.RotationAngle = (Mathf.Sin((SideMovement.AnimationTime * 2.0f - 0.5f) * Mathf.PI) + 1.0f) * (-SideMovement.AnimationSign) * (Settings.HorizontalAngle / 2.0f);
            SideMovement.AnimationTime -= Settings.TransitionSpeed * 0.01f;

            t *= 3.0f;
            float sigError = Sigmoid(-3.0f); // t ist -3.0, also minimum am Anfang
            SideMovement.Position = SideMovement.Start + FixedSigmoid(t, sigError) * SideMovement.AnimationSign * SideMovement.Delta;
        }
        else
        {
            SideMovement.AnimationTime = 0.0f;
        }
    }

    void ProcessControls()
    {

        if (Input.GetKey(KeyCode.UpArrow) && animationTimeY <= 0.000001f)
        {
            if (currentFloor < maxFloor)
            {
                animationTimeY = 1.0f;
                startHeight = currentFloor * Settings.FloorHeight;
                animationSign = 1;
                currentFloor += 1;
            }      
        }
        if (Input.GetKey(KeyCode.DownArrow) && animationTimeY <= 0.000001f)
        {
            if (currentFloor > minFloor)
            {
                animationTimeY = 1.0f;
                startHeight = currentFloor * Settings.FloorHeight;
                animationSign = -1;
                currentFloor -= 1;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) && SideMovement.AnimationTime <= 0.000001f)
        {
            if (SideMovement.Current < SideMovement.Max)
            {
                SideMovement.AnimationTime = 1.0f;
                SideMovement.Start = SideMovement.Current * SideMovement.Delta;
                SideMovement.AnimationSign = 1;
                SideMovement.Current += 1;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && SideMovement.AnimationTime <= 0.000001f)
        {
            if (SideMovement.Current > SideMovement.Min)
            {
                SideMovement.AnimationTime = 1.0f;
                SideMovement.Start = SideMovement.Current * SideMovement.Delta;
                SideMovement.AnimationSign = -1;
                SideMovement.Current -= 1;
            }
        }



    }

    public static float FixedSigmoid(float value, float sigError)
    {
        float unfixed = Sigmoid(value);
        return (unfixed - sigError) / (1-2.0f*sigError);
    }

    public static float Sigmoid(float value)
    {
        return 1.0f / (1.0f + (float)Mathf.Exp(-value));
    }
}
