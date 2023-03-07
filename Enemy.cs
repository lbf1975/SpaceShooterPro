using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySpeed = 4.0f;
    private Player _player;
    private Animator _enemyAnim;
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnim = GetComponent<Animator>();

        
        
    }

    
    void Update()
    {
        
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        
        if (transform.position.y <= -5.5f)
        {
            float randomx = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomx, 7.5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.3f);
        }
        else if(other.tag == "Laser")
        { 

            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore();
            }
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.3f);
            
        }
    }
}
