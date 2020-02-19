﻿using System;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
namespace NewRelic.Agent.Core.Utilities.UnitTest
{
	[TestFixture]
	public class Class_EventSubscription
	{
		[Test]
		public void publishing_outside_using_statement_results_in_no_callback()
		{
			var wasCalled = false;
			using (new EventSubscription<object>(_ => wasCalled = true)) {
			}

			EventBus<object>.Publish(new object());

			Assert.IsFalse(wasCalled);
		}

		[Test]
		public void publishing_inside_using_statement_results_in_callback()
		{
			var wasCalled = false;
			using (new EventSubscription<object>(_ => wasCalled = true)) {
				EventBus<object>.Publish(new object());
			}

			Assert.IsTrue(wasCalled);
		}

		[Test]
		public void two_disposables_with_same_callback_are_called_once()
		{
			var callCount = 0;
			Action<object> callback = _ => ++callCount;
			using (new EventSubscription<object>(callback))
			using (new EventSubscription<object>(callback))
			{
				EventBus<object>.Publish(new object());
			}

			Assert.AreEqual(1, callCount);
		}
	}
}