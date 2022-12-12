using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    public WorldGravity worldGravity;

    public int ChangeIndex = 0;




    private void OnTriggerEnter(Collider other)
    {
        worldGravity.SwitchToGravity(other.gameObject, ChangeIndex);
    }
}
