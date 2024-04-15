using Assets.Scripts.Bullets;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat
{
    public interface IMonsterCombat
    {
        public void TakeDamage(BulletInfo bulletInfo, IMonsterStats monsterStats);
    }
}
