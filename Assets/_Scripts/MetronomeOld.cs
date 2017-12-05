using UnityEngine;

// adapted from Unity documentation sample (Nov 2017) found at:
// https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html

public class MetronomeOld : MonoBehaviour
{
    public AudioClip clipTickBasic;
    public AudioClip clipTickAccent;

    public double bpm = 140.0F;
    public int signatureHi = 4;
    public int signatureLo = 4;

    private double nextTickTime = 0.0F;
    private int beatCount;
    private bool running = false;

    private double beatDuration;

    private AudioSource audioSourceTickBasic;
    private AudioSource audioSourceTickAccent;

    void Awake()
    {
        audioSourceTickBasic = this.CreateAudioSource(clipTickBasic, false);
        audioSourceTickBasic.volume = 0.5F;

        audioSourceTickAccent = this.CreateAudioSource(clipTickAccent, false);
        audioSourceTickAccent.volume = 1.00F;
    }

    void Start()
    {
        // 1 minute = 60 seconds / (numebr of beats per minute)
        beatDuration = 60.0F / bpm;

        beatCount = signatureHi; // so about to do a beat

        double startTick = AudioSettings.dspTime;

        nextTickTime = startTick;
        running = true;

    }


    void Update()
    {
        if (!running)
            return;

        if (IsNearlyTimeForNextTick())
            BeatAction();
    }

    private bool IsNearlyTimeForNextTick()
    {
        float lookAhead = 0.1F;
        if ((AudioSettings.dspTime + lookAhead) >= nextTickTime)
            return true;
        else
            return false;
    }


    private void BeatAction()
    {
        beatCount++;

        // default to no accent
        if (beatCount > signatureHi){
            audioSourceTickAccent.PlayScheduled(nextTickTime);
            beatCount = 1;
            print("-- ACCENT ---");
        } else {
            audioSourceTickBasic.PlayScheduled(nextTickTime);
        }

        nextTickTime += beatDuration;

        print("Tick: " + beatCount + "/" + signatureHi);
    }


}