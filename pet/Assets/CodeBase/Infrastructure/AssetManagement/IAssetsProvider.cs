using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssetsProvider
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
  }
}