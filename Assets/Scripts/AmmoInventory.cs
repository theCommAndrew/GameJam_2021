using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class AmmoInventory : MonoBehaviour
{
    [System.Serializable]
    public struct AmmoEntry{
        public AmmoType type;
        public int stock;
        public int maxCapacity;
    }

    [SerializeField]
    List<AmmoEntry> inventory = new List<AmmoEntry>();

    // get current amount of ammo of a given type
    public int GetCurrentStock(AmmoType type){
        return inventory[(int)type].stock;
    }

    // add [amount] ammo to stock, returns amount added
    public int Collect(AmmoType type, int amount){
        AmmoEntry held = inventory[(int)type];
        int collect = Mathf.Min(amount, held.maxCapacity - held.stock);
        held.stock += collect;
        inventory[(int)type] = held;
        return collect;
    }    

    // spend [amount] ammo, returns amount spent    
    public int Spend(AmmoType type, int amount){
        AmmoEntry held = inventory[(int)type];
        int spend = Mathf.Min(amount, held.stock);
        held.stock -= spend;
        inventory[(int)type] = held;
        return spend;
    }
}
