using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveByBPM : MonoBehaviour
{
    public Button start;
    public Transform pointA;
    public Transform pointB;
    public float bpm = 100.0f;
    private bool Pause = false;
    private bool Play = false;
    private bool isPlaying = false;
    private Coroutine beatCoroutine;
    private bool Music=false;
    public RectTransform rectTransform;
    public AudioSource audioSource;
    public AudioClip clip;
    public GameObject video;
    public GameObject vp;
    public List<GameObject> Targets;
    float limit = 2460; //;
    private void Start()
    {
        clip.LoadAudioData();
    }

    private void Update()
    {
        if (rectTransform.position.x <= limit)
        {
            //Debug.Log(rectTransform.position.x);
            Music = true;
            PlayHaruhikage();
            limit=-9999;
        }
        if (Play)
        {
            beatCoroutine = StartCoroutine(MoveObject());  // 將協程存儲在變量中
            Debug.Log("Play");
            Play = false;
            isPlaying = true;
        }
        if (Pause)
        {
            StopCoroutine(beatCoroutine);  // 使用變量來停止協程
            Debug.Log("Pause");
            Pause = false;
            isPlaying = false;
        }
    }
    public void PlayHaruhikage()
    {
        if (Music)
        {
            audioSource.PlayOneShot(clip);
            Music = false;
        }
    }

    public void PlayOrPause()
    {
        if (isPlaying)
        {
            Pause = true;
        }
        else
        {
            Play = true;
        }
        start.gameObject.SetActive(false);
    }
    int targetState = 1;
    private IEnumerator MoveObject()
    {
        while (true)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * bpm/3 / 60.0f;
                transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
                yield return null;
            }
            // Swap points
            Transform temp = pointA;
            pointA = pointB;
            pointB = temp;
            try
            {
                Targets[targetState - 1].SetActive(false);
                Targets[targetState].SetActive(true);
            } catch ( Exception e )
            {
                Debug.Log(e.Message);
                Pause = true;
                
                video.SetActive(true);
                vp.SetActive(true);
                this.gameObject.SetActive(false);
            }
            targetState++;
        }
    }
}
