using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace last
{
    public partial class Form2 : Form
    {
        List<List<int>> ranges;
        List<String> cpuMaxedOutMessages;
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        

     
        public Form2(List<List<int>>ranges,List<String>messages)
        {
            InitializeComponent();
            this.ranges = ranges;
            this.cpuMaxedOutMessages = messages;

            this.Width = 50;
            this.Height = 100;

            #region My Performance Counters
            perfCpuCount.NextValue();
            #endregion
            timer1.Interval = 6000;
            timer1.Start();

           
        }

        public List<String> getPhrasesFromFile() {
            List<String> phrases = new List<String>;
            // Read the file and display it line by line.
            string path = System.Environment.CurrentDirectory;
            path = path + "//jarvisphrase.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            String line;

            while ((line = file.ReadLine()) != null)
            {
                phrases.Add(line);
            }
            return phrases;
        }

        /// <summary>
        /// Speaks with a selected voice
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voiceGender"></param>
        public static void JarvisSpeak(string message, VoiceGender voiceGender)
        {
            synth.SelectVoiceByHints(voiceGender);
            synth.Speak(message);
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            
                // Get the current performance counter values
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
             

          
            #region Logic
            int i = percentageExist(currentCpuPercentage);
                if (i != -1)
                {
                    
                        string cpuLoadVocalMessage = cpuMaxedOutMessages[i];

                        JarvisSpeak(cpuLoadVocalMessage, VoiceGender.Male);
       
                }
             #endregion
                Thread.Sleep(100);
             // end of loop
        }

        private int percentageExist(int n)
        {
            for(int i=0;i<ranges.Count;i++)
            {
                if (n >= ranges[i][0] && n <= ranges[i][1])
                    return i;
            }
            return -1;
        }

     

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 100);
        }
    }
}


