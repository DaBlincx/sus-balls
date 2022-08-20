using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour
{
    public PlayerStats Player;
    public Text LifeText;
    public Text PunkteText;
    public Text SpeedText;
    public GameObject GameOverScreen;
    public Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeText.text = "Leben: " + Player.life;
        PunkteText.text = "Punkte: " + Player.Punkte;
        SpeedText.text = "Speed: " + settings.Speed.ToString("F2") + "km/h";
        GameOverScreen.SetActive(Player.GameOver);
    }
}