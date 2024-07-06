using System;
using _Core.Scripts.Utility;
using UnityEngine;

namespace _Core.Scripts.Blaster
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private float shootingCooldown = 1f;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform gunPoint;
        [SerializeField] private bool debugCanShoot = true;
        
        private float _lastShootTime = -999f;
        private bool _canShoot = true;

        private void Start()
        {
            GameManager.Instance.OnArControllerAvailable += EnableShooting;
            GameManager.Instance.OnArControllerUnavailable += DisableShooting;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnArControllerAvailable -= EnableShooting;
            GameManager.Instance.OnArControllerUnavailable -= DisableShooting;
        }

        private void Update()
        {
            if (_canShoot && debugCanShoot && ShootCooldownReady())
            {
                Shoot();
            }
        }

        private bool ShootCooldownReady()
        {
            if (_lastShootTime + shootingCooldown <= Time.time)
            {
                return true;
            }
            return false;
        }

        private void Shoot()
        {
            var newBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
            newBullet.transform.SetParent(GameManager.Instance.TemporaryObjects);
            var newBulletMovement = newBullet.GetComponent<BulletMovement>();
            if (newBulletMovement)
            {
                newBulletMovement.StartBulletMovement(bulletSpeed);
            }

            _lastShootTime = Time.time;
            Destroy(newBullet, 5f);
        }
    
        private void SwitchCanShootOnKey()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canShoot = !_canShoot;
            }
        }

        public void EnableShooting()
        {
            _canShoot = true;
        }

        public void DisableShooting()
        {
            _canShoot = false;
        }
    }
}
