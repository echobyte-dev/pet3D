using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    
    public GameFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public GameObject CreateSanta(GameObject at) =>
      _assets.Instantiate(AssetPath.PlayerPath, at: at.transform.position);
  }
}