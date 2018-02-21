namespace GameForm.framework
{
    public class Sounds
    {
        static WMPLib.WindowsMediaPlayer jump = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer die = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer intro = new WMPLib.WindowsMediaPlayer();
        static WMPLib.WindowsMediaPlayer lobby = new WMPLib.WindowsMediaPlayer();
        static Sounds()
        {
            jump.URL = "JSound.mp3";
            die.URL = "death.mp3";
            intro.URL = "DASBOSS.mp3";
            lobby.URL = "intro.mp3";
        }
        

        public static void JSoundPlay()  // Play Jump Sound
        {


            jump.controls.play();
        }

        public static void JSoundStop() // Stop Jump Sound
        {
            //WMPLib.WindowsMediaPlayer intro = new WMPLib.WindowsMediaPlayer();
            
            jump.controls.stop();
        }

        public static void DSoundPlay() // Play Death Sound
        {
            
            
            die.controls.play();
        }


        public static void GameSoundPlay() // Play Background Music Sound
        {
            
            
            intro.controls.play();

        }

        public static void GameSoundStop()// Stop Background Music Sound
        {
            intro.controls.stop();
        }

        public static void LobbySoundPlay() // Play Lobby Music
        {
       

            lobby.controls.play();

        }
        public static void LobbySoundStop() // Stop Lobby Music
        {
           lobby.controls.stop();

        }
    }
}