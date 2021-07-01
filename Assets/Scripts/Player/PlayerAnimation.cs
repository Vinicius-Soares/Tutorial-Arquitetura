using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerAnimation : Player
{
    
    private enum Facing
    {
        Up,
        Down,
        Right,
        Left
    }

    private enum Animations
    {
        WalkingRight,
        WalkingLeft,
        WalkingUp,
        WalkingDown,
        IdleRight,
        IdleLeft,
        IdleUp,
        IdleDown
    }

    private Animator m_Animator;
    private Facing m_Facing;

    private string m_CurrentAnimation;
    protected override void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
        m_Facing = Facing.Down;
    }
    
    private void FixedUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (m_Rb.velocity == Vector2.zero)
        {
            switch (m_Facing)
            {
                case Facing.Down:
                    ChangeAnimation(Animations.IdleDown);
                    break;
                case Facing.Up:
                    ChangeAnimation(Animations.IdleUp);
                    break;
                case Facing.Left:
                    ChangeAnimation(Animations.IdleLeft);
                    break;
                case Facing.Right:
                    ChangeAnimation(Animations.IdleRight);
                    break;
                default:
                    ChangeAnimation(Animations.IdleDown);
                    break;
            }
        }
        if (m_Rb.velocity.x > 0.1f)
        {
            ChangeAnimation(Animations.WalkingRight);
            m_Facing = Facing.Right;
        }
        else if (m_Rb.velocity.x < -0.1f)
        {
            ChangeAnimation(Animations.WalkingLeft);
            m_Facing = Facing.Left;
        }
        else if (m_Rb.velocity.y > 0.1f)
        {
            ChangeAnimation(Animations.WalkingUp);
            m_Facing = Facing.Up;
        }
        else if (m_Rb.velocity.y < -0.1f)
        {
            ChangeAnimation(Animations.WalkingDown);
            m_Facing = Facing.Down;
        }
    }

    private void ChangeAnimation(Animations animationName)
    {
        if(m_CurrentAnimation == animationName.ToString()) return;

        m_CurrentAnimation = animationName.ToString();
        m_Animator.Play(m_CurrentAnimation);
    }
}
