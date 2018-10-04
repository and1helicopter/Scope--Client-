using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Xml.Linq;
using System.Xml;
using System.Threading;


namespace UniSerialPort
{
	public class AsynchSerialPort
	{

		public delegate void DataRecieved(bool DataOk, byte[] RxBuffer);
		public delegate void DataRecievedRTU(bool DataOk, ushort[] ParamRTU, object param);

		public bool portError { get; private set; }//= false;
		public event EventHandler SerialPortError;
		public event EventHandler FatalSerialPortError;

		SerialPort serialPort;
		//System.Windows.Forms.Timer 

		System.Timers.Timer requestTimer;

		byte slaveAddr = 1;
		public byte SlaveAddr
		{
			get { return slaveAddr; }
			set { slaveAddr = value; }
		}

		public int BaudRate
		{
			get { return serialPort.BaudRate; }
			set { serialPort.BaudRate = value; }
		}

		public Parity Parity
		{
			get { return serialPort.Parity; }
			set { serialPort.Parity = value; }
		}

		public StopBits StopBits
		{
			get { return serialPort.StopBits; }
			set { serialPort.StopBits = value; }
		}

		public string PortName
		{
			get { return serialPort.PortName; }
			set { serialPort.PortName = value; }
		}

		public bool IsOpen
		{

			get 
			{
				if (serialPortMode == SerialPortModes.RSMode)
				{
					return serialPort.IsOpen;
				}
				else
				{
					if (tcpMaster == null) { return false; }
					return tcpMaster.Connected;
				}
			
			}
		}

		bool portBusy = false;
		public bool PortBusy
		{
			get { return portBusy; }
		}

		public void UnsetPortBusy()
		{
			portBusy = false;
		}

		string ipAddress = "213.21.27.140";
		public string IpAddress
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}


		ushort portNum = 502;
		public ushort PortNum
		{
			get { return portNum; }
			set { portNum = value; }
		}


		SerialPortModes serialPortMode = SerialPortModes.RSMode;
		public SerialPortModes SerialPortMode
		{
			get { return serialPortMode; }
			set { serialPortMode = value; }
		}
		ModbusTCPMaster tcpMaster;

		byte[] tcpReadData;
		bool tcpWriteOk;

		public void SetNewPortMode(SerialPortModes SerialPortMode)
		{
			if (IsOpen)
			{
				throw new Exception("Serial port is open!");
			}

			if (SerialPortMode == SerialPortModes.RSMode)
			{
				serialPortMode = SerialPortModes.RSMode;
				if (tcpMaster != null)
				{
					tcpMaster = null;
				}
			}
			else
			{
				serialPortMode = SerialPortModes.TCPMode;
			}
		}

		public AsynchSerialPort()
		{
			serialPort = new SerialPort();
			serialPort.DataReceived +=new SerialDataReceivedEventHandler(serialPort_DataReceived);
			requestTimer = new System.Timers.Timer
			{
				Interval = 10
			};
			requestTimer.Elapsed += requestTimer_Tick;
		}

		public void Open()
		{
			portError = false;
			portBusy = false;
			if (serialPortMode == SerialPortModes.RSMode)
			{
				if (serialPort.IsOpen)
				{
					return;
				}
				serialPort.Open();
			}
			else if (serialPortMode == SerialPortModes.TCPMode)
			{
				
				tcpMaster = new ModbusTCPMaster(ipAddress, portNum);
				tcpMaster.OnResponseData +=new ModbusTCPMaster.ResponseData(tcpMaster_OnResponseData);
			}
			requestTimer.Enabled = true; 
		}

