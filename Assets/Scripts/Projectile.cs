using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] private float lifeTime;

    private IEnumerator m_ProjectileCoroutine;
    void Update()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));
    }

    public void Deactivate()
    {
        if (m_ProjectileCoroutine != null)
        {
            StopCoroutine(m_ProjectileCoroutine);
        }

        m_ProjectileCoroutine = DeactivateInTime(lifeTime);
        StartCoroutine(m_ProjectileCoroutine);
    }

    IEnumerator DeactivateInTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Enemy.ToString()))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
