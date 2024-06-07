using Assembly_CSharp;
using Assets.ObjectPooling;
using Assets.Scripts.InterfacesAndImplementations.Tower;
using Assets.Scripts.Towers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    
    public GameObject archerPrefab;
    public GameObject magePrefab;
    public GameObject aoePrefab;
    public ObjectPool archerPool;
    public ObjectPool magePool;
    public ObjectPool aoePool;
    public GoldManager goldManager;
    void Start()
    {
        archerPool.prefab = archerPrefab;
        magePool.prefab = magePrefab;
        aoePool.prefab = aoePrefab;

        archerPool.Initialize(10); 
        magePool.Initialize(10);   
        aoePool.Initialize(10);    
    }

    public GameObject SpawnArcher(Vector3 position)
    {
        GameObject archer = archerPool.Get();
        archer.transform.position = position;
        InitializeArcher(archer);
        return archer;
    }

    public GameObject SpawnMage(Vector3 position)
    {
        GameObject mage = magePool.Get();
        mage.transform.position = position;
        InitializeMage(mage);
        return mage;
    }

    public GameObject SpawnAoe(Vector3 position)
    {
        GameObject aoe = aoePool.Get();
        aoe.transform.position = position;
        InitializeAoe(aoe);
        return aoe;
    }

    private void InitializeArcher(GameObject archer)
    {
        Archer archerComponent = archer.GetComponent<Archer>();
        archerComponent.InitializeArcherTower(new TowerStats(), new TowerLevel(), new TowerShoot(), goldManager);
    }

    private void InitializeMage(GameObject mage)
    {
        Mage mageComponent = mage.GetComponent<Mage>();
        mageComponent.InitializeMageTower(new TowerStats(), new TowerLevel(), new TowerShoot(), goldManager);
    }

    private void InitializeAoe(GameObject aoe)
    {
        Aoe aoeComponent = aoe.GetComponent<Aoe>();
        aoeComponent.InitializeAoeTower(new TowerStats(), new TowerLevel(), new TowerShoot(), goldManager);
    }
}