using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Component.Animating;

public class move2d : NetworkBehaviour
{
    private Rigidbody2D _rb2d;
    private Animator _animator;
    public float speed =1.0f;
    private Vector2 move;
    private bool _flipped=false; 
    private NetworkAnimator _animator2;

    private void Awake()
    {
          _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator2 = GetComponent<NetworkAnimator>();

    }
    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        GetComponent<PlayerInput>().enabled = base.IsOwner;
    }
    public void attack(InputAction.CallbackContext context)
    {
        if(!base.IsOwner) { return; }
        if (context.started) { 
            _animator2.SetTrigger("ATTCK");
        }
        if (context.canceled)
        {
            _animator2.ResetTrigger("ATTCK");
        }
    }
    public void Move(InputAction.CallbackContext context)     
    {
        if(!base.IsOwner) { return; }
        move  = context.ReadValue<Vector2>();
        _animator.SetFloat("speed", move.magnitude);
        if(move.x<0 && !_flipped) {
            _flipped = true;
        }
        else if(move.x>0 && _flipped)
        {   
            _flipped = false;
        }
        transform.localScale = new Vector3(_flipped ? -1:1 , 1, 1);
    }
    void FixedUpdate()

    {
        if(!base.IsOwner) { return; }
        _rb2d.MovePosition(_rb2d.position+move* Time.fixedDeltaTime * speed);
    }
}
