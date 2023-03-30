using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int Money { get; private set; }

    public void AddMoney(int reward)
    {
        if(reward > 0)
        {
            Money += reward;
        }
    }
}
