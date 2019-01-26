using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowTip : MonoBehaviour
{
    public CanvasGroup tips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Show tips
            ShowTips(tips);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Hide tips
            HideTips(tips);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Show tips
            ShowTips(tips);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Hide tips
            HideTips(tips);
        }
    }

    private void ShowTips(CanvasGroup tipCanvasGroup)
    {
        tipCanvasGroup.alpha = 0;
        DOTween.Kill(tipCanvasGroup);
        Sequence sequence = tipCanvasGroup.DOSequence();
        sequence.Append(tipCanvasGroup.DOFade(1, 2.0f));

        if (transform.gameObject.name == "Wall-Right")
        {
            HideTips(tips);
        }
    }

    private void HideTips(CanvasGroup tipCanvasGroup)
    {
        tipCanvasGroup.alpha = 1;
        DOTween.Kill(tipCanvasGroup);
        Sequence sequence = tipCanvasGroup.DOSequence();
        sequence.Append(tipCanvasGroup.DOFade(0, 2.0f));
    }
}

public static class GameObjExt
{
    public static Sequence DOSequence(this UnityEngine.Object seq)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.target = seq;

        return sequence;
    }
}