		bool flagToClose = false;
		public void Close()
		{
			if (serialPortMode == SerialPortModes.RSMode)
			{
				#region RS232Mode
				if (!serialPort.IsOpen) { return; }
				if (portError)
				{
					try
					{
						serialPort.Close();
						flagToClose = false;
						requests.Clear();
						if (PortClosed != null) { PortClosed(null, null); }
					}
					catch
					{
						if (FatalSerialPortError != null)
						{
							FatalSerialPortError(serialPort, null);
						}
					}
					return;
				}
				flagToClose = true;
				#endregion
			}

			if (serialPortMode == SerialPortModes.TCPMode)
			{
				#region TCPMode
				requests.Clear();
				if (tcpMaster == null) { if (PortClosed != null) { PortClosed(null, null); } return; }
				if (!tcpMaster.Connected) { if (PortClosed != null) { PortClosed(null, null); } return; }
				flagToClose = true;
				if (PortClosed != null) { PortClosed(null, null); }
				#endregion
			}
		}

	   
		void CloseBody()
		{
			requestTimer.Enabled = false;
			if (serialPortMode == SerialPortModes.RSMode)
			{
				serialPort.Close();
			}
			if (serialPortMode == SerialPortModes.TCPMode)
			{
				
				try
				{
					tcpMaster.Disconnnect();
				}
				catch
				{
					throw new NotImplementedException();
				}
			}
			flagToClose = false;
			requests.Clear();
			if (PortClosed != null) { PortClosed(null, null); }
		}
		public event EventHandler PortClosed;


		EventWaitHandle waitSerialData = new AutoResetEvent(false);

		void serialPort_DataReceived(object Sender, EventArgs e)
		{
			waitSerialData.Set();
		}


		void tcpMaster_OnResponseData(ushort ID, byte Unit, byte Function, byte[] Data)
		{
			if (Function == 0x03 || Function == 0x04)
			{
				tcpReadData = Data;
				waitSerialData.Set();
			}
			else if (Function == 0x10)
			{
				tcpWriteOk = true;
				waitSerialData.Set();
			}
		}


		void SendDataBody(RequestUnit RequestUnit)
		{
			if (portError) { return; }
			if (serialPortMode == SerialPortModes.TCPMode)
			{
				if (RequestUnit.PortAnswerType != PortAnswerType.RTU)
				{
					throw new Exception("Invalid request  type");
				}
			}

			if (serialPortMode == SerialPortModes.RSMode)
			{
				try
				{
					if (serialPort.BytesToRead !=0)
					{
						byte[] buff = new byte[serialPort.BytesToRead+1];
						serialPort.Read(buff, 0, serialPort.BytesToRead);
					}
					Thread.Sleep(3);
					serialPort.Write(RequestUnit.TxBuffer, 0, RequestUnit.TxBuffer.Length);
				}
				catch
				{
					portError = true;
					if (SerialPortError != null) { SerialPortError(serialPort, null); }
					return;

				}
			}

			tcpReadData = null;
			tcpWriteOk = false;
			if (serialPortMode == SerialPortModes.TCPMode)
			{
				if (RequestUnit.GetTCPFunction() == TCPFunctions.TCPRead)
				{
					tcpMaster.ReadHoldingRegister(1, RequestUnit.GetSlaveAddr(), RequestUnit.GetTCPStartAddr(), RequestUnit.GetTCPReadCount());
				}
				else
				{
					tcpMaster.WriteMultipleRegister(1, RequestUnit.GetSlaveAddr(), RequestUnit.GetTCPStartAddr(), RequestUnit.GetTCPWriteData());
				}
			}

			if (serialPortMode == SerialPortModes.RSMode)
			{
				waitSerialData.WaitOne(TimeSpan.FromMilliseconds(100));

				for (int i = 0; i < 100; i++)
				{
					if (serialPort.BytesToRead < RequestUnit.ReceivedBytesThreshold)
					{
						waitSerialData.WaitOne(TimeSpan.FromMilliseconds(10));
					}
					else break;
				}
			}

			if (serialPortMode == SerialPortModes.TCPMode)
			{
				waitSerialData.WaitOne(TimeSpan.FromMilliseconds(3000));
			}

			bool dataOk = false;
			#region RecieveRS232
			if (serialPortMode == SerialPortModes.RSMode)
			{
				byte[] rxBuffer = new byte[0];

				if (serialPort.BytesToRead >= RequestUnit.ReceivedBytesThreshold)
				{
					int count = serialPort.BytesToRead;
					rxBuffer = new byte[count];

					try
					{
						serialPort.Read(rxBuffer, 0, count);
					}
					catch
					{
						portError = true;
						if (SerialPortError != null) { SerialPortError(serialPort, null); }
						return;
					}

					dataOk = true;
				}
				else
				{
					// System.Windows.Forms.MessageBox.Show(RequestUnit.ReceivedBytesThreshold.ToString());
				}

				switch (RequestUnit.PortAnswerType)
				{
					case PortAnswerType.Byte:
						{
							if (RequestUnit.DataRecieved != null)
							{
								RequestUnit.DataRecieved(dataOk, rxBuffer);
							} break;
						}
					case PortAnswerType.RTU:
						{
							ushort[] ubuff = new ushort[0];

							if (!ModBusCRC.CheckCRC(rxBuffer, rxBuffer.Length))
							{
								// System.Windows.Forms.MessageBox.Show("Ошибка CRC  " + rxBuffer.Length.ToString());
								dataOk = false;
							}
							else
							{
								if (rxBuffer[1] == 0x03 || rxBuffer[1] == 0x04)
								{
									ModBusCRC.RemoveData(rxBuffer, 3, RequestUnit.RTUReadCount, out ubuff);
								}
								else
								{
									ubuff = new ushort[0];
								}
								dataOk = true;
							}

							if (RequestUnit.DataRecievedRTU != null)
							{
								RequestUnit.DataRecievedRTU(dataOk, ubuff, RequestUnit.Param);
							}

						} break;
				}
			}
			#endregion
			#region RecieveTCP
			if (serialPortMode == SerialPortModes.TCPMode)
			{
				if (RequestUnit.GetTCPFunction() == TCPFunctions.TCPRead)
				{
					dataOk = (tcpReadData != null);
					ushort[] us = new ushort[0];
					if (dataOk) ModBusCRC.RemoveData(tcpReadData, 0, RequestUnit.RTUReadCount, out us);
					if (RequestUnit.DataRecievedRTU != null)
					{
						RequestUnit.DataRecievedRTU(dataOk, us, RequestUnit.Param);
					}
				}
				else
				{
					ushort[] us = new ushort[0];
					if (RequestUnit.DataRecievedRTU != null)
					{
						RequestUnit.DataRecievedRTU(tcpWriteOk, us, RequestUnit.Param);
					}
				}
			}
			#endregion

			lock (locker)
			{
				bool b = CheckQueue(false);
				portBusy = b;
			}

			
		}

