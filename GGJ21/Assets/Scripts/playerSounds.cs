using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.PlayerSounds
{

    [RequireComponent(typeof(AudioSource))]

    public class playerSounds : MonoBehaviour
    {

        [SerializeField] bool ifSand;

        [SerializeField] AudioClip[] beachFoosteps;
        [SerializeField] AudioClip[] mainFootSteps;
        [SerializeField] float fsPitchMin = 1;
        [SerializeField] float fsPitchMax = 1;
        [SerializeField] bool fsDoNotRepeat = true;




        [SerializeField] AudioClip[] beachJumpSounds;
        [SerializeField] AudioClip[] mainJumpSounds;
        [SerializeField] float jumpPitchMin = 1;
        [SerializeField] float jumpPitchMax = 1;
        [SerializeField] bool jumpDoNotRepeat = true;


        [SerializeField] AudioClip[] dBLJumpSounds;
        [SerializeField]
        float dBLJumpPitchMin = 1;
        [SerializeField]
        float dBLJumpPitchMax = 1;
        [SerializeField]
        bool dBLJumpDoNotRepeat = true;

        [SerializeField] AudioClip[] beachLandingSounds;
        [SerializeField] AudioClip[] mainLandingSounds;
        [SerializeField]
        float landingPitchMin = 1;
        [SerializeField]
        float landingPitchMax = 1;
        [SerializeField]
        bool landingDoNotRepeat = true;

        [Space(10)]

        [SerializeField]
        bool playerSoundsDebug;

        private AudioSource soundEmitter;
        private int lastArrayPosition;

        void Start()
        {
            soundEmitter = GetComponent<AudioSource>();

        }

        void Update()
        {

        }

        public void FSEventL()
        {
            PlayFootStepSound();
        }

        public void FSEventR()
        {
            PlayFootStepSound();
        }

        public void PlayFootStepSound()
        {
                PlaySoundPlayerSound(mainFootSteps, beachFoosteps, fsDoNotRepeat, fsPitchMin, fsPitchMax, ifSand);
        }

        public void PlayJumpSound()
        {
            PlaySoundPlayerSound(mainJumpSounds, beachJumpSounds, jumpDoNotRepeat, jumpPitchMin, jumpPitchMax, ifSand);
        }

        public void PlayDoubleJumpSound()
        {
            if (dBLJumpSounds.Length < 1)
            {
                if (playerSoundsDebug)
                {
                    Debug.Log("DBLJumpSounds[] is empty - RETURN");
                }
                return;
            }

            if (soundEmitter.isPlaying)
            {
                soundEmitter.Stop();
            }

            RadomizePitch(dBLJumpPitchMin, dBLJumpPitchMax);
            ChooseSound(dBLJumpSounds);
            soundEmitter.PlayOneShot(soundEmitter.clip);
            NoRepeat(dBLJumpDoNotRepeat, dBLJumpSounds);

            if (playerSoundsDebug)
            {
                Debug.Log("Play DOUBLE JUMP sound");
            }
        }

        public void LandSFXEvent()
        {

            PlaySoundPlayerSound(mainLandingSounds, beachLandingSounds, landingDoNotRepeat, landingPitchMin, landingPitchMax, ifSand);
        }

        public void PlaySoundPlayerSound(AudioClip[] _audioClipArray, AudioClip[] _SandClipArray, bool _doNotRepeat, float _pitchMin, float _pitchMax, bool _ifSand)
        {

            if (!_ifSand)
            {

                if (_audioClipArray.Length < 1)
                {
                    if (playerSoundsDebug)
                    {
                        Debug.Log("jumpSounds[] is empty - RETURN");
                    }
                    return;
                }

                if (soundEmitter.isPlaying)
                {
                    soundEmitter.Stop();
                }

                RadomizePitch(_pitchMin, _pitchMax);
                ChooseSound(_audioClipArray);
                soundEmitter.PlayOneShot(soundEmitter.clip);
                NoRepeat(_doNotRepeat, _audioClipArray);

                if (playerSoundsDebug)
                {
                    Debug.Log("Play JUMP sound");
                }

            }
                else
                {
                    if (_SandClipArray.Length < 1)
                    {
                        if (playerSoundsDebug)
                        {
                            Debug.Log("jumpSounds[] is empty - RETURN");
                        }
                        return;
                    }

                    if (soundEmitter.isPlaying)
                    {
                        soundEmitter.Stop();
                    }

                    RadomizePitch(_pitchMin, _pitchMax);
                    ChooseSound(_SandClipArray);
                    soundEmitter.PlayOneShot(soundEmitter.clip);
                    NoRepeat(_doNotRepeat, _SandClipArray);

                    if (playerSoundsDebug)
                    {
                        Debug.Log("Play JUMP sound");
                    }

                }


        }

        private void RadomizePitch(float PitchMin, float PitchMax)
        {
            float pitch = Random.Range(PitchMin, PitchMax);
            soundEmitter.pitch = pitch;
        }

        private void ChooseSound(AudioClip[] soundArray)
        {

            lastArrayPosition = Random.Range(1, soundArray.Length);
            soundEmitter.clip = soundArray[lastArrayPosition];
        }

        private void NoRepeat(bool noRepeat, AudioClip[] soundArray)
        {
            if (noRepeat)
            {
                soundArray[lastArrayPosition] = soundArray[0];
                soundArray[0] = soundEmitter.clip;
            }
        }
    }

}
