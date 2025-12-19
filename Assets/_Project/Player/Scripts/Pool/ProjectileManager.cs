using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// -RESPONSABILIDAD-
/// Este script gestiona el disparo de proyectiles utilizando un sistema de pooling para optimizar el rendimiento.
/// Un spawn de enemigos (crear otro script y cambiar el nombre de la clase).
/// -POSIBLES UPGRADES-
/// Puede usar el jugador (podemos añadir como hijo del objeto Player y asignar el metodo OnShoot al Input Action correspondiente).
/// </summary>
public class ProjectileManager : MonoBehaviour
{
    private Projectile projectilePrefab;
    private int poolSize = 10;
    private bool canShoot;
    private int amountAmmoToRemove = 1;
    private ObjectPool<Projectile> projectilePool;

    [SerializeField] private PlayerAmmunition playerAmmunition;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce; //10f estandar;
    [SerializeField] private float shootRate; //0.5f estandar;


    private void Awake()
    {
        projectilePrefab = GameManager.Instance.playerAmmunitionData.projectile.GetComponent<Projectile>();
        projectilePool = new ObjectPool<Projectile>(projectilePrefab, poolSize, transform);

        // Ligar cada proyectil al pool
        foreach (var proj in GetComponentsInChildren<Projectile>(true))
        {
            proj.SetPool(projectilePool);
        }
    }

    private void Start()
    {
        if (playerAmmunition == null)
        {
            Debug.Log("<color=orange>[OBJECT]</color>: " + gameObject.name + " necesita asignacion.");
            return;
        }
        Invoke(nameof(ReloadWeapon), .75f);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!canShoot) return;
        if (!playerAmmunition.HasAmmo()) return;

        canShoot = false;
        StartCoroutine(Shoot());
    }

    private void ReloadWeapon()
    {
        canShoot = true;
    }


    private IEnumerator Shoot()
    {
        Projectile proj = projectilePool.Get(firePoint.position, firePoint.rotation);
        proj.GetComponent<Rigidbody2D>().AddForce(firePoint.parent.right * shootForce, ForceMode2D.Impulse);
        proj.SetPool(projectilePool);
        playerAmmunition.RemoveAmmo(amountAmmoToRemove);

        yield return new WaitForSeconds(shootRate);
        canShoot = true;
    }


    ///<summary>
    /// Metodo: Shoot
    /// Antes usaba proj.GetComponent<Rigidbody2D>().velocity = firePoint.right * shootForce;
    /// Tuve un inconveniente al disparar con la rotacion del firePoint, asi que use la del padre (Objeto: PLAYER) y AddForce.
    /// proj.GetComponent<Rigidbody2D>().AddForce(firePoint.parent.right * shootForce, ForceMode2D.Impulse);
    ///<summary>
}
