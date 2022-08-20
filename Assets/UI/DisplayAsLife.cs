using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAsLife : MonoBehaviour
{

    public PlayerStats playerStats;
    public int DisplayAtMoreThanEqual = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.life >= DisplayAtMoreThanEqual)
        {
            gameObject.GetComponent<RawImage>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<RawImage>().color = Color.gray;
        }
            
    }
}
