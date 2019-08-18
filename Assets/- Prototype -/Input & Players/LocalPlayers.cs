using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum PlayerID { Player1 = 0, Player2 = 1, Player3 = 2, Player4 = 3 }

public static class LocalPlayers
{
    public static List<PlayerInfo> players = new List<PlayerInfo>();

    static LocalPlayers()
    {
        players.Add(new PlayerInfo(PlayerID.Player1, PlayerType.Default));
        players.Add(new PlayerInfo(PlayerID.Player2, PlayerType.Default));
        players.Add(new PlayerInfo(PlayerID.Player3, PlayerType.Default));
        players.Add(new PlayerInfo(PlayerID.Player4, PlayerType.Default));
    }

    public static PlayerInfo GetPlayer(PlayerID ID)
    {
        foreach (PlayerInfo player in players)
        if (player.ID == ID) { return player; }

        return null;
    }
}



public enum PlayerType { Default = 0, SubSystem = 1 }

public class PlayerInfo
{
    public PlayerID ID;
    public Player rewiredPlayer;
    
    PlayerType _type; public PlayerType type
    {
        get { return _type; }
        set
        {
            if (rewiredPlayer != null)
            {
                _type = value;
                rewiredPlayer.controllers.maps.SetAllMapsEnabled(false);
                rewiredPlayer.controllers.maps.SetMapsEnabled(true, (int)value);
            }
        }
    }

    public PlayerInfo(PlayerID ID, PlayerType type)
    {
        this.ID = ID;
        this.type = type;

        if (ReInput.players.GetPlayer((int)ID) != null)
        {
            rewiredPlayer = ReInput.players.GetPlayer((int)ID);
        }
    }

    #region GetVector
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
    #endregion

    #region UpOneLevel Functions
    public float GetAxis(string axisName) { return rewiredPlayer.GetAxis(axisName); }
    public bool GetButton(string buttonName) { return rewiredPlayer.GetButton(buttonName); }
    public bool GetButtonDown(string buttonName) { return rewiredPlayer.GetButtonDown(buttonName); }
    public bool GetButtonUp(string buttonName) { return rewiredPlayer.GetButtonUp(buttonName); }
    #endregion
}
