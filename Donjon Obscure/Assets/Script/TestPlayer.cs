using UnityEngine;
using System.Collections.Generic;

public class TestPlayer : MonoBehaviour
{
    public List<Tile> sequency;
    public List<Tile>.Enumerator current;

    public bool record;

    private void Start() {
       current = sequency.GetEnumerator();
    }

    private void Update()
    {
        if(record)
        {
            sequency.Add(Game.Instance.tileCliked);
        }
        else
        {
            //Game.Instance.tileCliked.OnMouseDown()
        }
    }   
}