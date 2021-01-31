using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableItem : MonoBehaviour
{
    private bool _lost = false;
    public bool Lost {
        get => _lost;
        set {
            _lost = value;
            UpdateSignal();
        }
    }
    private bool _picked = false;
    public bool Picked
    {
        get => _picked;
        set
        {
            _picked = value;
            UpdateSignal();
        }
    }

    public Vector3 RotationOnPick = new Vector3(90.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        UpdateSignal();
        //GameBehaviour.PutOnFloorParent(transform);
    }

    void UpdateSignal()
    {
        Transform trans = GameBehaviour.RecursiveFindChild(transform, "Particle System");
        if(GameManager.Instance != null) trans.gameObject.SetActive(Lost == GameManager.Instance.IsLocalPlayerHuman && !Picked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
