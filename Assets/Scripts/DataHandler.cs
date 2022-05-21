using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandler
{
    public static void LoadGameData()
    {
        Globals.Ranks = Resources.LoadAll<Rank>("Ranks") as Rank[];
    }
}
