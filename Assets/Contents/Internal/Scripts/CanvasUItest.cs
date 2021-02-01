using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUItest : GameBehaviour
{
    public Text placar;
    public Text tempo;
    public Text dropSign;

    // Start is called before the first frame update
    void Awake()
    {
        placar.text = "Finding another player...";
        tempo.text = "Wait a second...";
    }

    // Update is called once per frame
    void Update()
    {
        var gm = GameManager.Instance;
        if(gm.IsPlaying)
        {
            placar.text = "Dog: " + gm.itemsPerdidos.Count + " x " + gm.itemsEncontrados.Count + " :Human";
            if(gm.ElapsedTime < gm.Duration)
            {
                tempo.text = "Time: " + Mathf.RoundToInt((float)(gm.Duration - gm.ElapsedTime)) + "s";
            }
            else
            {
                tempo.text = "Time: Waiting score!";
            }
        }

        dropSign.enabled = gm.player != null && gm.player.itemPicked != null;
        if (dropSign.enabled)
        {
            dropSign.text = "Press shift to drop " + gm.player.itemPicked.gameObject.name;
        }
    }

    public override void OnGameEnd(string winner)
    {
        base.OnGameEnd(winner);
        placar.text = winner + " WINS!";
#if UNITY_EDITOR
        tempo.text = "TIME'S UP! Press <b>Command + F4</b> to exit";
#elif UNITY_STANDALONE_OSX
        tempo.text = "TIME'S UP! Press <b>Command + F4</b> to exit";
#elif UNITY_STANDALONE
        tempo.text = "TIME'S UP! Press <b>Alt + F4</b> to exit";
#endif
    }

}
