using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region time
    public double Duration = 60.0f;
    private double StartTime = 0.0f;
    private double CurrentTime { get => IsMock ? Time.time : PhotonNetwork.Time; }
    public double ElapsedTime { get => CurrentTime - StartTime; }
    #endregion

    #region booleans
    public bool IsPlaying { get; private set; }
    public bool IsFinished { get; private set; }
    public bool IsMock { get; private set; }
    public bool IsMasterClient { get => IsMock ? true : PhotonNetwork.IsMasterClient; }
    #endregion

    #region Player
    public PlayerData[] playerData;
    #endregion

    #region callbacks
    [HideInInspector]
    public List<GameBehaviour> callbacks = new List<GameBehaviour>();
    public void AddCallback(GameBehaviour callback)
    {
        //Debug.Log("Callback vinculado!");
        callbacks.Add(callback);
    }
    public void RemoveCallback(GameBehaviour callback)
    {
        //Debug.Log("Callback retirado!");
        callbacks.Remove(callback);
    }

    #endregion

    #region singleton
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    List<HideableItem> itemsEscondiveis = new List<HideableItem>();
    List<HideableItem> itemsEncontrados = new List<HideableItem>();
    List<HideableItem> itemsPerdidos = new List<HideableItem>();

    public bool IsLocalPlayerHuman { get; set; }

    public void CreatePlayer()
    {
        if (!PhotonNetwork.InRoom) return;

        var data = playerData[PhotonNetwork.IsMasterClient ? 0 : 1];
        PhotonNetwork.Instantiate(data.prefabName, data.transform.position, data.transform.rotation);
    }

    #region RPC

    [PunRPC]
    public void StartGame(int randomSeed, PhotonMessageInfo info)
    {
        //Verifica se está no modo "Mock" (simulando o modo online)
        IsMock = FindObjectsOfType<GameMock>().Length >= 1;
        //Registra o horário que começou o jogo
        StartTime = IsMock ? Time.time : info.SentServerTime;

        //Prepara random com seed definida pelo master
        System.Random rnd = new System.Random(randomSeed);

        //Obtém itens da cena
        var escondiveis = new List<HideableItem>(FindObjectsOfType<HideableItem>());
        //Adiciona todos os itens escondiveis na lista
        itemsEscondiveis.AddRange(escondiveis);
        //Verifica se está ok
        if(itemsEscondiveis.Count == 0 || itemsEscondiveis.Count % 2 != 0)
        {
            Debug.LogWarning("Total de itens escondiveis deve ser >0 e par! " + itemsEscondiveis.Count);
            return;
        }
        //Obtém os pontos para esconder os itens
        var spawnPointsEscondidos = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPointHideableItem"));
        if(spawnPointsEscondidos.Count < itemsEscondiveis.Count)
        {
            Debug.LogWarning("SpawnPoints para itens escondidos é menor que o total de itens a esconder! " + itemsEscondiveis.Count + " vs " + spawnPointsEscondidos.Count);
            return;
        }

        //Começa o jogo com metade dos itens perdidos
        int countMetade = itemsEscondiveis.Count / 2;
        while(escondiveis.Count > countMetade)
        {
            int indexItem = rnd.Next(0, escondiveis.Count);
            var item = escondiveis[indexItem];
            item.Lost = true;
            escondiveis.RemoveAt(indexItem);
            itemsPerdidos.Add(item);

            int indexSpawn = rnd.Next(0, spawnPointsEscondidos.Count);
            var spawnPoint = spawnPointsEscondidos[indexSpawn];
            //Coloca item no spawn point
            item.transform.parent = spawnPoint.transform;
            item.transform.localPosition = Vector3.zero;
            //Debug.Log("Aee: " + item.transform.parent);
        }
        itemsEncontrados.AddRange(escondiveis);

        //Trigger event
        IsPlaying = true;
        IsFinished = false;
        callbacks.ForEach(c => c.OnGameStart());
    }

    [PunRPC]
    public void PickHideableItem(HideableItem item, PlayerItemController player)
    {
        if(player.tag != "Human")
        {
            if(!item.Lost && player.itemPicked == null)
            {
                item.transform.parent = player.pickSpawnPoint;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                item.transform.Rotate(item.RotationOnPick);
                player.itemPicked = item;
                item.Picked = true;
            }
        }
        else
        {
            if(item.Lost && player.itemPicked == null)
            {
                item.Lost = false;
                itemsEncontrados.Add(item);
                itemsPerdidos.Remove(item);
                player.itemPicked = item;
                item.Picked = true;

                item.transform.parent = player.pickSpawnPoint;
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                item.transform.Rotate(item.RotationOnPick);
               
            }
        }

        //Trigger event
        callbacks.ForEach(c => c.OnHideableItemPicked(item, player));
    }

    [PunRPC]
    public void DropHideableItem(HideableItem item, PlayerItemController player)
    {
        if (player.tag != "Human")
        {
            if (!item.Lost)
            {
                item.Lost = true;
                itemsPerdidos.Add(item);
                itemsEncontrados.Remove(item);
                player.itemPicked = null;
                item.Picked = false;

                item.transform.parent = null;
                //item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                GameBehaviour.PutOnFloor(item.transform);
            }
        }
        else
        {
            item.transform.parent = null;
            //item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            GameBehaviour.PutOnFloor(item.transform);
            player.itemPicked = null;
            item.Picked = false;

            //if (item.Lost)
            //{
            //TODO: Colocar item na posicao atual do cachorro
            //}
        }

        //Trigger event
        callbacks.ForEach(c => c.OnHideableItemDropped(item, player));
    }

    [PunRPC]
    public void EndGame(string winner)
    {
        IsPlaying = false;
        IsFinished = true;

        //Trigger event
        callbacks.ForEach(c => c.OnGameEnd(winner));
    }
    #endregion

    private void Update()
    {
        if(IsPlaying && IsMasterClient )
        {
            //Debug.Log("Log: " + ElapsedTime + "/" + Duration);
            //Chegou hora de acabar jogo?
            if(ElapsedTime > Duration)
            {
                //Está em prorrogação até encontrarmos um ganhador?
                if(itemsPerdidos.Count != itemsEncontrados.Count)
                {
                    //Define quem ganhou
                    string winner = itemsPerdidos.Count > itemsEncontrados.Count ? "Dog" : "Human";
                    if(IsMock)
                    {
                        EndGame(winner);
                    }
                    else
                    {
                        photonView.RPC("EndGame", RpcTarget.All, winner);
                    }
                }
            }
        }
    }

    public LoadingController LoadController;

    public static IEnumerator ExitGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"[GameManager] Bye Bye!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }

}
