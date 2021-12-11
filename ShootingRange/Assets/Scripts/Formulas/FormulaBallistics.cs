using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormulaBallistics
{
    // Internal Ballistics

    /// <summary>
    /// Calculate the projetciles shadow area
    /// </summary>
    /// <param name="diameter">Projectile diameter in (meter)</param>
    /// <returns></returns>
    public static float ShadowArea(float diameter)
    {
        return (Mathf.PI / 4) * (diameter * diameter);
    }

    /// <summary>
    /// Calculate force on projectiles floor
    /// </summary>
    /// <param name="diameter">Projectile diameter in (meter)</param>
    /// <param name="gasPressure">Gas pressure in (pascal)</param>
    /// <returns></returns>
    public static float ForceOnProjectilefloor(float diameter, float gasPressure)
    {
        float shadowArrea = (Mathf.PI / 4) * (diameter * diameter);
        return gasPressure * shadowArrea;
    }

    /// <summary>
    /// Calculate the bullet acceleration
    /// </summary>
    /// <param name="diameter">Projectile diameter in (meter)</param>
    /// <param name="gasPressure">Gas pressure in (pascal)</param>
    /// <param name="mass">Projectile mass in (kilogramm)</param>
    /// <returns></returns>
    public static float BulletAcceleration(float diameter, float gasPressure, float mass)
    {
        float shadowArrea = (Mathf.PI / 4) * (diameter * diameter);
        float forceOnProjectilefloor = gasPressure * shadowArrea;
        return forceOnProjectilefloor / mass;
    }

    /// <summary>
    /// Calculate the muzzle velocity
    /// </summary>
    /// <param name="diameter">Projectile diameter in (meter)</param>
    /// <param name="gasPressure">Gas pressure in (pascal)</param>
    /// <param name="mass">Projectile mass in (kilogramm)</param>
    /// <param name="barrelLength">Length of the guns barrel in (meter)</param>
    /// <returns></returns>
    public static float MuzzleVelocity(float diameter, float gasPressure, float mass, float barrelLength)
    {
        float shadowArrea = (Mathf.PI / 4) * (diameter * diameter);
        float forceOnProjectilefloor = gasPressure * shadowArrea;
        float bulletAcceleration = forceOnProjectilefloor / mass;
        return Mathf.Sqrt(2 * bulletAcceleration * barrelLength);
    }
}
