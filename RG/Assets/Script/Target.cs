using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject SlidingBar;
    RectTransform TargetRectTrans;
    RectTransform SlidingRectTrans;
    private float SlidingWidthToPosLeft;
    private float SlidingWidthToPosRight;
    private float TargetWidthToPosLeft;
    private float TargetWidthToPosRight;
    private Vector2 TargetPos;
    private Vector2 SlidingPos;
    // Start is called before the first frame update
    void Start()
    {
        SlidingBar = GameObject.Find("SlidingBar");
        SlidingRectTrans = SlidingBar.GetComponent<RectTransform>();
        TargetRectTrans = GetComponent<RectTransform>();
        SetAllBarWidthToFloat();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount>0 &&IsHitTarget())
        {
           this.gameObject.SetActive(false);
        }
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
        else { return false; }
    }
    void SetAllBarWidthToFloat()
    {
        //前後位置=寬度*縮放/2
        TargetWidthToPosLeft = -TargetRectTrans.rect.width * TargetRectTrans.localScale.x / 2;
        SlidingWidthToPosLeft = -SlidingRectTrans.rect.width * SlidingRectTrans.localScale.x / 2;

        TargetWidthToPosRight = -TargetWidthToPosLeft;
        SlidingWidthToPosRight = -SlidingWidthToPosLeft;
    }
}
