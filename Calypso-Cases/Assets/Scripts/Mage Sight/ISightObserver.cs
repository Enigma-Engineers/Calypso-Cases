public interface ISightObserver
{
    public abstract MageSightToggle Manager { get; }

    /// <summary>
    /// Should subscribe the SetSightEffect method to the manager's observer event.
    /// Additionally, should be called on the gameobject's Start method.
    /// </summary>
    public abstract void Subscribe();
    /// <summary>
    /// Should unsubscribe the SetSightEffect method from the manager's observer event.
    /// Additionally, should be called on the gameobject's onDestroy method.
    /// </summary>
    public abstract void Unsubscribe();
    /// <summary>
    /// Should perform whatever action needed to occur when the status of MageSight changes.
    /// </summary>
    public abstract void SetSightEffect();
}
