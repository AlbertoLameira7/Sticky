using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NS_Portal
{
    public class Portal : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                // end level
                NS_GameManager.GameManager.currentLevelFinished();
            }
        }
    }
}

