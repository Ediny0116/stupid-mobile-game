using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEbar : MonoBehaviour
{
    [SerializeField] private GameObject BackgroundBar;
    [SerializeField] private GameObject TargetBar;
    [SerializeField] private GameObject SlidingBar;
    private float SlideSpeed;
    [SerializeField] private float SlideTime = 1;
    [SerializeField] private float BPM = 100.0f;
    RectTransform BackgroundRectTrans;
    RectTransform TargetRectTrans;
    RectTransform SlidingRectTrans;
    private float BackgroundWidthToPosLeft;
    private float BackgroundWidthToPosRight;
    private float TargetWidthToPosLeft;
    private float TargetWidthToPosRight;
    private float SlidingWidthToPosLeft;
    private float SlidingWidthToPosRight;
    private bool isSlideForward=true;
    private Vector2 TargetPos;
    private Vector2 SlidingPos;

    private bool play=false;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        BackgroundRectTrans = BackgroundBar.GetComponent<RectTransform>();
        TargetRectTrans = TargetBar.GetComponent<RectTransform>();
        SlidingRectTrans = SlidingBar.GetComponent<RectTransform>();
        SetAllBarWidthToFloat();
        RandomTargetPos();
        SlideTime= 60.0f / BPM;
        SlideSpeed = (BackgroundWidthToPosRight - (SlidingWidthToPosRight*2))/(SlideTime/2);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (play)
        {
            Slide();
        }*/
    }
    public void PlayOrPause()
    {
        play = !play;
    }
    public bool IsHitTarget()
    {
        TargetPos = TargetRectTrans.localPosition;
        SlidingPos = SlidingRectTrans.localPosition;
        if (SlidingPos.x + SlidingWidthToPosLeft >= TargetPos.x + TargetWidthToPosLeft &&
            SlidingPos.x + SlidingWidthToPosRight <= TargetPos.x + TargetWidthToPosRight)
        {
            //UIreset();
            return true;
        }
        else { UIreset(); return false; }  
    }
    void UIreset()
    {
        RandomTargetPos();
        SlidingRectTrans.localPosition =new Vector2(BackgroundWidthToPosLeft - SlidingWidthToPosLeft, SlidingRectTrans.localPosition.y);
    }
    void RandomTargetPos()
    {
        //背景條的左右位置扣除目標條的半寬=目標條不超出背景條的範圍
        //TargetRectTrans.localPosition = new Vector2(Random.Range(BackgroundWidthToPosLeft - TargetWidthToPosLeft, BackgroundWidthToPosRight - TargetWidthToPosRight), BackgroundRectTrans.localPosition.y);
    }
    void SetAllBarWidthToFloat()
    {
        //前後位置=寬度*縮放/2
        BackgroundWidthToPosLeft = -BackgroundRectTrans.rect.width * BackgroundRectTrans.localScale.x / 2;
        TargetWidthToPosLeft = -TargetRectTrans.rect.width * TargetRectTrans.localScale.x / 2;
        SlidingWidthToPosLeft = -SlidingRectTrans.rect.width * SlidingRectTrans.localScale.x / 2;

        BackgroundWidthToPosRight = -BackgroundWidthToPosLeft;
        TargetWidthToPosRight = -TargetWidthToPosLeft;
        SlidingWidthToPosRight = -SlidingWidthToPosLeft;
    }
    void Slide()
    {
        if (SlidingRectTrans.localPosition.x <= BackgroundWidthToPosLeft - SlidingWidthToPosLeft)
        {
            audioSource.Play();
            isSlideForward = true;
        }
        if (SlidingRectTrans.localPosition.x >= BackgroundWidthToPosRight - SlidingWidthToPosRight)
        {
            audioSource.Play();
            isSlideForward = false;
        }
        if (isSlideForward)
        {
            SlidingRectTrans.localPosition = new Vector2(SlidingBar.transform.localPosition.x + SlideSpeed * Time.deltaTime, SlidingBar.transform.localPosition.y);
        }
        if (!isSlideForward)
        {
            SlidingRectTrans.localPosition = new Vector2(SlidingRectTrans.localPosition.x - SlideSpeed * Time.deltaTime, SlidingRectTrans.localPosition.y);
        }
    }
}
