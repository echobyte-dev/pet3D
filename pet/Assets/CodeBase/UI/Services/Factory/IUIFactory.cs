using System.Threading.Tasks;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory
  {
    void CreateShop();
    Task CreateUIRoot();
  }
}