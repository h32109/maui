using System;
using System.Windows.Input;
using NUnit.Framework;
using System.Maui.Core.UnitTests;

namespace System.Maui.Xaml.UnitTests
{
#pragma warning disable 0618 //retaining legacy call to obsolete code

	[TestFixture]
	public class XamlLoaderCreateTests
	{
		[TearDown]
		public void TearDown()
		{
			Device.PlatformServices = null;
			XamlLoader.FallbackTypeResolver = null;
			XamlLoader.ValueCreatedCallback = null;
			XamlLoader.InstantiationFailedCallback = null;
			System.Maui.Internals.ResourceLoader.ExceptionHandler = null;
			System.Maui.Xaml.Internals.XamlLoader.DoNotThrowOnExceptions = false;
		}

		[Test]
		public void CreateFromXaml ()
		{
			var xaml = @"
				<ContentView xmlns=""http://xamarin.com/schemas/2014/forms""
							 xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
						 	 x:Class=""System.Maui.Xaml.UnitTests.FOO"">
					<Label Text=""Foo""  x:Name=""label""/>
				</ContentView>";

			var view = XamlLoader.Create (xaml);
			Assert.That (view, Is.TypeOf<ContentView> ());
			Assert.AreEqual ("Foo", ((Label)((ContentView)view).Content).Text);
		}

		[Test]
		public void CreateFromXamlDoesntFailOnMissingEventHandler ()
		{
			var xaml = @"
				<Button xmlns=""http://xamarin.com/schemas/2014/forms""
						xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
						Clicked=""handleClick"">
				</Button>";

			Button button = null;
			Assert.DoesNotThrow (() => button = XamlLoader.Create (xaml, true) as Button);
			Assert.NotNull (button);
		}
	}
#pragma warning restore 0618
}