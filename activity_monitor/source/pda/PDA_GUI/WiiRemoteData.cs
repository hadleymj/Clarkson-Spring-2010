using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PDA_GUI
{
    public class WiiRemoteData
    {
        Thread procThread;
        bool procThreadRunning = false;

        public static event EventHandler<EventArgs> StepOccurred;

        /// <summary>
        /// Default constructor. This will instantiate the Stats Queue and the processing Thread.
        /// </summary>
        public WiiRemoteData()
        {
            totalsteps = 0;
            myStats = new Queue<Stats>();
            procThread = new Thread(new ThreadStart(this.processDataLoop));

        }

        /// <summary>
        /// Begin collecting and processing data from the provided WiiRemoteConnection object.
        /// This must only be called once.
        /// </summary>
        /// <param name="conn">The object to collect data from and process.</param>
        public void processData(WiiRemoteConnection conn)
        {
            if (!procThreadRunning)
            {
                wiiConn = conn;
                procThread.Start();
                procThreadRunning = true;
            }

        }


        /// <summary>
        /// This method should only be started in a thread. It is responsible for
        /// collecting data from the WiiRemmoteConnection and procesing it to determine
        /// if the patient is performing a predetermined activity.
        /// </summary>
        protected void processDataLoop()
        {
            RawData data = null;//stop complaining c#
            int[] offset = new int[3];//should change later but yeah
            offset[0] = 0;
            offset[1] = 0;
            offset[2] = 0;

            int thres = 10;
            while (true)//gets all stuff out of the buffer
            {

                data = wiiConn.getData();
                if (data != null)
                    ProcessRawData(data, offset, thres);

                Thread.Sleep(1);
            }


        }
        
        /// <summary>
        /// Process the next data object and trigger the appropriate 
        /// events if processing finds something interesting.
        /// </summary>
        /// <param name="data">The next set of RawData to read.</param>
        /// <param name="offset">The offset to calibrate the WiiRemote Accelerometer readings to zero.</param>
        /// <param name="thres">The processing threshold for a step.</param>
        public void ProcessRawData(RawData data, int[] offset, int thres)
        {
            Console.WriteLine("Accel: {0}", data.time);
            data.accel_x = data.accel_x - offset[0];
            data.accel_y = data.accel_y - offset[1];
            data.accel_z = data.accel_z - offset[2];

            //before it quit last time this was called, it moved the data forward in the buffer
            buff[4] = data;

            for (int i = 0; i < 5; i++)
            {
                if (buff[i] == null)
                {
                    buff[i] = new RawData(0, 0, 0, (long)0); ;
                }
            }


            if (buff[0].accel_z > thres && buff[4].accel_z > thres && (buff[1].accel_z < -1 * thres || buff[2].accel_z < -1 * thres || buff[3].accel_z < -1 * thres))
            {
                int max_cur = Math.Max(buff[0].accel_z, buff[4].accel_z);
                if (buff[1].accel_z < max_cur)
                {
                    buff[1].accel_z = max_cur;
                }
                if (buff[2].accel_z < max_cur)
                {
                    buff[2].accel_z = max_cur;
                }
                if (buff[3].accel_z < max_cur)
                {
                    buff[3].accel_z = max_cur;
                }
            }

            if (buff[0].accel_z < 0)
            {
                buff[0].accel_z = 0;
            }


            //%Second processing cycle on the five samples.
            //%Similar to the first but attempts to remove areas where the accel dropped
            //%below the threshold/2 but is surrounded on either side by values above the
            //%threshold.

            if (buff[0].accel_z >= thres / 2 && buff[4].accel_z >= thres / 2 && (buff[1].accel_z < thres / 2 || buff[2].accel_z < thres / 2 || buff[3].accel_z < thres / 2))
            {
                int max_cur = Math.Max(buff[0].accel_z, buff[4].accel_z);

                if (buff[1].accel_z < max_cur)
                {
                    buff[1].accel_z = max_cur;
                }
                if (buff[2].accel_z < max_cur)
                {
                    buff[2].accel_z = max_cur;
                }
                if (buff[3].accel_z < max_cur)
                {
                    buff[3].accel_z = max_cur;
                }
            }

            //%If the buff point is a value below half the threshold it is set to zero.
            if (buff[0].accel_z < thres / 2)
            {
                buff[0].accel_z = 0;
            }


            //%If there are five zero readings in a row count the next positive reading
            //% as a setp.
            if (zero_count >= 5 && buff[0].accel_z > 0)
            {
                //GUI Update function call.
                if (StepOccurred != null)
                    StepOccurred(this, new EventArgs());

                StepEventTrigger(buff[0].time, true);
                zero_count = 0;
            }
            else
                StepEventTrigger(buff[0].time, false);

            if (buff[0].accel_z == 0)
            {
                zero_count = zero_count + 1;
            }

            //%Pop the first element off of the Array and move all the elements
            //%up one cell.
            for (int i = 0; i < 4; i++)
            {
                buff[i] = buff[i + 1];
            }

        }


        /// <summary>
        /// Called when a step may or may not have occurred.
        /// </summary>
        /// <param name="time">The time this event occured</param>
        /// <param name="step">Whether or not the event is a step.</param>
        private void StepEventTrigger(long time, bool step)
        {

            if (step && (int)(time - lastStep) > 10 * 1000 && step_count == 0)
            {
                lock (myStats)
                {
                    myStats.Enqueue(new WalkingStats(0, lastStep, time));
                }

                firstStep = time;
                lastStep = time;
                step_count = 1;
                totalsteps++;
            }
            else if (!step && step_count > 0 && (int)(time - lastStep) > 10 * 1000)
            {
                lock (myStats)
                {
                    myStats.Enqueue(new WalkingStats(step_count, firstStep, lastStep));
                }
                step_count = 0;
            }
            else if (step)
            {
                Console.WriteLine("Step: {0} - {1}", step_count, time);
                step_count += 1;
                lastStep = time;
                totalsteps++;
            }

        }

        /// <summary>
        /// Return the most recent Stats object if one exists.
        /// </summary>
        /// <returns>The most recent Stats object if one exists, else null.</returns>
        public Stats getStats()
        {
            lock (myStats)
            {
                if (myStats.Count > 0)
                {
                    return myStats.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        public int getTotalSteps()
        {
            return totalsteps;
        }


        private WiiRemoteConnection wiiConn;
        //queue of Stats objects
        private Queue<Stats> myStats;
        private long lastStep, firstStep;
        private RawData[] buff = new RawData[5];
        private int zero_count;
        private int step_count;
        private int totalsteps;


    }
}
