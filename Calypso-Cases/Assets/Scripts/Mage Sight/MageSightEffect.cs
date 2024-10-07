using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MageSightEffect : MonoBehaviour, ISightObserver
{
    [SerializeField]
    private PostProcessVolume _volume;
    [SerializeField]
    private MageSightToggle _manager;

    public MageSightToggle Manager => _manager;

    public void Subscribe()
    {
        Manager.SightObservers += SetSightEffect;
    }

    public void Unsubscribe()
    {
        Manager.SightObservers -= SetSightEffect;
    }

    /// <summary>
    /// Enables or disables volume depending on the MageSight status.
    /// </summary>
    public void SetSightEffect()
    {
        _volume.enabled = Manager.SightEnabled;
    }

    private void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
