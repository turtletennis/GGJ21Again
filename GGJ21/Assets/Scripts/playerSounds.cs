using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound.PlayerSounds
{

    [RequireComponent(typeof(AudioSource))]

    public class playerSounds : MonoBehaviour
    {
        [SerializeField]
        AudioClip[] footstepSounds;
        [SerializeField]
        float fsPitchMin = 1;
        [SerializeField]
        float fsPitchMax = 1;
        [SerializeField]
        bool fsDoNotRepeat = true;

        [SerializeField]
        AudioClip[] jumpSounds;
        [SerializeField]
        float jumpPitchMin = 1;
        [SerializeField]
        float jumpPitchMax = 1;
        [SerializeField]
        bool jumpDoNotRepeat = true;


        [SerializeField]
        AudioClip[] dBLJumpSounds;
        [SerializeField]
        float dBLJumpPitchMin = 1;
        [SerializeField]
        float dBLJumpPitchMax = 1;
        [SerializeField]
        bool dBLJumpDoNotRepeat = true;

        [SerializeField]
        AudioClip[] landingSounds;
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

        public void PlayFootStepSound()
        {
            if (footstepSounds.Length < 1)
            {
                if (playerSoundsDebug)
                {
                    Debug.Log("foostepSounds[] is empty - RETURN");
                }
                return;
            }

            if (soundEmitter.isPlaying)
            {
                soundEmitter.Stop();
            }

            RadomizePitch(fsPitchMin, fsPitchMax);
            ChooseSound(footstepSounds);
            soundEmitter.PlayOneShot(soundEmitter.clip);
            NoRepeat(fsDoNotRepeat, footstepSounds);

            if (playerSoundsDebug)
            {
                Debug.Log("Play FOOTSTEP sound");
            }
        }

        public void PlayJumpSound()
        {
            if(jumpSounds.Length < 1)
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
            ChooseSound(jumpSounds);
            soundEmitter.PlayOneShot(soundEmitter.clip);
            NoRepeat(jumpDoNotRepeat, jumpSounds);

            if (playerSoundsDebug)
            {
                Debug.Log("Play JUMP sound");
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
            if (soundEmitter.isPlaying)
            {
                soundEmitter.Stop();
            }

            RadomizePitch(landingPitchMin, landingPitchMax);
            ChooseSound(landingSounds);
            soundEmitter.PlayOneShot(soundEmitter.clip);
            NoRepeat(landingDoNotRepeat, landingSounds);

            if (playerSoundsDebug)
            {
                Debug.Log("Play Landing sound");
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
