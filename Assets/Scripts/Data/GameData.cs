using System;
using System.Collections.Generic;


[Serializable]
public class GameData
{
    public string FavoriteFoodAddress = "Assets/Prefabs/Item/FavoriteFood.prefab";
    public string HateFoodAddress = "Assets/Prefabs/Item/HateFood.prefab";
    public string PositionAddress = "Assets/Prefabs/Item/ItemPositions.prefab";

    public string InGamePathBGM = "Assets/Sounds/BGM/InGame.wav"; 
    public string LobbyPathBGM = "Assets/Sounds/BGM/Lobby.wav";

    public int FAVORITE_FOOD_MAX_COUNT = 9;
    public int HATE_FOOD_MAX_COUNT = 9;
    public int SCORE_RANK_MAX_COUNT = 5;
    public int START_PLAY_TIME = 60;
}