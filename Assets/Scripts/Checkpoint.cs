using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        var otherLapManager = other.GetComponent<LapManager>();
        if(otherLapManager != null )
        {
            otherLapManager.AddCheckPoint( this );
            Debug.Log("checkpoint");
        }
    }
}
