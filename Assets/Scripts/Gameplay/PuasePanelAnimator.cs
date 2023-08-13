using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Animator for pause panel with dotween engine
/// </summary>
public class PuasePanelAnimator : MonoBehaviour
{
    [SerializeField] Image img_Bg;
    [SerializeField] RectTransform rt_ShapeSpawn;
    [SerializeField] RectTransform rt_Graphics;
    [SerializeField] Button btn_Resume;
    [SerializeField] Button btn_Restart;

    [Space(10)]
    [SerializeField] private AnimData animData_Bg;
    [SerializeField] private AnimData animData_ShapeSpawn;
    [SerializeField] private AnimData animData_Graphics;
    [SerializeField] private AnimData animData_Resume;
    [SerializeField] private AnimData animData_Restart;

    private Sequence sequence;
    private void OnEnable()
    {
        Do_Animation(null);
    }


    [ContextMenu("Enable")]
    void TestEnable()
    {
        Do_Animation(null);
    }

    [ContextMenu("Disable")]
    void TestDisable()
    {
        gameObject.SetActive(false);
    }

    public void Do_Animation(System.Action _callback)
    {
        if (sequence != null)
        {
            sequence.Rewind();
            sequence.Kill();
        }

        sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);
        sequence.SetUpdate(true);

        Tween tween_Bg = img_Bg.DOFade(animData_Bg.targetValue, animData_Bg.duration).SetEase(animData_Bg.ease);
        Tween tween_Spawn = rt_ShapeSpawn.DOAnchorPosX(animData_ShapeSpawn.targetValue, animData_ShapeSpawn.duration).SetEase(animData_ShapeSpawn.ease);
        Tween tween_Graphics = rt_Graphics.DOAnchorPosX(animData_Graphics.targetValue, animData_Graphics.duration).SetEase(animData_Graphics.ease);


        Tween tween_Resume = btn_Resume.image.rectTransform.DOAnchorPosX(animData_Resume.targetValue, animData_Resume.duration).SetEase(animData_Resume.ease);
        Tween tween_Restart = btn_Restart.image.rectTransform.DOAnchorPosX(animData_Restart.targetValue, animData_Restart.duration).SetEase(animData_Restart.ease);


        sequence.Insert(animData_Bg.startAt, tween_Bg);
        sequence.Insert(animData_ShapeSpawn.startAt, tween_Spawn);
        sequence.Insert(animData_Graphics.startAt, tween_Graphics);
        sequence.Insert(animData_Resume.startAt, tween_Resume);
        sequence.Insert(animData_Restart.startAt, tween_Restart);


        sequence.OnComplete(() =>
        {
            _callback?.Invoke();
        });

    }

    [System.Serializable]
    struct AnimData
    {
        public float startAt;
        public float targetValue;
        public float duration;
        public Ease ease;
    }
}
