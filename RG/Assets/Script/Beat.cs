using System.Collections;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public AudioSource drumSound;
    public float bpm = 100.0f;
    private bool Pause = false;
    private bool Play = false;
    private bool isPlaying = false;
    private Coroutine drumCoroutine;  // 新增的變量

    private void Start()
    {
        drumSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Play)
        {
            drumCoroutine = StartCoroutine(PlayDrum());  // 將協程存儲在變量中
            Debug.Log("Play");
            Play = false;
            isPlaying = true;
        }
        if (Pause)
        {
            StopCoroutine(drumCoroutine);  // 使用變量來停止協程
            Debug.Log("Pause");
            Pause = false;
            isPlaying = false;
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
    }
    private IEnumerator PlayDrum()
    {
        while (true)
        {
            drumSound.Play();
            yield return new WaitForSeconds(60.0f / bpm);
        }
    }
}