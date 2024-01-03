using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup Curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }

    public void Hide() =>
      StartCoroutine(FadeIb());

    private IEnumerator FadeIb()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }

      gameObject.SetActive(false);
    }
  }
}