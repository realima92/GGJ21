using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsToggle : MonoBehaviour
{
    public AudioGroup group;

    public Image icon;

    public Sprite iconOn;
    public Sprite iconOff;

    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        var dataName = group.ToString() + "On";
        isOn = PlayerPrefs.GetInt(dataName, 1) == 1;
        icon.sprite = isOn ? iconOn : iconOff;
    }

    public void Toggle()
    {
        var dataName = group.ToString() + "On";
        isOn = !isOn;
        PlayerPrefs.SetInt(dataName, isOn ? 1 : 0);
        icon.sprite = isOn ? iconOn : iconOff;
    }
}
