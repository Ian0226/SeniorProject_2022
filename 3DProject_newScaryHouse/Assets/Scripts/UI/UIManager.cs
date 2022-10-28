using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private InventoryInterface inventoryInterface = new InventoryInterface();
    private InteractiveInterface interactiveInterface = new InteractiveInterface();
    private GamePauseInterface gamePauseInterface = new GamePauseInterface();

    public InventoryInterface GetInventoryInterface()
    {
        return inventoryInterface;
    }
    public InteractiveInterface GetInteractiveInterface()
    {
        return interactiveInterface;
    }
    public GamePauseInterface GetGamePauseInterface()
    {
        return gamePauseInterface;
    }
    public void UIInitialize()
    {
        inventoryInterface.Initialize();
        interactiveInterface.Initialize();
    }
    public void UIUpdate()
    {
        inventoryInterface.Update();
        interactiveInterface.Update();
        
    }
}
