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
    public class MonsterCombat : IMonsterCombat
    {
        public void TakeDamage(BulletInfo bulletInfo, IMonsterStats monsterStats)
        {
            // logic for taking damage
            // Damage formula
            // Effective Physical Damage = (Physical Damage * (100 - Armor)) / 100 + Armor Penetration
            // Effective Magic Damage = (Magic Damage * (100 - Magic Resist)) / 100 + Magic Penetration
            float EffectiveDamageTaken;
            if (bulletInfo.DamageType == Enums.DamageType.Physical)
            {
                EffectiveDamageTaken = (bulletInfo.Damage * (100 - monsterStats.Armor)) / 100 + bulletInfo.ArmorPenetration;
                monsterStats.Hp = monsterStats.Hp - EffectiveDamageTaken;
                Debug.Log($"{monsterStats.Name.ToUpper()} took {EffectiveDamageTaken} damage from {bulletInfo.TowerName}\n Hp left : {monsterStats.Hp}");
            }

            else if (bulletInfo.DamageType == Enums.DamageType.Magic)
            {
                EffectiveDamageTaken = (bulletInfo.Damage * (100 - monsterStats.MagicResist)) / 100 + bulletInfo.MagicPenetration;
                monsterStats.Hp = monsterStats.Hp - EffectiveDamageTaken;
                Debug.Log($"{monsterStats.Name.ToUpper()} took {EffectiveDamageTaken} damage from {bulletInfo.TowerName} \n Hp left : {monsterStats.Hp}");
            }
            else if(bulletInfo.DamageType == Enums.DamageType.Area)
            {
                float effectivePhysicalDamage = 0f;
                float effectiveMagicDamage = 0f;
                float totalEffectiveDamage = 0f;

                // Define a ratio for physical and magic damage distribution
                float physicalDamageRatio = 0.7f; // 70% of damage as physical
                float magicDamageRatio = 1f - physicalDamageRatio; // rest is magic

                // Calculate effective physical damage
                effectivePhysicalDamage = (bulletInfo.Damage * physicalDamageRatio * (100 - monsterStats.Armor)) / 100 + bulletInfo.ArmorPenetration;

                // Calculate effective magic damage
                effectiveMagicDamage = (bulletInfo.Damage * magicDamageRatio * (100 - monsterStats.MagicResist)) / 100 + bulletInfo.MagicPenetration;

                // Total effective damage is magic dmg and phys dmg
                totalEffectiveDamage = effectivePhysicalDamage + effectiveMagicDamage;

                monsterStats.Hp -= totalEffectiveDamage;

                Debug.Log($"{monsterStats.Name.ToUpper()} took {totalEffectiveDamage} damage from {bulletInfo.TowerName}\n Hp left : {monsterStats.Hp}");
            }
        }
    }
}
