using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MageSightEffect : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume _volume;
    public void SetSightEffect(bool on)
    {
        _volume.enabled = on;
    }
}
