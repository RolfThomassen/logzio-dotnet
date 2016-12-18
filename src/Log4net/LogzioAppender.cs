﻿using System;
using System.Collections.Generic;
using log4net.Appender;
using log4net.Core;
using Logzio.DotNet.Core.Shipping;

namespace Logzio.DotNet.Log4net
{
	public class LogzioAppender : AppenderSkeleton
	{
		private readonly Shipper _shipper = new Shipper();

		public LogzioAppender()
		{
			_shipper.SendOptions.Type = "log4net";
		}

		protected override void Append(LoggingEvent loggingEvent)
		{
			_shipper.Log(new LogEvent(new Dictionary<string,string>
			{
				{"@timestamp", loggingEvent.TimeStamp.ToString("o")},
				{"logger", loggingEvent.LoggerName },
				{"domain", loggingEvent.Domain },
				{"level", loggingEvent.Level.DisplayName },
				{"thread", loggingEvent.ThreadName },
				{"message", loggingEvent.RenderedMessage },
				{"exception", loggingEvent.GetExceptionString() },
				{"user", loggingEvent.UserName }
			}));
		}

		public void AddToken(string value)
		{
			_shipper.SendOptions.Token = value;
		}

		public void AddType(string value)
		{
			_shipper.SendOptions.Type = value;
		}

		public void AddIsSecured(bool value)
		{
			_shipper.SendOptions.IsSecured = value;
		}

		public void AddBufferSize(int bufferSize)
		{
			_shipper.Options.BufferSize = bufferSize;
		}

		public void AddBufferTimeout(TimeSpan value)
		{
			_shipper.Options.BufferTimeLimit = value;
		}

		public void AddRetriesMaxAttempts(int value)
		{
			_shipper.SendOptions.RetriesMaxAttempts = value;
		}

		public void AddRetriesInterval(TimeSpan value)
		{
			_shipper.SendOptions.RetriesInterval = value;
		}
	}
}