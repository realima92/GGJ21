using System;
using TMPro;
using UnityEngine;

public class TVAlertUI: MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TMP_Text alertMessage;

    private string alertRoom;

    private Action OnPrimary;
    private Action OnSecondary;

    public void PlayRoomAlert(string name, Action Ok, Action Cancel)
    {
        alertRoom = name;
        alertMessage.text = $"404 Not Found!\nCreate room\n{alertRoom}?";
        OnPrimary = Ok;
        OnSecondary = Cancel;
        popup.SetActive(true);
    }

    public void AlertOk()
    {
        OnPrimary();
        Close();
    }

    public void AlertCancel()
    {
        OnSecondary();
        Close();
    }

    public void Close()
    {
        popup.SetActive(false);
    }
}