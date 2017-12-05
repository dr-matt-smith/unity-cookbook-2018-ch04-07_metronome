using UnityEngine;

// adapted to use AudioSources with clips
// from Unity documentation sample (Nov 2017) found at:
// https://docs.unity3d.com/ScriptReference/AudioSettings-dspTime.html
public class Metronome : MonoBehaviour
{
    public AudioSource audioSourceTickBasic;
    public AudioSource audioSourceTickAccent;

    public double bpm = 140.0F;
    public int signatureHi = 4;
    public int signatureLo = 4;

    private double nextTickTime = 0.0F;
    private int beatCount;
    private double beatDuration;

    /// <summary>
    /// calc duration of each beat (based on 'bpm')
    /// initialise the beat count (so first beat is an accented beat)
    /// set time to next tick is NOW!
    /// </summary>
    void Start()
    {
        // 1 minute = 60 seconds / (numebr of beats per minute)
        beatDuration = 60.0F / bpm;

        beatCount = signatureHi; // so about to do a beat

        double startTick = AudioSettings.dspTime;
        nextTickTime = startTick;
    }

    /// <summary>
    /// if close to time for next tick invoke BeatAction()
    /// </summary>
    void Update()
    {
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