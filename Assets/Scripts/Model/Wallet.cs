using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money;

    public void TryAddMoney(int money)
    {
        if (money > 0)
            _money += money;
    }
}
