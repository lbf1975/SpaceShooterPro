using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f; //controls player movement speed.
    private float _speedboostX = 2;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _tripshotPrefab;
    [SerializeField] GameObject _ShieldPwrup;
    [SerializeField] float _fireRate = 0.5f;
    [SerializeField] float _canFire = -1f;
    [SerializeField] int _lives = 3;
    [SerializeField] SpawnManager _spawnManager;
    [SerializeField] int _score;
    bool _tripleshotActive = false;
    bool _speedBoostActive = false;
    bool _shieldActive = false;
    private UIManager _uiManager;

   // Start is called before the first frame update
   void Start()
    {
        //current position = new position with (0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        



    }

    void CalculateMovement() //all things movement
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //new Vector3(1,0,0) * real time

        //if (_speedBoostActive == false)
        //{
        transform.Translate(direction * _speed * Time.deltaTime);
        //}
        //else if (_speedBoostActive == true)
        //{
        //    transform.Translate(direction * _speed * _speedboostX * Time.deltaTime);
        //}


        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        
        if (transform.position.x >= 11.30)
        {
            transform.position = new Vector3(-11.30f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.30f)
        {
            transform.position = new Vector3(11.30f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_tripleshotActive == true)
        {
            
            //Vector3 _laserOffset = transform.position + new Vector3(0, 1.0f, 0);
            Instantiate(_tripshotPrefab, transform.position + new Vector3(3.0f,1.9f,0), Quaternion.identity);
        }                

        else
        {
            
            //Vector3 _laserOffset = transform.position + new Vector3(0, 1.0f, 0);
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
    }
   

public void Damage()
    {
        if(_shieldActive == true)
        {
            _shieldActive = false;
            _ShieldPwrup.SetActive(false);
            return;
        }

        _lives -= 1;

        _uiManager.UpdateLives(_lives);
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripShotActive()
    {
        _tripleshotActive = true;
        StartCoroutine(TripShotPowerDwn());
    }

    IEnumerator TripShotPowerDwn()
    {
        yield return new WaitForSeconds(5f);
        _tripleshotActive = false;
    }
    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed *= _speedboostX;
        StartCoroutine(SpeedBoostPowerDwn());
    }

    IEnumerator SpeedBoostPowerDwn()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _speed /= _speedboostX;
    }
    public void ShieldActive()
    {
        _shieldActive = true;
        _ShieldPwrup.SetActive(true);

    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

}