		delegate void SendDataHandler(RequestUnit RequestUnit);
		public void SendData(RequestUnit RequestUnit)
		{
			
			if (!IsOpen) { return; }
			lock (locker)
			{
				portBusy = true;
			}
			SendDataHandler senddelegate = new SendDataHandler(SendDataBody);
			senddelegate.BeginInvoke(RequestUnit, null, null);
		}

		public Queue<RequestUnit> requests = new Queue<RequestUnit>();
		public Queue<RequestUnit> requestsMain = new Queue<RequestUnit>();
		public void AddRequest(byte[] TxBuffer, int ReceivedBytesThreshold, DataRecieved OnDataRecieved)
		{
			if (!IsOpen) { return; }
			lock (locker)
			{
				requests.Enqueue(new RequestUnit(TxBuffer, ReceivedBytesThreshold, OnDataRecieved));
			}

		}
		void AddRequest(byte[] TxBuffer, int ReceivedBytesThreshold, DataRecievedRTU OnDataRecievedRTU, int RTUReadCount, RequestPriority RequestPriority, object param)
		{
			if (!IsOpen) { return; }
			

			if (RequestPriority == UniSerialPort.RequestPriority.High)
			{
				lock (locker)
				{
					requestsMain.Enqueue(new RequestUnit(TxBuffer, ReceivedBytesThreshold, OnDataRecievedRTU, RTUReadCount, param));
				}
			}
			else
			{
				lock (locker)
				{
					var request = new RequestUnit(TxBuffer, ReceivedBytesThreshold, OnDataRecievedRTU,RTUReadCount, param);
					requests.Enqueue(request);
				}
			}
		}

