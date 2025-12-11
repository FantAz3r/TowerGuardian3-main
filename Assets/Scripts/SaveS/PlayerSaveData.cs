using UnityEngine;

public struct PlayerSaveData 
{
    public LevelID LevelID;
    public int Floor;
    public float PositionX;
    public float PositionY;
    public float PositionZ;

    public PlayerSaveData(LevelID levelID, int floor, Vector3 position)
    {
        LevelID = levelID;
        Floor = floor;
        PositionX = position.x;
        PositionY = position.y;
        PositionZ = position.z;
    }
}
