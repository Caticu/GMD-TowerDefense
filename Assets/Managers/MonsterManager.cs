using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using Assets.Scripts.Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Wolf wolfPrefab;

    private IMonsterCombat monsterCombat = new MonsterCombat();
    private ICheckPointMonsterMovement monsterMovement = new CheckPointMonsterMovement();
    private IMonsterStats monsterStats = new MonsterStats();
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        // Check if the wolf prefab is assigned
        if (wolfPrefab != null)
        {
            // Instantiate a wolf object at the specified position and rotation
            Wolf wolfInstance = Instantiate(wolfPrefab, new Vector3(-245f, 99.9f, 0f), Quaternion.identity);
            wolfInstance.GetComponent<Wolf>().InitializeWolf(monsterCombat, monsterMovement, monsterStats, 1);
        }
        else
        {
            Debug.LogError("Wolf prefab is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