		public void AddRequest(byte[] TxBuffer, int ReceivedBytesThreshold, DataRecievedRTU OnDataRecievedRTU, RequestPriority RequestPriority, object param)
		{
			lock (locker)
			{
				AddRequest(TxBuffer, ReceivedBytesThreshold, OnDataRecievedRTU, 0, RequestPriority, param);
			}
		}


		bool CheckQueue(bool CheckPortBusy)
		{
			lock (locker)
			{
				if (portBusy && CheckPortBusy) { return false; }
			}

			if (flagToClose) { CloseBody(); return false; }
			if (!IsOpen) { return false; }

			RequestUnit mu;
			
			lock (locker)
			{
				if (requestsMain.Count != 0)
				{
					mu = requestsMain.Dequeue();
					SendData(mu);
					return true;
				}
			}

			lock (locker)
			{
				if (requests.Count == 0) { return false; }
				mu = requests.Dequeue();
				SendData(mu);
				return true;
			}
		}

		object locker = new object();

		void requestTimer_Tick(object sender, EventArgs e)
		{
				CheckQueue(true);
		}

		public void GetDataRTU(byte SlaveAddress, ushort StartAddr, ushort WordCount, DataRecievedRTU DataRecievedRTU, RequestPriority RequestPriority, object param)
		{
			byte[] buffer = new byte[6];
			buffer[0] = SlaveAddress;
			buffer[1] = 0x03;
			buffer[2] = (byte)((StartAddr >> 8) & 0xFF);
			buffer[3] = (byte)(StartAddr & 0xFF);
			buffer[4] = (byte)((WordCount >> 8) & 0xFF);
			buffer[5] = (byte)(WordCount & 0xFF);
			byte[] buffer1;
			ModBusCRC.CalcCRC(buffer, 6, out buffer1);
			AddRequest(buffer1, WordCount * 2 + 5, DataRecievedRTU, WordCount, RequestPriority, param);
		}
		public void GetDataRTU(ushort StartAddr, ushort WordCount, DataRecievedRTU DataRecievedRTU, object param)
		{
			GetDataRTU(SlaveAddr, StartAddr, WordCount, DataRecievedRTU, RequestPriority.Normal, param);
		}
		public void GetDataRTU(ushort StartAddr, ushort WordCount, DataRecievedRTU DataRecievedRTU, RequestPriority RequestPriority, object param)
		{
			GetDataRTU(SlaveAddr, StartAddr, WordCount, DataRecievedRTU, RequestPriority, param);
		}

		public void GetDataRTU04(byte SlaveAddress, ushort StartAddr, ushort WordCount, DataRecievedRTU DataRecievedRTU, RequestPriority RequestPriority, object param)
		{
			byte[] buffer = new byte[6];
			buffer[0] = SlaveAddress;
			buffer[1] = 0x04;
			buffer[2] = (byte)((StartAddr >> 8) & 0xFF);
			buffer[3] = (byte)(StartAddr & 0xFF);
			buffer[4] = (byte)((WordCount >> 8) & 0xFF);
			buffer[5] = (byte)(WordCount & 0xFF);
			byte[] buffer1;
			ModBusCRC.CalcCRC(buffer, 6, out buffer1);
			AddRequest(buffer1, WordCount * 2 + 5, DataRecievedRTU, WordCount, RequestPriority, param);
		}
		public void GetDataRTU04(ushort StartAddr, ushort WordCount, DataRecievedRTU DataRecievedRTU, object param)
		{
			GetDataRTU04(SlaveAddr, StartAddr, WordCount, DataRecievedRTU, RequestPriority.Normal, param);
		}

		private void SetDataRTU(byte slaveAddress, ushort startAddr, DataRecievedRTU dataRecievedRtu, RequestPriority  requestPriority, object param, params ushort[] data)
		{
			if ((data.Length > 32) || (data.Length < 1))
			{
				dataRecievedRtu?.Invoke(false, new ushort[0], null);
				return;
			}
			byte[] buffer = new byte[9+data.Length*2];
			buffer[0] = slaveAddress;
			buffer[1] = 0x10;
			buffer[2] = (byte)((startAddr >> 8) & 0xFF);
			buffer[3] = (byte)(startAddr & 0xFF);
			buffer[4] = (byte)((data.Length >> 8) & 0xFF);
			buffer[5] = (byte)(data.Length & 0xFF);
			buffer[6] = (byte)(2 * data.Length);
			
			for (int i = 0; i < data.Length; i++)
			{
				buffer[7+i*2] = (byte)((data[i] >> 8) & 0xFF);
				buffer[8+i*2] = (byte)(data[i] & 0xFF);
			}

			byte[] buffer1;
			ModBusCRC.CalcCRC(buffer, 7+data.Length*2, out buffer1);
			AddRequest(buffer1, 8, dataRecievedRtu, requestPriority, param);
		}

