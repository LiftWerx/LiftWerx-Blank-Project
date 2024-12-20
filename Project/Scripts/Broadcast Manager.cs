using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlaySteps;

public class BroadcastManager : MonoBehaviour
{
    [Tooltip("A List of Broadcast Events")]
    public List<BroadcastUpdate> broadcastUpdate;

    [System.Serializable]
    public class BroadcastUpdate
    {
        public string name;
        public string Message;
    }
    public void EmitSignal(int MessageNumber)
    {
        BroadcastUpdate update = broadcastUpdate[MessageNumber];
        SendBroadcast(update.Message);
        Debug.Log(update.Message);
        

    }
    public void SendBroadcast(string message)
    {
        try
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                using (AndroidJavaObject broadcastSender = new AndroidJavaObject("com.liftwerx.broadcastemitter.BroadcastSender"))
                {
                    broadcastSender.Call("sendBroadcast", currentActivity, message);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Exception occurred while sending broadcast: " + e.Message);
        }
    }
}
