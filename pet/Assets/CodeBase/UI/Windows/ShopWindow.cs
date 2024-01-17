using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows
{
  public class ShopWindow : WindowBase
  {
    [SerializeField] private TextMeshProUGUI _moneyText;

    protected override void Initialize() => 
      RefreshMoneyText();

    protected override void SubscribeUpdates() => 
      Progress.WorldData.LootData.Changed += RefreshMoneyText;

    protected override void Cleanup()
    {
      base.Cleanup();
      Progress.WorldData.LootData.Changed -= RefreshMoneyText;
    }

    private void RefreshMoneyText() => 
      _moneyText.text = Progress.WorldData.LootData.Collected.ToString();
  }
}