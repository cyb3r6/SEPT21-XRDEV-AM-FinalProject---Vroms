using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    /// <summary>
    /// prefabs to duplicate
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// the amount of prefabs to spawn in scene
    /// </summary>
    public int count;

    /// <summary>
    /// the rate at which we spawn the enemy
    /// </summary>
    public float rate;
}
