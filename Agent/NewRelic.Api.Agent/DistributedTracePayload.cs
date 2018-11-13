﻿namespace NewRelic.Api.Agent
{
	internal class DistributedTracePayload : IDistributedTracePayload
	{
		private static IDistributedTracePayload _noOpDistributedTracePayload = new NoOpDistributedTracePayload();
		private dynamic _wrappedPayload = _noOpDistributedTracePayload;

		internal DistributedTracePayload(dynamic wrappedPayload)
		{
			_wrappedPayload = wrappedPayload;
		}

		public string HttpSafe()
		{
			return _wrappedPayload.HttpSafe();
		}

		public string Text()
		{
			return _wrappedPayload.Text();
		}

		public bool IsEmpty()
		{
			return _wrappedPayload.IsEmpty();
		}
	}
}
