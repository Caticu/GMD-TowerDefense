using Assets.Scripts.InterfacesAndImplementations.Tower;
using Assets.Scripts.Towers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Archer Archer;

    private ITowerLevel TowerLevel = new TowerLevel();
    private ITowerShoot TowerShoot = new TowerShoot();
    private ITowerStats TowerStats = new TowerStats();


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (Archer != null)
        {
            Archer archerInstance = Instantiate(Archer, new Vector2(-185.199997f, 44.9000015f), Quaternion.identity);
            archerInstance.GetComponent<Archer>().InitializeTower(TowerStats, TowerLevel, TowerShoot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
