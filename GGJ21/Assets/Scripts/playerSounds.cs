using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.PlayerSounds
{

    [RequireComponent(typeof(AudioSource))]

    public class playerSounds : MonoBehaviour
    {

        [SerializeField] bool ifSand = false;

        [SerializeField] AudioClip[] beachFoosteps = null;
        [SerializeField] AudioClip[] mainFootSteps = null;
        [SerializeField] float fsPitchMin = 1;
        [SerializeField] float fsPitchMax = 1;
        [SerializeField] bool fsDoNotRepeat = true;
        [SerializeField] float fsVolume = 0.2f;



        [SerializeField] AudioClip[] beachJumpSounds = null;
        [SerializeField] AudioClip[] mainJumpSounds = null;
        [SerializeField] float jumpPitchMin = 1;
        [SerializeField] float jumpPitchMax = 1;
        [SerializeField] bool jumpDoNotRepeat = true;


        [SerializeField]
        AudioClip[] dBLJumpSounds = null;
        [SerializeField]
        float dBLJumpPitchMin = 1;
        [SerializeField]
        float dBLJumpPitchMax = 1;
        [SerializeField]
        bool dBLJumpDoNotRepeat = true;

        [SerializeField] AudioClip[] beachLandingSounds = null;
        [SerializeField] AudioClip[] mainLandingSounds = null;
        [SerializeField]
        float landingPitchMin = 1;
        [SerializeField]
        float landingPitchMax = 1;
        [SerializeField]
        bool landingDoNotRepeat = true;

        [Space(10)]

        [SerializeField] bool playerSoundsDebug = false;

        private AudioSource soundEmitter;
        private int lastArrayPosition;

        void Start()
        {
            soundEmitter = GetComponent<AudioSource>();
           
        }

        void Update()
        {

        }

        public void PlayFootStepSound()
        {
            PlaySFX(mainFootSteps , beachFoosteps , fsPitchMin , fsPitchMax , fsDoNotRepeat , ifSand);
        }

        public void PlayJumpSound()
        {
            PlayJumpSound(ifSand);
        }

        private void PlayJumpSound(bool _ifSand)
        {

            if (!_ifSand)
            {

                if(mainJumpSounds.Length < 1)
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

                RadomizePitch(jumpPitchMin, jumpPitchMax);
                ChooseSound(mainJumpSounds);
                soundEmitter.PlayOneShot(soundEmitter.clip);
                NoRepeat(jumpDoNotRepeat, mainJumpSounds);

                if (playerSoundsDebug)
                {
                    Debug.Log("Play JUMP sound");
                }

                else
                {
                    if (beachJumpSounds.Length < 1)
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

                    RadomizePitch(jumpPitchMin, jumpPitchMax);
                    ChooseSound(beachJumpSounds);
                    soundEmitter.PlayOneShot(soundEmitter.clip);
                    NoRepeat(jumpDoNotRepeat, beachJumpSounds);

                    if (playerSoundsDebug)
                    {
                        Debug.Log("Play JUMP sound");
                    }

                }
            }
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

        public void PlayLandingSound()
        {
            PlaySFX(mainLandingSounds, beachLandingSounds, landingPitchMin, landingPitchMax, landingDoNotRepeat, ifSand);
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

        public void FSEventL()
        {
            PlayFootStepSound();
            //Debug.Log("footstep triggered");
        }

        public void FSEventR()
        {
            PlayFootStepSound();
            //Debug.Log("footstep triggered");
        }

        public void LandSFXEvent()
        {

            PlayLandingSound();
        }

        public void PlaySFX(AudioClip[] _mainAudioClipArray , AudioClip[] _sandAudioClipArray , float _pitchMin , float _pitchMax , bool _doNotRepeat , bool _isSand)
        {
            if (!_isSand)
            {

                if (_mainAudioClipArray.Length < 1)
                {
                    if (playerSoundsDebug)
                    {
                        Debug.Log("mainFootSteps[] is empty - RETURN");
                    }
                    return;
                }

                if (soundEmitter.isPlaying)
                {
                    soundEmitter.Stop();
                }

                RadomizePitch(_pitchMin, _pitchMax);
                ChooseSound(_mainAudioClipArray);
                soundEmitter.PlayOneShot(soundEmitter.clip);
                NoRepeat(_doNotRepeat , _mainAudioClipArray);

                if (playerSoundsDebug)
                {
                    Debug.Log("Play FOOTSTEP sound");
                }
            }

            else
            {
                if (_sandAudioClipArray.Length < 1)
                {
                    if (playerSoundsDebug)
                    {
                        Debug.Log("beachFoosteps[] is empty - RETURN");
                    }
                    return;
                }

                if (soundEmitter.isPlaying)
                {
                    soundEmitter.Stop();
                }

                RadomizePitch(_pitchMin, _pitchMax);
                ChooseSound(_sandAudioClipArray);
                soundEmitter.volume = fsVolume;
                soundEmitter.PlayOneShot(soundEmitter.clip);
                NoRepeat(_doNotRepeat , _sandAudioClipArray);

                if (playerSoundsDebug)
                {
                    Debug.Log("Play FOOTSTEP sound");
                }
            }
        }
    }


}
