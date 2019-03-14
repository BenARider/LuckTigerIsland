using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInteract : InteractEvent {
    public BoatScript boat;

    public override void Interact(int argID)
    {
        boat.pathID = argID-1;
        boat.OnInteract(PlayerManager.Instance);
    }
}
