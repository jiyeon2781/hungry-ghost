using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum Scene
    {
        Default,
        Start, // 시작 화면
        InGame, // 게임 진행 중
        Result, // 결과 화면
        Loading, // 로딩 화면
    }

    public enum Item
    {
        Favorite,
        Hate,
        MaxCount,
    }
}
