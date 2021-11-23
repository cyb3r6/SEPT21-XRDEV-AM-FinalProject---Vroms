using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int lives;
    public int startLives = 20;
    public static int Rounds;

    private void Start()
    {
        lives = startLives;
        Rounds = 0;
    }
   
}
