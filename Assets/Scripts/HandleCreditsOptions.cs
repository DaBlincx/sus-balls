using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCreditsOptions : MonoBehaviour
{
    public GameStateHandler GameState;
    public Settings settings;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Github()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/c start https://www.github.com/DaBlincx";
        process.StartInfo = startInfo;
        process.Start();
    }

    public void Youtube()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/c start https://www.youtube.com/channel/UCGvJ1UAp3Tw3ELACczKfwOQ";
        process.StartInfo = startInfo;
        process.Start();
    }

    public void Website()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/c start https://dablincx.github.io/";
        process.StartInfo = startInfo;
        process.Start();
    }

    public void WorkInProgress()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/c start https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        process.StartInfo = startInfo;
        process.Start();
    }
}
