using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip levelUp;
    [SerializeField] private AudioSource refSource;


    public void PlayAttackAudio()
    {
        PlayAudioClip(attack);   
    }

    public void PlayMoveAudio()
    {
        PlayAudioClip(move);
    }

    public void PlayLevelUp()
    {
        PlayAudioClip(levelUp);
    }

    //オーディオクリップを自分の位置で再生する
    private void PlayAudioClip(AudioClip clip)
    {
        refSource.PlayOneShot(clip);
    }
}