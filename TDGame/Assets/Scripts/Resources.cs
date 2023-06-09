using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Resources : MonoBehaviour
{
    [SerializeField] private UICounter _counter;

    public int Coins { get; private set; }

    public event Action<int> OnChangeCoins;
    public event Action<Vector3> OnCollectCoins;

    private void Start()
    {
        OnChangeCoins?.Invoke(Coins);
    }

    public void CollectCoins(int value, Vector3 worldPosition)
    {
        OnCollectCoins.Invoke(worldPosition);
        StartCoroutine(AddCoinsAfterDelay(value, 1f));
    }

    private IEnumerator AddCoinsAfterDelay(int value, float delay)
    {
        yield return new WaitForSeconds(delay);
        Coins += value;
        OnChangeCoins?.Invoke(Coins);
        _counter.Display();
    }

    public bool Buy(int price)
    {
        if (Coins >= price)
        {
            Coins -= price;
            OnChangeCoins?.Invoke(Coins);
            _counter.Display();
            return true;
        }

        else
        {
            return false;
        }
    }
}
