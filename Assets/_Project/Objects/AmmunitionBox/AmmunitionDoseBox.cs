using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionDoseBox : MonoBehaviour
{
    [SerializeField] private int ammoToAdd;
    //[SerializeField] private AmmunitionSpawner ammunitionSpawner;
    [SerializeField] private GameObject dose;
    [SerializeField] private Collider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D tri)
    {
        if(tri.CompareTag("Player"))
        {
            ICumulative ammo = tri.gameObject.GetComponent<ICumulative>();
            ammo.AddAmmo(ammoToAdd);
            //ammunitionSpawner.SpawnAmmunitionBox();

            //UIEvents.AmmoRestoreUI();

            //Para iniciar la oleada al recoger la primera caja de munición.
            //if (!GameManager.Instance.startWaveSequence)
            //{
            //    GameManager.Instance.StartGame();
            //}

            EmptyBox();
        }
    }

    private void EmptyBox()
    {
        boxCollider.enabled = false;
        dose.SetActive(false);
    }

    public void LoadBox()
    {
        dose.SetActive(true);
        boxCollider.enabled = true;
    }
}
