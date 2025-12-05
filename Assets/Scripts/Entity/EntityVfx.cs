using System.Collections;
using UnityEngine;

public class EntityVfx : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;
    private Material originalMaterial;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void PlayOnDamageVfx()
    {
        StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }
}
