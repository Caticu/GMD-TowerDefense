using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement
{
    public interface ICheckPointMonsterMovement
    {
        
        public event Action<GameObject> OnLastCheckpointReached;
        public int PathId { get; set; }

        
        public void MoveTowardsCheckPoint(GameObject gameObject, float movementSpeed, UnityEngine.Transform[] checkPoint);
       
        public UnityEngine.Transform[] FindCheckPoints(int PathId);
    }
}
