using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipCollider : MonoBehaviour
{
    public Settings Settings;
    public PlayerStats Player;
    public GameObject Shield;


    private float hitPreventionTime;
    private float shakeness;

    // Start is called before the first frame update
    void Start()
    {
        hitPreventionTime = 0.0f;
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.localPosition += new Vector3(
            Random.Range(-shakeness, shakeness),
            Random.Range(-shakeness, shakeness),
            Random.Range(-shakeness, shakeness));
        shakeness *= 0.98f;
        if (hitPreventionTime > 0)
        {
            hitPreventionTime -= 1 / 50.0f;
        }
        else
        {   
            if (Shield.active)
                Shield.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        GameObject go = collision.gameObject;

        switch (go.GetComponentInParent<PositionAccess>().type)
        {
            case PositionAccess.Type.Damage:
                if (hitPreventionTime <= 0)
                {
                    shakeness = 0.4f;
                    Player.life--;
                    Shield.SetActive(true);
                    hitPreventionTime = Settings.HitRecoverTime;
                }
                break;
            case PositionAccess.Type.Heal:
                if (Player.life < 5)
                {
                    Player.life++;  
                }
                Destroy(collision.gameObject);
                break;
        }
    }
}
