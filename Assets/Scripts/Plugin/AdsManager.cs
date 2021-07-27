using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4101371");
        Advertisement.AddListener(this);
    }

    public void PlayAd()
    {
        if (Advertisement.IsReady("GetMoneh"))
        {
            Advertisement.Show("GetMoneh");
        }
    }

    public void PlayRewardedAd()
    {
        if (Advertisement.IsReady("AddMoneh"))
        {
            Advertisement.Show("AddMoneh");
        }
        else
        {
            Debug.Log("Ads Is Not Ready!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads Are Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Video started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "AddMoneh" && showResult == ShowResult.Finished)
        {
            Debug.Log("Player Rewarded");
        }
    }
}