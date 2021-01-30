using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemController : GameBehaviour
{
    public Transform pickSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (pickSpawnPoint == null)
        {
            pickSpawnPoint = RecursiveFindChild(transform, "JawEnd_M");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HideableItem")
        {
            if(IsMock)
            {

            }
        }
    }

}
