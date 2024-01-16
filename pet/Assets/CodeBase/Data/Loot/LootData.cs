using System;

namespace CodeBase.Data.Loot
{
  [Serializable]
  public class LootData
  {
    public int Collected;
    public Action Changed;

    public void Collect(Loot loot)
    {
      Collected += loot.Value;
      Changed?.Invoke();
    }
  }
}