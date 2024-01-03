using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public static IInputService InputService;

    public Game()
    {
      RegisterInputInput();
    }

    private static void RegisterInputInput()
    {
      InputService = new StandaloneInputService();
    }
  }
}