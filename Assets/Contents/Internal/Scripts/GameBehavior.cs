using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
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

    public virtual void OnItemPicked(ItemEscondivel item, GameObject player)
    {

    }

    public virtual void OnItemDropped(ItemEscondivel item, GameObject player)
    {

    }

    public virtual void OnGameEnd(string winner)
    {

    }
}
