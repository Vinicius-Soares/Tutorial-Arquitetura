using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Player
{
    [SerializeField]private float Speed;

    private PlayerInput m_InputActions;
    private Vector2 m_Direction;

    
    protected override void Awake()
    {
        base.Awake();
        m_InputActions = new PlayerInput();

        m_InputActions.Player.Movement.performed += ctx => m_Direction = ctx.ReadValue<Vector2>();
        m_InputActions.Player.Movement.canceled += ctx => m_Direction = Vector2.zero;
    }

    private void OnEnable() => m_InputActions.Enable();

    private void OnDisable() => m_InputActions.Disable();

     private void FixedUpdate()
    {
        m_Rb.velocity = m_Direction * Speed;
    }
}
