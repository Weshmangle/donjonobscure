using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnEntry(Entity entity)
    {
        entity.KillEntity();
    }
}
