using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace JARVIS4
{
    public static class JARVISMedia
    {
        /// <summary>
        /// Function to run audio files
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static StatusObject RunAudioFile(string path)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SoundPlayer audio_player = new SoundPlayer();
                audio_player.SoundLocation = path;
                audio_player.Load();
                audio_player.Play();
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "JARVISMedia_01", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
        public static StatusObject RecordAudioFromMicrophone(string path)
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject RecordAudioFromSystem()
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {

            }
            return SO;
        }
    }
}
