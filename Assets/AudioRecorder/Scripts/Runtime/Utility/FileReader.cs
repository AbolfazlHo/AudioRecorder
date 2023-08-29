using System.IO;
using Cysharp.Threading.Tasks;
using Mayank.AudioRecorder.Utility.Result;
using UnityEngine;

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
            var audioClipFileReadingResultModel = new AudioClipFileReadingResultModel();
            // var loadedAudioClip = await LoadAudioFileAsAudioClip(filePath);

            
            await UniTask.Delay(500);
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var loadedAudioClip = WavUtility.ToAudioClip(fileBytes, 0);
            
            
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

            return audioClipFileReadingResultModel;
        }

        // public static async UniTask<AudioClip> LoadAudioFileAsAudioClip(string filePath)
        // {
        //     await UniTask.Delay(500);
        //     var fileBytes = await File.ReadAllBytesAsync(filePath);
        //     return WavUtility.ToAudioClip(fileBytes, 0);
        // }
    }
}