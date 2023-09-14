using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _occupieSound;
    [SerializeField] private AudioClip _moneySound;
    [Space(10), SerializeField] private AudioSource _cellOccupieSoundSource;
    [SerializeField] private AudioSource _moneySoundSource;

    public void PlayMoneySound() => _moneySoundSource.PlayOneShot(_moneySound);
    public void PlayOccupieSound() => _cellOccupieSoundSource.PlayOneShot(_occupieSound);
}
