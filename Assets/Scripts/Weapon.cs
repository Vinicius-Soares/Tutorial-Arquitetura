using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float Firerate;
    [SerializeField] private int PoolSize;

    private PlayerInput m_InputActions;

    private Queue<GameObject> projectilePool;

    private float m_FirerateTime;

    private bool m_ShootPerformed;
    private void Awake()
    {
        

        m_InputActions = new PlayerInput();

        m_InputActions.Player.Shoot.performed += ctx => ShootPerformed(ctx);
        m_InputActions.Player.Shoot.canceled += ctx => ShootPerformed(ctx);
        projectilePool = new Queue<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject newProjectile = Instantiate(Bullet);
            projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
            
        }
    }
    
    private void OnEnable() => m_InputActions.Enable();

    private void OnDisable() => m_InputActions.Disable();

    private void Update()
    {
        StaffRotation();

        m_FirerateTime += Time.deltaTime;

        if (m_FirerateTime > Firerate && m_ShootPerformed)
        {
            SpawnProjectile();
            m_FirerateTime = 0f;
        }
    }


    private void ShootPerformed(InputAction.CallbackContext ctx)
    {
        m_ShootPerformed = ctx.performed;
    }
    private void StaffRotation()
    {
        var direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private void SpawnProjectile()
    {
        GameObject bullet = projectilePool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Projectile>().Deactivate();
        
        projectilePool.Enqueue(bullet);

    }
}
