using UnityEngine;

public abstract class ElementGrid : MonoBehaviour
{
    [SerializeField] protected bool animationOver;

    public bool IsAnimationOver()
    {
        return animationOver;
    }
}