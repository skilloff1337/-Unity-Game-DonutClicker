using _0._Localization.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerData : MonoBehaviour
{
    private ILocalizationSystem _localizationSystem;
    public string Name;
    public int Level;
    public int Exp;
    
    [Inject]
    private void Constructor(ILocalizationSystem localizationSystem)
    {
        Debug.Log($"<color=red> Constructor PlayerData </color>");
        _localizationSystem = localizationSystem;
    }
}
