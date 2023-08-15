using UnityEngine;

namespace SpaceGame.Cooldown
{
    public interface IHasCooldown
    {
        int Id { get; }
        float CooldownDuration { get; }
    }
}
