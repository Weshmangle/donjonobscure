using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetScrip : MonoBehaviour
{
    private static GameAssetScrip _i;
    public static GameAssetScrip instance
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssetScrip>("GameAssetScript"));
            return _i;
        }
    }
    public AudioClip chest_opening;
}
