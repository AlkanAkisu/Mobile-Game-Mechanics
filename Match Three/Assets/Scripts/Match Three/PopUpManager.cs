using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private PopUpEvent popUpPopper;
    [SerializeField] private GameObject popUpPrefab;
    [SerializeField] private float matchScaler;
    [SerializeField] private IntEvent UpdateUI;


    private void OnEnable()
    {
        popUpPopper.RegisterListener(CreatePopUp);
    }


    private void OnDisable()
    {
        popUpPopper.UnregisterListener(CreatePopUp);
    }

    private void CreatePopUp((Vector3, int) val)
    {
        var (insPint, match) = val;
        var popUp = Instantiate(
            popUpPrefab,
            Camera.main.WorldToScreenPoint(insPint),
            Quaternion.identity,
            FindObjectOfType<CanvasScaler>().transform);

        popUp.GetComponent<TMP_Text>().text = $"x{match}";
        popUp.GetComponent<RectTransform>().localScale = popUp.GetComponent<RectTransform>().localScale * 0.5f;
        AnimatePopUp(popUp.GetComponent<RectTransform>(), match);
    }

    private void AnimatePopUp(RectTransform popUp, int match)
    {
        popUp
            .DOScale(popUp.localScale * (match / 3f) * matchScaler, 0.2f);
        popUp
            .DOMove(popUp.position + Vector3.up * 100f, 0.2f);
        FadeAwayPopUp(popUp, match);
    }

    // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":867.0,"y":1578.0,"z":0.0}
    // 
    private void FadeAwayPopUp(RectTransform popUp, int match)
    {
        var target = popUp.GetComponent<TMP_Text>().color;
        target.a = 0.5f;

        DOTween.To
            (
                () => popUp.GetComponent<TMP_Text>().color,
                val => popUp.GetComponent<TMP_Text>().color = val,
                target,
                0.2f
            )
            .SetDelay(0.4f);

        popUp.DOAnchorPos
            (
                new Vector2(319, 624),
                0.6f
            )
            .SetDelay(0.4f)
            .OnComplete(
                () =>
                {
                    UpdateUI.Raise(match);
                    Destroy(popUp.gameObject);
                }
            );

        popUp
            .DOScale(Vector3.one * 1f, 0.2f)
            .SetDelay(0.3f);
    }
}