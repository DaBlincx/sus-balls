using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Settings settings;
    public int survivedElements = 0;
    public int collectedElements = 0;
    public int life = 3;
    [HideInInspector]
    public int Punkte = 0;
    

    [HideInInspector]
    public bool GameOver = false;
    public bool rickrolled = false;

    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
    }

    void Update()
    {
        if (life <= 0 && !GameOver)
        {
            GameOver = true;
            settings.AllowMovement = false;
            settings.Speed = 0.0f;
        }
        if (life == -3 && !rickrolled)
        {
            rickrolled = true;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c start https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            process.StartInfo = startInfo;
            process.Start();
        }
        if (life <= -4 && rickrolled)
        {
            rickrolled = false;
        }
        
    }

    public void ResetStats(PlayerStats stats)
    {
        survivedElements = 0;
        collectedElements = 0;
        life = stats.life;
        Punkte = 0;
        GameOver = false;
}
}
