using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour {

    [SerializeField]
    ELocations m_location = ELocations.nullLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventManager.Instance.SetLastLocation(m_location);
        }
    }
}
