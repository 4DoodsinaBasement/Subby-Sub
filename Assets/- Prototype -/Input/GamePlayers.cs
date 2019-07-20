using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public static class GamePlayers
{
    public static PlayerInfo player1, player2, player3, player4;

    static GamePlayers()
    {
        player1 = new PlayerInfo(0, PlayerType.FPS);
    }
}

public enum PlayerType {Null, Menu, FPS, RTS};

public class PlayerInfo
{
    public int ID;
    public Player rewiredPlayer;
    
    PlayerType _type; public PlayerType type
    {
        get { return _type; }
        set 
        { 
            switch(value)
            {
                case PlayerType.Null:
                    SetAllMapsInCategory("Default", false);
                    SetAllMapsInCategory("FPS", false);
                    SetAllMapsInCategory("RTS", false);
                    break;
                case PlayerType.Menu:
                    SetAllMapsInCategory("Default", true);
                    SetAllMapsInCategory("FPS", false);
                    SetAllMapsInCategory("RTS", false);
                    break;
                case PlayerType.FPS:
                    SetAllMapsInCategory("Default", true);
                    SetAllMapsInCategory("FPS", true);
                    SetAllMapsInCategory("RTS", false);
                    break;
                case PlayerType.RTS:
                    SetAllMapsInCategory("Default", true);
                    SetAllMapsInCategory("FPS", false);
                    SetAllMapsInCategory("RTS", true);
                    break;
            }
        }
    }

    public PlayerInfo(int ID, PlayerType type)
    {
        this.ID = ID;
        this.type = type;

        if (ReInput.players.GetPlayer(ID) != null)
        {
            rewiredPlayer = ReInput.players.GetPlayer(ID);
        }
        else
        {
            this.type = PlayerType.Null;
        }
    }

    void SetAllMapsInCategory(string category, bool setValue)
    {
        if (rewiredPlayer != null)
        {
            IEnumerable<ControllerMap> categoryMaps = rewiredPlayer.controllers.maps.GetAllMapsInCategory(category);

            foreach (ControllerMap map in categoryMaps)
            {
                rewiredPlayer.controllers.maps.GetMap(map.id).enabled = setValue;
            }
        }
    }

    public Vector2 GetVector2(string xAxisName, string yAxisName)
    {
        Vector2 vectorToReturn = Vector2.zero;

        vectorToReturn.x = rewiredPlayer.GetAxis(xAxisName);
        vectorToReturn.y = rewiredPlayer.GetAxis(yAxisName);
        
        if (rewiredPlayer.GetButton(xAxisName + "_ButtonNegative")) { vectorToReturn.x = -1.0f; }
        if (rewiredPlayer.GetButton(xAxisName + "_ButtonPositive")) { vectorToReturn.x = 1.0f; }
        
        if (rewiredPlayer.GetButton(yAxisName + "_ButtonNegative")) { vectorToReturn.y = -1.0f; }
        if (rewiredPlayer.GetButton(yAxisName + "_ButtonPositive")) { vectorToReturn.y = 1.0f; }

		if (vectorToReturn.magnitude > 1.0f) { vectorToReturn = vectorToReturn.normalized; }

        return vectorToReturn;
    }

    public Vector3 GetVector3(string xAxisName, string zAxisName)
    {
        Vector3 vectorToReturn = Vector3.zero;

        vectorToReturn.x = rewiredPlayer.GetAxis(xAxisName);
        vectorToReturn.z = rewiredPlayer.GetAxis(zAxisName);
        
        if (rewiredPlayer.GetButton(xAxisName + "_ButtonNegative")) { vectorToReturn.x = -1.0f; }
        if (rewiredPlayer.GetButton(xAxisName + "_ButtonPositive")) { vectorToReturn.x = 1.0f; }
        
        if (rewiredPlayer.GetButton(zAxisName + "_ButtonNegative")) { vectorToReturn.z = -1.0f; }
        if (rewiredPlayer.GetButton(zAxisName + "_ButtonPositive")) { vectorToReturn.z = 1.0f; }

		if (vectorToReturn.magnitude > 1.0f) { vectorToReturn = vectorToReturn.normalized; }

        return vectorToReturn;
    }

    #region UpOneLevel Functions
    public float GetAxis(string axisName) { return rewiredPlayer.GetAxis(axisName); }
    public bool GetButton(string buttonName) { return rewiredPlayer.GetButton(buttonName); }
    public bool GetButtonDown(string buttonName) { return rewiredPlayer.GetButtonDown(buttonName); }
    public bool GetButtonUp(string buttonName) { return rewiredPlayer.GetButtonUp(buttonName); }
    #endregion
}
