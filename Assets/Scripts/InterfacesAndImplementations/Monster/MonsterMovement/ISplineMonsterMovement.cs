using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement
{
    public interface ISplineMonsterMovement
    {
        public void Move(GameObject monster, IMonsterStats monsterStats);
        public void FindSplinePoints(GameObject monster);
    }
}
