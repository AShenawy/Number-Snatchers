using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthCard : MonoBehaviour
{
    public Sprite healthFull, healthHalf, healthEmpty;
    public AudioClip damageSFX;
    public ParticleSystem explosionPFX;
    Image image;
    Animator anim;
    bool isHalf, isEmpty;


    private void Awake()
    {
        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    public void SetFull()
    {
        image.sprite = healthFull;
        isHalf = false;
        isEmpty = false;
    }

    public void SetHalf()
    {
        if (isHalf)
            return;

        if (damageSFX)
            SoundManager.instance.PlaySFX(damageSFX);

        anim.ResetTrigger("Idle");
        anim.SetTrigger("Shake");

        if (explosionPFX)
            StartCoroutine(PlayExplosionPFX());

        image.sprite = healthHalf;
        isHalf = true;
    }

    public void SetEmpty()
    {
        if (isEmpty)
            return;

        if (damageSFX)
            SoundManager.instance.PlaySFX(damageSFX);

        anim.ResetTrigger("Idle");
        anim.SetTrigger("Shake");

        if (explosionPFX)
            StartCoroutine(PlayExplosionPFX());

        image.sprite = healthEmpty;
        isEmpty = true;
    }

    public void ReturnToIdle()
    {
        anim.ResetTrigger("Shake");
        anim.SetTrigger("Idle");
    }

    IEnumerator PlayExplosionPFX()
    {
        //if (!explosionPFX)
        //    yield break;

        ParticleSystem ps = Instantiate(explosionPFX, transform);
        
        yield return new WaitForSeconds(2f);
        Destroy(ps.gameObject);
    }
}
