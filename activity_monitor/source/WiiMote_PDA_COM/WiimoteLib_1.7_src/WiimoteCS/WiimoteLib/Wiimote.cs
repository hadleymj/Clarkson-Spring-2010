//////////////////////////////////////////////////////////////////////////////////
//	Wiimote.cs
//	Managed Wiimote Library
//	Written by Brian Peek (http://www.brianpeek.com/)
//	for MSDN's Coding4Fun (http://msdn.microsoft.com/coding4fun/)
//	Visit http://blogs.msdn.com/coding4fun/archive/2007/03/14/1879033.aspx
//  and http://www.codeplex.com/WiimoteLib
//	for more information
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace WiimoteLib
{
	/// <summary>
	/// Implementation of Wiimote
	/// </summary>
	public class Wiimote : IDisposable
	{

		// VID = Nintendo, PID = Wiimote
		private const int VID = 0x057e;
		private const int PID = 0x0306;

		// sure, we could find this out the hard way using HID, but trust me, it's 22
		private const int REPORT_LENGTH = 22;

		// read/write handle to the device
		private SafeFileHandle wiiHandle;

        // read/write handle to the PDA
        private SafeFileHandle pdaHandle;

		// a pretty .NET stream to read/write from/to
		private FileStream wiiStream;

        //FileStream to read/write to the PDA
        private FileStream pdaStream;

        //Byte array to hold read data for the PDA
        private byte[] pdaReadBuff;

        //Byte array to hold write data for the PDA
        private byte[] pdaWriteBuff;

		// current state of controller
		private readonly WiimoteState mWiimoteState = new WiimoteState();

		// HID device path of this Wiimote
		private string wiiDevicePath = string.Empty;

		// unique ID
		private readonly Guid wiiID = Guid.NewGuid();

		// delegate used for enumerating found Wiimotes
		internal delegate bool WiimoteFoundDelegate(string devicePath);

		// kilograms to pounds
		private const float KG2LB = 2.20462262f;

		/// <summary>
		/// Default constructor
		/// </summary>
		public Wiimote()
		{
		}

		internal Wiimote(string devicePath)
		{
			wiiDevicePath = devicePath;
		}

		/// <summary>
		/// Connect to the first-found Wiimote
		/// </summary>
		/// <exception cref="WiimoteNotFoundException">Wiimote not found in HID device list</exception>
		public void Connect()
		{
			if(string.IsNullOrEmpty(wiiDevicePath))
				FindWiimote(WiimoteFound);
			else
				OpenWiimoteDeviceHandle(wiiDevicePath);
		}

		internal static void FindWiimote(WiimoteFoundDelegate wiimoteFound)
		{
			int index = 0;
			bool found = false;
			Guid guid;
			SafeFileHandle wiiHandle;

			// get the GUID of the HID class
			HIDImports.HidD_GetHidGuid(out guid);

			// get a handle to all devices that are part of the HID class
			// Fun fact:  DIGCF_PRESENT worked on my machine just fine.  I reinstalled Vista, and now it no longer finds the Wiimote with that parameter enabled...
			IntPtr hDevInfo = HIDImports.SetupDiGetClassDevs(ref guid, null, IntPtr.Zero, HIDImports.DIGCF_DEVICEINTERFACE);// | HIDImports.DIGCF_PRESENT);

			// create a new interface data struct and initialize its size
			HIDImports.SP_DEVICE_INTERFACE_DATA diData = new HIDImports.SP_DEVICE_INTERFACE_DATA();
			diData.cbSize = Marshal.SizeOf(diData);

			// get a device interface to a single device (enumerate all devices)
			while(HIDImports.SetupDiEnumDeviceInterfaces(hDevInfo, IntPtr.Zero, ref guid, index, ref diData))
			{
				UInt32 size;

				// get the buffer size for this device detail instance (returned in the size parameter)
				HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, IntPtr.Zero, 0, out size, IntPtr.Zero);

				// create a detail struct and set its size
				HIDImports.SP_DEVICE_INTERFACE_DETAIL_DATA diDetail = new HIDImports.SP_DEVICE_INTERFACE_DETAIL_DATA();

				// yeah, yeah...well, see, on Win x86, cbSize must be 5 for some reason.  On x64, apparently 8 is what it wants.
				// someday I should figure this out.  Thanks to Paul Miller on this...
				diDetail.cbSize = (uint)(IntPtr.Size == 8 ? 8 : 5);

				// actually get the detail struct
				if(HIDImports.SetupDiGetDeviceInterfaceDetail(hDevInfo, ref diData, ref diDetail, size, out size, IntPtr.Zero))
				{
					Debug.WriteLine(string.Format("{0}: {1} - {2}", index, diDetail.DevicePath, Marshal.GetLastWin32Error()));

					// open a read/write handle to our device using the DevicePath returned
					wiiHandle = HIDImports.CreateFile(diDetail.DevicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero);

					// create an attributes struct and initialize the size
					HIDImports.HIDD_ATTRIBUTES attrib = new HIDImports.HIDD_ATTRIBUTES();
					attrib.Size = Marshal.SizeOf(attrib);

					// get the attributes of the current device
					if(HIDImports.HidD_GetAttributes(wiiHandle.DangerousGetHandle(), ref attrib))
					{
						// if the vendor and product IDs match up
						if(attrib.VendorID == VID && attrib.ProductID == PID)
						{
							// it's a Wiimote
							Debug.WriteLine("Found one!");
							found = true;

							// fire the callback function...if the callee doesn't care about more Wiimotes, break out
							if(!wiimoteFound(diDetail.DevicePath))
								break;
						}
					}
					wiiHandle.Close();
				}
				else
				{
					// failed to get the detail struct
					throw new WiimoteException("SetupDiGetDeviceInterfaceDetail failed on index " + index);
				}

				// move to the next device
				index++;
			}

			// clean up our list
			HIDImports.SetupDiDestroyDeviceInfoList(hDevInfo);

			// if we didn't find a Wiimote, throw an exception
			if(!found)
				throw new WiimoteNotFoundException("No Wiimotes found in HID device list.");
		}

		private bool WiimoteFound(string devicePath)
		{
			wiiDevicePath = devicePath;

			// if we didn't find a Wiimote, throw an exception
			OpenWiimoteDeviceHandle(wiiDevicePath);

			return false;
		}

		private void OpenWiimoteDeviceHandle(string devicePath)
		{
			// open a read/write handle to our device using the DevicePath returned
			wiiHandle = HIDImports.CreateFile(devicePath, FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero);

            // open a read/write handle to the PDA 
            pdaHandle = HIDImports.CreateFile("\\\\.\\COM20", FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, HIDImports.EFileAttributes.Overlapped, IntPtr.Zero);

			// create an attributes struct and initialize the size
			HIDImports.HIDD_ATTRIBUTES attrib = new HIDImports.HIDD_ATTRIBUTES();
			attrib.Size = Marshal.SizeOf(attrib);

			// get the attributes of the current device
			if(HIDImports.HidD_GetAttributes(wiiHandle.DangerousGetHandle(), ref attrib))
			{
				// if the vendor and product IDs match up
				if(attrib.VendorID == VID && attrib.ProductID == PID)
				{
					// create a nice .NET FileStream wrapping the handle above
					wiiStream = new FileStream(wiiHandle, FileAccess.ReadWrite, REPORT_LENGTH, true);

                    // create a FileStream wrapping the pdaHandle
                    pdaStream = new FileStream(pdaHandle, FileAccess.ReadWrite, REPORT_LENGTH, true);

					// Loop and wait for the PDA to write bytes
					PDA_ReadWriteLoop();

				}
				else
				{
					// otherwise this isn't the controller, so close up the file handle
					wiiHandle.Close();				
					throw new WiimoteException("Attempted to open a non-Wiimote device.");
				}
			}
		}
        ///<summary>
        /// Loop used to relay PDA reports to and from the wii Remote
        ///</summary>
        public void PDA_ReadWriteLoop()
        {
            int i;
            int read; 
            BeginWiiRead();
            while (true)
            {
                if (pdaStream != null && wiiStream != null && pdaStream.CanRead && wiiStream.CanRead)
                {
                    pdaReadBuff = new byte[REPORT_LENGTH];
                    read = pdaStream.Read(pdaReadBuff, 0, REPORT_LENGTH);
                    if (read != 0)
                    {
                        
                        for (i = 0; i < 22; i++)
                        {
                            if (pdaReadBuff[i] != 255)
                            {
                                break;
                            }
                        }

                        if (i == 22)
                        {
                            pdaStream.Close();
                            Disconnect();
                            Environment.Exit(1);

                        }
                        else
                        {
                            wiiStream.Write(pdaReadBuff, 0, REPORT_LENGTH);
                        }
                    }
                }

            }
        }

        public void BeginWiiRead()
        {
            pdaWriteBuff = new byte[REPORT_LENGTH];
            if(wiiStream.CanRead)
                wiiStream.BeginRead(pdaWriteBuff, 0, REPORT_LENGTH, new AsyncCallback(OnWiiRead), pdaWriteBuff);
        }
            

        public void OnWiiRead(IAsyncResult ir)
        {
    
                pdaStream.Write(pdaWriteBuff, 0, REPORT_LENGTH);
         
                //Start reading some more
                BeginWiiRead();
        }
		/// <summary>
		/// Disconnect from the controller and stop reading data from it
		/// </summary>
		public void Disconnect()
		{
			// close up the stream and handle
			if(wiiStream != null)
				wiiStream.Close();

			if(wiiHandle != null)
				wiiHandle.Close();
		}


		/// <summary>
		/// Current Wiimote state
		/// </summary>
		public WiimoteState WiimoteState
		{
			get { return mWiimoteState; }
		}

		///<summary>
		/// Unique identifier for this Wiimote (not persisted across application instances)
		///</summary>
		public Guid ID
		{
			get { return wiiID; }
		}

		/// <summary>
		/// HID device path for this Wiimote (valid until Wiimote is disconnected)
		/// </summary>
		public string HIDDevicePath
		{
			get { return wiiDevicePath; }
		}

		#region IDisposable Members

		/// <summary>
		/// Dispose Wiimote
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose wiimote
		/// </summary>
		/// <param name="disposing">Disposing?</param>
		protected virtual void Dispose(bool disposing)
		{
			// close up our handles
			if(disposing)
				Disconnect();
		}
		#endregion
	}

	/// <summary>
	/// Thrown when no Wiimotes are found in the HID device list
	/// </summary>
	[Serializable]
	public class WiimoteNotFoundException : ApplicationException
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public WiimoteNotFoundException()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Error message</param>
		public WiimoteNotFoundException(string message) : base(message)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="innerException">Inner exception</param>
		public WiimoteNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="info">Serialization info</param>
		/// <param name="context">Streaming context</param>
		protected WiimoteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}

	/// <summary>
	/// Represents errors that occur during the execution of the Wiimote library
	/// </summary>
	[Serializable]
	public class WiimoteException : ApplicationException
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public WiimoteException()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Error message</param>
		public WiimoteException(string message) : base(message)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="innerException">Inner exception</param>
		public WiimoteException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="info">Serialization info</param>
		/// <param name="context">Streaming context</param>
		protected WiimoteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}