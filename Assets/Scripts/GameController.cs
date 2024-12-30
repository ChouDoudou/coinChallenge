using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isInGame = true;
    public bool IsInGame
    {
        get
        {
            return isInGame;
        }
        set
        {
            isInGame = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NomTemporaire()
    {
        while(isInGame)
        {
            
        }
    }
}
