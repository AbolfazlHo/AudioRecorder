﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Recorder
{
    /// <summary>
    /// Add this component to a GameObject to Record Mic Input 
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioRecordHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        // /// <summary>
        // /// A flag that represents if the recorder is recording a voice or not.
        // /// </summary>
        // public bool isRecording { get; private set; }


        /// <summary>
        /// Recording Time
        /// </summary>
        public float recordingTime { get; private set; }


        #region Constants &  Static Variables

        /// <summary>
        /// Audio Source to store Microphone Input, An AudioSource Component is required by default
        /// </summary>
        private static AudioSource audioSource;

        /// <summary>
        /// The samples are floats ranging from -1.0f to 1.0f, representing the data in the audio clip
        /// </summary>
        private static float[] samplesData;

        /// <summary>
        /// WAV file header size
        /// </summary>
        private const int HEADER_SIZE = 44;

        #endregion

        #region Editor Exposed Variables

        /// <summary>
        /// Set a keyboard key for saving the Audio File
        /// </summary>
        [Tooltip("Set a keyboard key for saving the Audio File")]
        public KeyCode keyCode;

        /// <summary>
        /// Audio Player Script for Playing Audio Files
        /// </summary>
        [Tooltip("Audio Player Script for Playing Audio Files")]
        public AudioPlayer audioPlayer;

        /// <summary>
        /// Set max duration of the audio file in seconds
        /// </summary>
        [Tooltip("Set max duration of the audio file in seconds")]
        public int timeToRecord = 30;

        /// <summary>
        /// Hold Button to Record
        /// </summary>
        [Tooltip("Press and Hold Record button to Record")]
        public bool holdToRecord = false;

        [SerializeField] private View _recorderView;

        #endregion


        #region MonoBehaviour Callbacks

        private void Start()
        {
            // _recorderView.Init(this);
            // _recorderView.Init();
            AuthorizeMicrophone();

            // Get the AudioSource component
            audioSource = GetComponent<AudioSource>();
            // isRecording = false;
        }

        private static void AuthorizeMicrophone()
        {
            // Check iOS Microphone permission
            if (Application.HasUserAuthorization(UserAuthorization.Microphone)) Debug.Log("Microphone found");
            else
            {
                Debug.Log("Microphone not found");
                // Request iOS Microphone permission
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
            }
        }

        private void Update()
        {
            // if (isRecording)
            if (AudioRecorder.IsRecording)
            {
                AudioRecorder.UpdateRecordingTime();
                recordingTime += Time.deltaTime;
                CheckRecordingTime();
            }
            // else CheckRecordKey();
            CheckRecordKey();
        }

        private void CheckRecordingTime()
        {
            // if (recordingTime >= timeToRecord) StopRecording();
            if (recordingTime >= timeToRecord) StartCoroutine(StopRecording());
        }

        private void CheckRecordKey()
        {
            if (holdToRecord) return;
            if (!Input.GetKeyDown(keyCode)) return;
            // if (isRecording) StopRecording();
            // if (AudioRecorder.IsRecording) StopRecording();
            if (AudioRecorder.IsRecording) StartCoroutine(StopRecording());
            else StartRecording();
        }

        #endregion

        #region Other Functions

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (holdToRecord) return;
            // if (isRecording) StopRecording();
            if (AudioRecorder.IsRecording) StartCoroutine(StopRecording());
            else StartRecording();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (holdToRecord) StartRecording();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (holdToRecord) StartCoroutine(StopRecording());
        }

        #endregion

        #region Recorder Functions

        public void StartRecording()
        {
            // isRecording = true;
            AudioRecorder.StartRecording(audioSource, timeToRecord);
            _recorderView.OnStartRecording();
        }

        public IEnumerator StopRecording(string fileName = "Audio")
        {
            // isRecording = false;
            _recorderView.OnStopRecording();
            var filePath = "";

            yield return new WaitUntil(() =>
            {
                filePath = AudioRecorder.SaveRecording(audioSource, fileName);
                return !string.IsNullOrEmpty(filePath);
            });
            
            _recorderView.OnRecordingSaved($"Audio saved at {filePath}");
        }

        #endregion
    }
}