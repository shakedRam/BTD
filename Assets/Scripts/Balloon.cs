using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Collider2D[] collidersToIgnore;
    [SerializeField] private Bullet bulletPrefab;
    private readonly string _targetColliderTag = "EndPosTag";
    private readonly string _bulletColliderTag = "Bullet";
    private NavMeshAgent _agent;
    private Game _game;
    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collidersToIgnore[0], true);

        _game = Game.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        _agent.SetDestination(target.position);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_targetColliderTag))
        {
            Destroy(gameObject);
            _game.BalloonPassed();
            _game.AddPoint();
        }
        else if (other.CompareTag(_bulletColliderTag))
        {
            Destroy(other.gameObject);
            bulletPrefab.GetComponent<Bullet>().DestroyBullet();
            _game.Hit();
            Color balloonColor = GetComponent<SpriteRenderer>().color;
            
            if (balloonColor == Color.red)
            {
                Destroy(gameObject);
                _game.AddPoint();
            }
            else if (balloonColor == Color.blue)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (balloonColor == Color.green)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
       
    }
}

