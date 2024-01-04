using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public Stats SantaStats;
    public State SantaState;

    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      SantaState = new State();
      SantaStats = new Stats();
    }
  }
}