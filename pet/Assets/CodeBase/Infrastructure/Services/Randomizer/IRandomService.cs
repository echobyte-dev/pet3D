namespace CodeBase.Infrastructure.Services.Randomizer
{
  public interface IRandomService
  {
    int Next(int minValue, int maxValue);
  }
}