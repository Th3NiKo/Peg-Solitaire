using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class banner : MonoBehaviour {


	private void RequestBanner()
	{
		
			string adUnitId = "ca-app-pub-5255112824989621/2377949597";


			// Create a 320x50 banner at the top of the screen.
			BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
			bannerView.Show ();
	
		}


		void Start()
		{
			
			RequestBanner ();
		}
}
