using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInputMovement : CMovement
{

    [HideInInspector]
    public Rigidbody2D _rigidbody2d;

    // 스프라이트
    SpriteRenderer _spriteRenderer;
    // 애니메이터
    Animator _animator;

    // 이동 속도
    public float _speed;
    public bool _isJump = false; // 1단 점프 유무
    public bool _isDoubleJump = false; // 2단 점프 유무

    // 점프 크기(힘)
    public float _jumpPower;
    // 지면 위 여부
    public bool _isGround = false;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        InputJump();
    }

    void InputMove()
    {
        //float h = Input.GetAxis("Horizontal");
        float h = Input.GetAxis("Horizontal");

        if ((_characterState._isRightDir && h < 0) ||
            (!_characterState._isRightDir && h > 0))
        {
            Flip(); // CMove.Flip() 호출
        }

        // 캐릭터에 속도를 부여함
        _rigidbody2d.velocity = new Vector2(
            h * _speed, _rigidbody2d.velocity.y);

        // 이동 애니메이션을 수행함
        _animator.SetFloat("Horizontal", h);

        // 수직 상승하강 속도를 애니메이터에 알려줌
        _animator.SetFloat("Vertical", _rigidbody2d.velocity.y);
    }

    // 점프
    void Jump()
    {
        _animator.SetTrigger("Jump");

        // 점프 초기화
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0f);

        // 점프
        _rigidbody2d.AddForce(Vector2.up * _jumpPower);
    }

    void InputJump()
    {
        // 왼쪽 컨트롤키를 누르면
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 점프를 안한상태면
            if (!_isJump) // _isJump == false
            {
                Jump(); // 점프 수행
                _isJump = true; // 점프를 한상태로 변경
            }
            // 이미 점프 한 상태에서 2단 점프를 하지 않은 상태면
            else if (_isJump && !_isDoubleJump)
            {
                Jump(); // 점프 수행

                _isDoubleJump = true; // 이단 점프를 한 상태로 변경
            }
        }
    }

    // 지면 여부 설정
    void GroundSetting(bool isGround)
    {
        _isGround = isGround;

        _animator.SetBool("IsGround", isGround);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // 캐릭터가 지면에 충돌 했다면
        if (col.gameObject.tag.Equals("Ground") ||
            col.gameObject.tag.Equals("MoveGround"))
        {
            GroundSetting(true);
            _isJump = _isDoubleJump = false;

            // 이동 블럭의 경우
            if (col.gameObject.tag.Equals("MoveGround"))
            {
                // 캐릭터를 이동 블럭의 자식으로 등록함
                transform.SetParent(col.transform);
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GroundSetting(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        // 캐릭터가 지면에서 떨어졌다면
        if (col.gameObject.tag.Equals("Ground") ||
            col.gameObject.tag.Equals("MoveGround"))
        {
            GroundSetting(false);

            // 이동 블럭의 경우
            if (col.gameObject.tag.Equals("MoveGround"))
            {
                // 캐릭터를 이동 블럭의 자식으로 등록함
                transform.SetParent(null);
            }
        }
    }
}