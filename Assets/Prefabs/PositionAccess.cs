using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAccess : MonoBehaviour
{
    public enum Type
    {
        Damage,
        Heal
    }
    public int Width;
    public int Height;
    public bool allowFly;
    public List<Vector2Int> PowerUpPositions = new List<Vector2Int>();
    public Type type;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
