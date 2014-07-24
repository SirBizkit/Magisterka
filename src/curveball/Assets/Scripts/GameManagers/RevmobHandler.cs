using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RevmobHandler : MonoBehaviour, IRevMobListener {
	
	private const int REFRESH_ADS_TIMER = 1;
	
	private static readonly Dictionary<string, string> REVMOB_APP_IDS = new Dictionary<string, string>() {
        { "Android", "5290d844e80ceb38c2000026"}
    };

	private RevMob revmob;
	private RevMobFullscreen preloadedFullscreenAd;
	
	public bool bannerShouldBeReloaded = true;
	public bool fullscreenAdShouldBeReloaded = true;

    public void Awake() {
       	revmob = RevMob.Start(REVMOB_APP_IDS, "gameManager");
		//revmob.SetTestingMode(RevMob.Test.WITH_ADS);
		//revmob.SetTestingMode(RevMob.Test.WITHOUT_ADS);
		StartCoroutine(loadAds());
	}
	
	IEnumerator loadAds(){
		while(true)
		{
			if(fullscreenAdShouldBeReloaded){
				preloadedFullscreenAd = revmob.CreateFullscreen(); // jest jeszcze android fullscreen?
				fullscreenAdShouldBeReloaded = false;
			}
			
			yield return new WaitForSeconds(REFRESH_ADS_TIMER);			
		}		
	}
	
    public void AdDidReceive (string revMobAdType) {
        Debug.Log("Ad did receive.");
    }

    public void AdDidFail (string revMobAdType) {
        Debug.Log("Ad did fail.");
    }

    public void AdDisplayed (string revMobAdType) {
        Debug.Log("Ad displayed.");
		fullscreenAdShouldBeReloaded = true;
    }

    public void UserClickedInTheAd (string revMobAdType) {
        Debug.Log("Ad clicked.");
    }

    public void UserClosedTheAd (string revMobAdType) {
        Debug.Log("Ad closed.");
    }
	
	public void InstallDidReceive(string revMobAdType) {
        Debug.Log("App Installed?");
    }
	
	public void InstallDidFail(string revMobAdType) {
        Debug.Log("App not installed?");
    }
	
	public RevMob getRevMobObject(){
		if(revmob == null)
			revmob = RevMob.Start(REVMOB_APP_IDS, "gameManager");
		
		return revmob;
	}
	
	public RevMobFullscreen getFullscreenAd(){
		return preloadedFullscreenAd;
	}
}
