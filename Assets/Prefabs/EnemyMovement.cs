using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    int time;
    public float amplitude;
    public int RangeMin, RangeMax;
    private Vector3 basePos;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        basePos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.localPosition = basePos + new Vector3(Mathf.Sin((float)time / 100.0f * Speed) * amplitude,0,0);
        time++;
    }
}
