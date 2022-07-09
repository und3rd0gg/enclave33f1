using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string language;
    public Dictionary<GameObject, Inventory> InventoryContainer;

    [SerializeField] private GameObject bullet;
    private readonly int _bulletCount = 50;
    public Camera mainCamera;
    public List<Projectile> projectilePool;
    private GameObject _bulletPool;

    #region Singleton

    public static GameManager Instance { get; private set; }

    #endregion
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        mainCamera = Camera.main;

        language = "Russian";

        InventoryContainer = new Dictionary<GameObject, Inventory>();
    }

    private void Start()
    {   //!инициализация буллет пула
        _bulletPool = new GameObject("BulletPool");
        CreateProjectilePool();
    }

    #region ProjectilePool
    private void CreateProjectilePool()
    {
        projectilePool = new List<Projectile>();
        for (int i = 0; i < _bulletCount; i++)
        {
            var bullet = Instantiate(this.bullet, _bulletPool.transform);
            projectilePool.Add(bullet.GetComponent<Projectile>());
            bullet.gameObject.SetActive(false);
        }
    }

    public Projectile GetProjectileFromPool(Vector2 spawnPoint)
    {
        if (projectilePool.Count > 0)
        {
            var projectile = projectilePool[0];
            projectilePool.Remove(projectile);
            projectile.gameObject.SetActive(true);
            projectile.transform.parent = null;
            projectile.transform.position = spawnPoint;
            return projectile;
        }
        return Instantiate(bullet, spawnPoint, Quaternion.identity).GetComponent<Projectile>();
    }

    public void ReturnProjectileToPool(Projectile projectile)
    {
        if (!projectilePool.Contains(projectile))
        {
            projectilePool.Add(projectile);
            projectile.transform.parent = _bulletPool.transform;
            projectile.transform.position = Vector3.zero;
            projectile.gameObject.SetActive(false);
        }
        else
        {
            Destroy(projectile.gameObject);
        }
        
    }
    #endregion
}