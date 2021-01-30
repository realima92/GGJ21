using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMock : GameBehaviour
{

    void Start()
    {
        GameManager.Instance.StartGame( (int) System.DateTime.Now.Ticks, new Photon.Pun.PhotonMessageInfo { });
    }

}
