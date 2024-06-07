using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assembly_CSharp
{
    public class GoldManager : MonoBehaviour
    {
        public int TotalGold { get; private set; }
        
        public TextMeshProUGUI GoldText;

        public delegate void GoldChangedHandler(int newGold);
        public event GoldChangedHandler OnGoldChanged;

        void Start()
        {
            TotalGold = 250;
            OnGoldChanged += UpdateGoldUI; 
            UpdateGoldUI(TotalGold);
        }

        public void AddGold(int gold)
        {
            Debug.Log($"{TotalGold}");
            TotalGold += gold;
            OnGoldChanged?.Invoke(TotalGold); 
        }

        private void UpdateGoldUI(int newGold)
        {
            Debug.Log($"{TotalGold}");
            Debug.Log("Updating Gold UI: " + newGold);
            GoldText.text = "Gold: " + newGold;
        }
        public void RemoveGold(int gold)
        {
            Debug.Log($"{TotalGold}");
            TotalGold -= gold;
            OnGoldChanged?.Invoke(TotalGold);
            Debug.Log($"Remove gold {gold}");
        }

        public int GetGold()
        {
            Debug.Log($"{TotalGold}");
            return TotalGold;
        }

        
    }
}
