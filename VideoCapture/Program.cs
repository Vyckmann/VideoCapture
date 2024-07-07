using Accord.Video;
using Accord.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VideoCapture
{
     class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (var device in videoDevices)
            {
                Console.WriteLine(device.Name);
                Console.WriteLine(device.MonikerString);
                Console.WriteLine();
            }
           
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            //videoSource.NewFrame += new NewFrameEventHandler(grabFrame);
            //videoSource.SnapshotFrame += new NewFrameEventHandler(grabFrame);

            VideoCapabilities[] caps = videoSource.VideoCapabilities;            
           
            foreach (var cap in caps)
            {
                Console.WriteLine(cap.FrameSize);
                Console.WriteLine(cap.AverageFrameRate);
                Console.WriteLine(cap.BitCount);
                Console.WriteLine(cap.MaximumFrameRate);
                Console.WriteLine();
            }

            VideoCapabilities[] snaps = videoSource.SnapshotCapabilities;
            Console.WriteLine("Snapshot Capabilities: " + snaps.Length);

            videoSource.VideoResolution = caps[2];
            //videoSource.SnapshotResolution = caps[2];
            videoSource.ProvideSnapshots = true;
            videoSource.Start();

            VideoCapabilities res = videoSource.VideoResolution;            
            Console.WriteLine(res.FrameSize);            

            videoSource.SimulateTrigger();

            Console.ReadKey();
            videoSource.SignalToStop();
            
            void grabFrame(object sender, NewFrameEventArgs eventArgs)
            {
                // get new frame
                Bitmap bitmap = eventArgs.Frame;
                bitmap.Save("..\\..\\samples\\" + DateTime.Now.Ticks/1200 + ".png", System.Drawing.Imaging.ImageFormat.Png);                

                // process the frame (you can do anything with it, such as running an
                // image processing filter, saving it to disk, showing on screen, etc)
            }
        }

        private static void VideoSource_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
