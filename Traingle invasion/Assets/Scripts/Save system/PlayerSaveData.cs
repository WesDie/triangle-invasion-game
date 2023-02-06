using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData : MonoBehaviour
{
    public int highScore;

    public PlayerSaveData (ManageGame manageGame)
    {
        highScore = manageGame.score;
    }
}
