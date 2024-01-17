using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UI/UIRoot";
    private readonly IAssetsProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;

    private Transform _uiRoot;

    public UIFactory(IAssetsProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
    }
    
    public void CreateShop()
    {
      var config = _staticData.ForWindow(WindowId.Shop);
      WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
      window.Construct(_progressService);
    }

    public void CreateUIRoot() => 
      _uiRoot = _assets.Instantiate(UIRootPath).transform;
  }
}