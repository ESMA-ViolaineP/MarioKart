using System.Collections;
using TMPro;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer, _text;
    [SerializeField]
    private Collider _itemBoxCollider;
    [SerializeField]
    private float _waitBeforeRespawn = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerItemManager playerItemManagerInContact = other.GetComponent<PlayerItemManager>();

        if(playerItemManagerInContact != null)
        {
            playerItemManagerInContact.GenerateItem();
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        _itemBoxCollider.enabled = false;
        _text.enabled = false;
        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(_waitBeforeRespawn);
        _itemBoxCollider.enabled = true;
        _text.enabled = true;
        _meshRenderer.enabled = true;
    }
}
