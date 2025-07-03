using System;
using UnityEngine;

public static class GlobalEvents
{
    public static Action<EnemyBT> OnPlayerEnterFightZone;
    public static Action<EnemyBT> OnPlayerExitFightZone;
    public static Action<EnemyBT> OnPlayerWin;
}