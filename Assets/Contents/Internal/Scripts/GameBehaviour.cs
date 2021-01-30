using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public bool IsMock { get => GameManager.Instance.IsMock; }

    private void OnEnable()
    {
        GameManager.Instance.AddCallback(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.RemoveCallback(this);
    }

    public virtual void OnGameStart()
    {

    }

    public virtual void OnItemPicked(HideableItem item, GameObject player)
    {

    }

    public virtual void OnItemDropped(HideableItem item, GameObject player)
    {

    }

    public virtual void OnGameEnd(string winner)
    {

    }

    public static Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}
