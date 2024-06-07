using Assembly_CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSiteManager : MonoBehaviour
{
    public GameObject buildMenu;
    public TowerManager towerManager;
    public GoldManager goldManager;


    void Start()
    {
        buildMenu.SetActive(false); 
    }
    /// <summary>
    /// Show the menu or not
    /// </summary>
    public void ShowBuildMenu()
    {
        if (gameObject.activeSelf)
        {
            buildMenu.SetActive(!buildMenu.activeSelf);

            if (buildMenu.activeSelf)
            {
                RectTransform rectTransform = buildMenu.GetComponent<RectTransform>();
                rectTransform.position = transform.position + new Vector3(0, rectTransform.sizeDelta.y * 1, 0);
            }
        }
    }
    /// <summary>
    /// Build the archer tower if there s enough gold
    /// </summary>

    public void BuildArcher()
    {
        if(goldManager.TotalGold >= 50)
        {
            goldManager.RemoveGold(50);
            towerManager.SpawnArcher(transform.position);
            DisableSite();
        }
       
    }
    /// <summary>
    /// Build mage tower if there s enough gold
    /// </summary>

    public void BuildMage()
    {
        if (goldManager.TotalGold >= 50)
        {
            goldManager.RemoveGold(50);
            towerManager.SpawnMage(transform.position);
            DisableSite();
        }
    }
    /// <summary>
    /// Build aoe if there s enough gold
    /// </summary>
    public void BuildAoe()
    {
        if (goldManager.TotalGold >= 50)
        {
            goldManager.RemoveGold(50);
            towerManager.SpawnAoe(transform.position);
            DisableSite();
        }
    }

    /// <summary>
    /// Hide the menu
    /// </summary>
    void DisableSite()
    {
        gameObject.SetActive(false);
        buildMenu.SetActive(false);
    }
}
