using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Projectile _currentProjectile;
    private Camera _mainCamera;
    private Transform _bulletSpawnPoint;
    private GameManager _gameManager;
    [SerializeField] private float projectileSpeed = 90f;
    [SerializeField] [Range(0f, 0.3f)]
    private float bulletSpread = 0.06f; // чем меньше тем меньше разброс
    public bool IsLoaded { get; private set; } = true;

    [SerializeField]
    private int maxClipAmmo;

    public int MaxClipAmmo
    {
        get => maxClipAmmo;
        private set => maxClipAmmo = value;
    }
    [SerializeField]
    private int currentClipAmmo;

    public int CurrentClipAmmo
    {
        get => currentClipAmmo;
        private set
        {
            currentClipAmmo = value;
            if (value < 1)
            {
                IsLoaded = false;
            }
        }
    }
    
    private void Start()
    {
        _bulletSpawnPoint = transform.GetChild(0).transform;
        _gameManager = GameManager.Instance;
        _mainCamera = _gameManager.mainCamera;
    }

    private void Update()
    {
        Vector2 pointer = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.LookAt2D(pointer);
        if (Input.GetButtonDown("Fire1") && IsLoaded)
        {
            CurrentClipAmmo--;
            _currentProjectile = _gameManager.GetProjectileFromPool(_bulletSpawnPoint.position);
            var cpTransform = _currentProjectile.transform;
            cpTransform.LookAt2D(pointer);
            var spread = cpTransform.rotation;
            spread.z += UnityEngine.Random.Range(-bulletSpread, bulletSpread);
            cpTransform.rotation = spread;
            _currentProjectile._rigidbody2D.AddForce(cpTransform.right.normalized * projectileSpeed, ForceMode2D.Impulse);
            //_currentProjectile._rigidbody2D.AddForce(pointer.normalized * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    public void Reload()
    {
        if (CurrentClipAmmo < maxClipAmmo)
        {
            CurrentClipAmmo = maxClipAmmo;
            IsLoaded = true;
        }
    }
}
