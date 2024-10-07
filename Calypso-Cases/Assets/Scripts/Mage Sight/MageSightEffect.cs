using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MageSightEffect : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume _volume;
    [SerializeField]
    private MageSightToggle _manager;

    private void Start()
    {
        _manager.SightObservers += SetSightEffect;
    }

    public void SetSightEffect()
    {
        _volume.enabled = _manager.SightEnabled;
    }

    private void OnDestroy()
    {
        _manager.SightObservers -= SetSightEffect;
    }
}
