﻿using System;
using Xamarin.Forms;
using ZXing;

namespace WifiBarcodeSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		// github.com/Redth/ZXing.Net.Mobile 
		public void Scan_Barcode(object sender, EventArgs e)
		{
			BarcodeImageView.IsVisible = false;

			BarcodeScanView.IsVisible = true;

			BarcodeScanView.IsScanning = true;
		}

		public void Handle_OnScanResult(Result result)
		{
			DisplayAlert("Failure", "Experience was NOT added!", "Ok");
			if (string.IsNullOrWhiteSpace(result.Text)) { 
				DisplayAlert("Failure", "Result er tomt", "Ok");
				return; 
			}
			DisplayAlert("Failure", "Result indeholder: " + result.Text, "Ok");
			if (!result.Text.ToUpperInvariant().StartsWith("WIFI:", StringComparison.Ordinal))
				return;

			var ssid = GetValueForIdentifier('S', result.Text);
			var security = GetValueForIdentifier('T', result.Text);
			var password = GetValueForIdentifier('P', result.Text);
			var ssidHidden = GetValueForIdentifier('H', result.Text);

			Device.BeginInvokeOnMainThread(() =>
			{
				//Ssid.Text = ssid;

				//switch (security)
				//{
				//	case "WPA":
				//		Security.SelectedIndex = 0;
				//		break;
				//	case "WEP":
				//		Security.SelectedIndex = 1;
				//		break;
				//	default:
				//		Security.SelectedIndex = 2;
				//		break;
				//}

				//Password.Text = password;
				//HiddenSsid.IsToggled = !string.IsNullOrWhiteSpace(ssidHidden) && ssidHidden.ToUpperInvariant() == "TRUE";
			});
		}

		private string GetValueForIdentifier(char identifier, string haystack)
		{
			var startIdx = haystack.IndexOf($"{identifier}:", StringComparison.Ordinal);

			if (startIdx == -1)
				return "";

			startIdx += 2;

			var length = haystack.IndexOf(';', startIdx) - startIdx;

			return haystack.Substring(startIdx, length);
		}

		
	}
}