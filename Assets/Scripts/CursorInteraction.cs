using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; 

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.CompareTag("TowerSite"))
                {
                    Debug.Log("Tower Site Clicked!");
                }

                else if (hit.collider.CompareTag("Tower"))
                {
                    Debug.Log("Tower Clicked!");
                }
            }
        }
    }
}