		public void SetDataRTU(ushort startAddr, DataRecievedRTU dataRecievedRtu, RequestPriority requestPriority, object param, params ushort[] data)
		{
			SetDataRTU(SlaveAddr, startAddr, dataRecievedRtu, requestPriority, param, data);
		}



		public void SaveSettingsToFile(string FileName)
		{
			try
			{
				XmlTextWriter textWritter = new XmlTextWriter(FileName, Encoding.UTF8);
				textWritter.WriteStartDocument();
				textWritter.WriteStartElement("Setup");
				textWritter.WriteEndElement();
				textWritter.Close();
			}
			catch
			{
				throw new Exception("Error create file!");
			}

			XmlDocument document = new XmlDocument();
			try
			{
				document.Load(FileName);
			}
			catch
			{
				throw new Exception("Error create file!");
			}

			XmlNode element = document.CreateElement("ComSettings");
			document.DocumentElement.AppendChild(element); // указываем родителя

			XmlAttribute attribute = document.CreateAttribute("PortName"); // создаём атрибут
			attribute.Value = PortName;
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("Parity"); // создаём атрибут
			attribute.Value = Parity.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("BaudRate"); // создаём атрибут
			attribute.Value = BaudRate.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("StopBits"); // создаём атрибут
			attribute.Value = StopBits.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("SlaveAddr"); // создаём атрибут
			attribute.Value = SlaveAddr.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("IPAddr"); // создаём атрибут
			attribute.Value = IpAddress;
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("IPPortNum"); // создаём атрибут
			attribute.Value = portNum.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			attribute = document.CreateAttribute("PortMode"); // создаём атрибут
			attribute.Value = serialPortMode.ToString();
			element.Attributes.Append(attribute); // добавляем атрибут

			try
			{
				document.Save(FileName);
			}
			catch
			{
				throw new Exception("Error create file!");
			}

		}

		public void LoadSettingsFromFile(string FileName)
		{
			XDocument document;
			try
			{
				document = XDocument.Load(FileName);

			}
			catch
			{
				throw new Exception("Error open xml file!");
			}

			try
			{
				XElement element = document.Root.Element("ComSettings");
				PortName = Convert.ToString(element.Attribute("PortName").Value);

				PortNum = Convert.ToUInt16(element.Attribute("IPPortNum").Value.ToString());
				IpAddress = element.Attribute("IPAddr").Value.ToString();

				if (element.Attribute("PortMode").Value.ToString().ToUpper() == "RSMODE")
				{
					serialPortMode = SerialPortModes.RSMode;
				}
				else
				{
					serialPortMode = SerialPortModes.TCPMode;
				}

				BaudRate = Convert.ToInt32(element.Attribute("BaudRate").Value.ToString());
				SlaveAddr = Convert.ToByte(element.Attribute("SlaveAddr").Value);

				string strPar, strStops;

				strPar = Convert.ToString(element.Attribute("Parity").Value);
				strStops = Convert.ToString(element.Attribute("StopBits").Value);

				if (strPar == "None")
				{
					Parity = System.IO.Ports.Parity.None;
				}
				else if (strPar == "Even")
				{
					Parity = System.IO.Ports.Parity.Even;
				}
				else
				{
					Parity = System.IO.Ports.Parity.Odd;
				}

				if (strStops == "Two")
				{
					StopBits = System.IO.Ports.StopBits.Two;
				}
				else
				{
					StopBits = System.IO.Ports.StopBits.One;
				}
			}

			catch
			{
				throw new Exception("Error load settings from file!");
			}


		}
	}
}
