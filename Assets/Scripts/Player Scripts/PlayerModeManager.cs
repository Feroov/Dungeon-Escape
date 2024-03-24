using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModeManager : MonoBehaviour
{
    public Behaviour[] componentsForGameMode;
    public Behaviour[] componentsForWaitingRoomMode;
    private bool isInWaitingRoom;

    void Start()
    {
        if (isInWaitingRoom)
        {
            SetWaitingRoomMode();
        }
        else
        {
            SetGameMode();
        }
    }

    public void SetGameMode()
    {
        foreach (var comp in componentsForGameMode)
        {
            comp.enabled = true;
        }
        
        foreach (var comp in componentsForWaitingRoomMode)
        {
            comp.enabled = false;
        }
    }

    public void SetWaitingRoom(bool isInRoom)
    {
        
        isInWaitingRoom = isInRoom;
        if (isInWaitingRoom)
        {
            SetWaitingRoomMode();
        }
        else
        {
            SetGameMode();
        }
    }
    public void SetWaitingRoomMode()
    {
        foreach (var comp in componentsForGameMode)
        {
            comp.enabled = false;
        }
        
        foreach (var comp in componentsForWaitingRoomMode)
        {
            comp.enabled = true;
        }
    }

    // Call this when you're about to change to the game scene
    public void OnStartGame()
    {
        SetGameMode();
        // Additional logic to handle scene transition can be added here.
    }
    
    // Add other methods as needed for your game logic.
}
