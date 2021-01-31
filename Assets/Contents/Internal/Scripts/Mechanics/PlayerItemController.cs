﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemController : GameBehaviour
{

    private InputMaster.GameplayControlsActions gameplayControls;

    private void Awake()
    {
        Debug.Log("tag: " + tag + " IsMine " + IsMine);
        gameplayControls = new InputMaster().GameplayControls;
        if (IsMine)
        {
            gameplayControls.Interact.performed += OnInteractPerformed;
        }
    }

    private void OnEnable()
    {
        if (IsMine)
        {
            gameplayControls.Enable();
        }
    }

    private void OnDisable()
    {
        if (IsMine)
        {
            gameplayControls.Disable();
        }
    }


    public Transform pickSpawnPoint { get; private set; }
    public Transform pickSpawnPointHand2 { get; private set; }
    public bool autoPick = false;
    public HideableItem itemToPick { get; private set; }
    public HideableItem itemPicked { get; set; }
    public HideableItem itemPickedHand2 { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(IsMine)
        {
            GameManager.Instance.player = this;
        }
        if(gameplayControls.enabled)
        {
            GameManager.Instance.IsLocalPlayerHuman = tag == "Human";
            Debug.Log("IsLocalPlayerHuman = " + GameManager.Instance.IsLocalPlayerHuman);
        }

        if (tag != "Human")
        {
            if (pickSpawnPoint == null)
            {
                pickSpawnPoint = RecursiveFindChild(transform, "bone_jaw");
            }
        }
        else
        {
            if (pickSpawnPoint == null)
            {
                pickSpawnPoint = RecursiveFindChild(transform, "IndexFinger2_R");
                pickSpawnPointHand2 = RecursiveFindChild(transform, "IndexFinger2_L");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HideableItem")
        {
            var item = other.GetComponent<HideableItem>();
            if(autoPick)
            {
                PickHideableItem(item);
            }
            else
            {
                itemToPick = item;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "HideableItem")
        {
            var item = other.GetComponent<HideableItem>();

            if(itemToPick == item) {
                itemToPick = null;
            }
        }
    }

    private void PickHideableItem(HideableItem item)
    {
        if (IsMock)
        {
            gameManager.PickHideableItem(GetGameObjectPath(item.transform), GetGameObjectPath(this.transform));
        }
        else
        {
            gameManager.photonView.RPC("PickHideableItem", Photon.Pun.RpcTarget.All, GetGameObjectPath(item.transform), GetGameObjectPath(this.transform));
        }
    }

    private void DropHideableItem(HideableItem item)
    {
        if (IsMock)
        {
            gameManager.DropHideableItem(GetGameObjectPath(item.transform), GetGameObjectPath(this.transform));
        }
        else
        {
            gameManager.photonView.RPC("DropHideableItem", Photon.Pun.RpcTarget.All, GetGameObjectPath(item.transform), GetGameObjectPath(this.transform));
        }
    }


    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        if (itemPicked != null)
        {
            DropHideableItem(itemPicked);
        }
        else
        {
            if(itemToPick)
            {
                PickHideableItem(itemToPick);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (IsMock)
        {
            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    DropHideableItem();
            //}
        }
    }



}
