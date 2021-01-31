using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameBehaviour : MonoBehaviour
{
    public bool IsMock { get => GameManager.Instance != null ? GameManager.Instance.IsMock : true; }

    /// <summary>Cache field for the PhotonView on this GameObject.</summary>
    private PhotonView pvCache;

    /// <summary>A cached reference to a PhotonView on this GameObject.</summary>
    /// <remarks>
    /// If you intend to work with a PhotonView in a script, it's usually easier to write this.photonView.
    ///
    /// If you intend to remove the PhotonView component from the GameObject but keep this Photon.MonoBehaviour,
    /// avoid this reference or modify this code to use PhotonView.Get(obj) instead.
    /// </remarks>
    public PhotonView photonView
    {
        get
        {
#if UNITY_EDITOR
            // In the editor we want to avoid caching this at design time, so changes in PV structure appear immediately.
            if (!Application.isPlaying || this.pvCache == null)
            {
                this.pvCache = PhotonView.Get(this);
            }
#else
                if (this.pvCache == null)
                {
                    this.pvCache = PhotonView.Get(this);
                }
#endif
            return this.pvCache;
        }
    }


    public bool IsMine { get => IsMock ? true : photonView.IsMine; }

    public GameManager gameManager { get => GameManager.Instance; }

    private bool needsToHookCallback = false;

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddCallback(this);
        }
        else
        {
            needsToHookCallback = true;
        }
        
    }

    private void Start()
    {
        if(needsToHookCallback)
        {
            GameManager.Instance.AddCallback(this);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.RemoveCallback(this);
    }

    public virtual void OnGameStart()
    {

    }

    public virtual void OnHideableItemPicked(HideableItem item, PlayerItemController player)
    {

    }

    public virtual void OnHideableItemDropped(HideableItem item, PlayerItemController player)
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


    public static void PutOnFloor(Transform trans)
    {
        RaycastHit hit;
        float dist = 0.0f;
        if (Physics.Raycast(trans.position, -Vector3.up, out hit))
        {
            //Debug.Log("Find floor!");
            dist = hit.distance;
            trans.position -= new Vector3(0f, dist - trans.localScale.y , 0f);
        } else
        {
            Debug.Log("NOT Find floor!");
        }
    }

    public static void PutOnFloorParent(Transform trans)
    {
        RaycastHit hit;
        float dist = 0.0f;
        if (Physics.Raycast(trans.position, -Vector3.up, out hit))
        {
            //Debug.Log("Find floor!");
            dist = hit.distance;
            trans.parent.position -= new Vector3(0f, dist - trans.localScale.y, 0f);
        }
        else
        {
            Debug.Log("NOT Find floor!");
        }
    }

    public static string GetGameObjectPath(Transform transform)
    {
        string path = transform.name;
        while (transform.parent != null)
        {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }
        return "/" + path;
    }


}
