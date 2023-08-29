using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Mayank.AudioRecorder.Utility.Result;
using UnityEngine;
using UnityEngine.Networking;
// using File = UnityEngine.Windows.File;

namespace Mayank.AudioRecorder.Utility
{
    public static class FileReader
    {
        /// <summary>
        /// Loads a WAV file from the specified path and converts it into an audio clip.
        /// </summary>
        /// <param name="filePath">The path of the WAV file.</param>
        /// <returns>The result of loading the audio clip from the file path.</returns>
        public static async UniTask<AudioClipFileReadingResultModel> LoadWavFileAsAudioClip(string filePath)
        {
            // var multimediaWebRequest = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.WAV);
            // await multimediaWebRequest.SendWebRequest();
            // await UniTask.WaitUntil(() => multimediaWebRequest.isDone);
            // var audioClipFileReadingResultModel = new AudioClipFileReadingResultModel();
            //
            // if (multimediaWebRequest.error != null)
            // {
            //     audioClipFileReadingResultModel.status = false;
            //     audioClipFileReadingResultModel.error = multimediaWebRequest.error;
            //     audioClipFileReadingResultModel.result = null;
            // }
            // else
            // {
            //     audioClipFileReadingResultModel.status = true;
            //     audioClipFileReadingResultModel.error = null;
            //     audioClipFileReadingResultModel.result = DownloadHandlerAudioClip.GetContent(multimediaWebRequest);
            // }
            //
            // return audioClipFileReadingResultModel;
            
            
            var audioClipFileReadingResultModel = new AudioClipFileReadingResultModel();
            var loadedAudioClip = await LoadAudioFileAsAudioClip(filePath);
            if (loadedAudioClip.length < 0.1f)
            {
                audioClipFileReadingResultModel.status = false;
                audioClipFileReadingResultModel.error = "I can't load audio file";
                audioClipFileReadingResultModel.result = null;
            }
            else
            {
                audioClipFileReadingResultModel.status = true;
                audioClipFileReadingResultModel.error = null;
                audioClipFileReadingResultModel.result = loadedAudioClip;
            }

            Debug.Log("audioClipFileReadingResultModel.status   :::    "+audioClipFileReadingResultModel.status);
            Debug.Log("loadedAudioClip.length    :::    "+loadedAudioClip.length);
            
            return audioClipFileReadingResultModel;




        }

        // public static async UniTask<AudioClipFileReadingResultModel> LoadAudioFileAsAudioClip(string filePath)
        public static async UniTask<AudioClip> LoadAudioFileAsAudioClip(string filePath)
        {
            await UniTask.Delay(500);
            
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            // var fileBytes = File.ReadAllBytes(filePath);
            // var fileBytes = File.Rea(filePath);
            
            // var fileStream = File.Ope
            
            // using (FileStream fileStream = File.Open)
            // {
            //     
            // }
            
            // var f = File.FileStr
            
            // await UniTask.WaitUntil(() => fileBytes != null);
            
            // var audioClip = AudioClip.Create("LoadedAudio", fileBytes.Length, 1, 48000, false);
            // var audioClip = AudioClip.Create("LoadedAudio", fileBytes.Length, 1, 44100, false);
            
            
            
            
            // return WavUtility.ToAudioClip(filePath);
            return WavUtility.ToAudioClip(fileBytes, 0);
            
            
            //
            //
            //
            // float[] audioData = ConvertByteToFloat(fileBytes);
            //
            //
            //
            // var audioClip = AudioClip.Create("LoadedAudio", audioData.Length, 1, 44100, false);
            // // var audioClip = AudioClip.Create("LoadedAudio", audioData.Length, 1, 48000, false);
            //
            // audioClip.SetData(audioData, 0);
            //
            //
            // return audioClip;
            //
            //
            //
            //
            //
            //
            //
            //
            //
            // // AudioClip audioClip2 = WavUtility.ToAudioClip (path);
            //
            //
            //
            // // WavUtility.FromAudioClip()
            //
            //
            //
            // await UniTask.Delay(500);
            // return WavUtility.ToAudioClip(filePath);

        }
        
        
        
        // private static float[] ConvertByteToFloat(byte[] array) 
        // {
        //     float[] floatArr = new float[array.Length / 4];
        //     for (int i = 0; i < floatArr.Length; i++) 
        //     {
        //         if (BitConverter.IsLittleEndian) 
        //             Array.Reverse(array, i * 4, 4);
        //         
        //         
        //         // floatArr[i] = BitConverter.ToSingle(array, i * 4);
        //         // floatArr[i] = BitConverter.ToSingle(array, i*4) / 0x80000000;
        //         // floatArr[i] = BitConverter.ToInt32(array, i*4) / 0x80000000;
        //         floatArr[i] = BitConverter.ToSingle(array, i*4) / 0x80000000;
        //         // floatArr[i] = BitConverter.ToInt32(array)
        //     }
        //     return floatArr;
        // } 
    }
}