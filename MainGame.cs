using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public event Action<Leaper> OnLeaperSpawn;

    public event Action<float> OnReminderChange;
    public event Action<float> OnShootChange;
    public event Action<float> OnEnergyChange;

    public const float EffectLifeTime = 10f;

    public static readonly Vector3 TileOffset = new Vector3(30, 0, 0);
    private readonly Vector3 _defoultGraity = new Vector3(0, -9.81f, 0);

    [SerializeField] private GameObject _leaperPrefab;
    [SerializeField] private TilesList _tilesList;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _spawnPoint;

    private Leaper _leaper;

    private const float GravityChangeRate = 1.5f;


    private float _changeReminder;
    private float _energy;
    private float _maxEnergy = 10;

    [SerializeField] private float _shootCost = 2f;

    private bool _canChangeGraity = true;
    private bool _isGameOver = false;

    private void Awake()
    {
        Physics.gravity = _defoultGraity;
        _energy = _maxEnergy;

        Vector3 position = _firstPoint.position;
        for (int i = 0; i < 2; i++)
        {
            Instantiate(_tilesList.Tiles[Random.Range(0, _tilesList.Tiles.Length)],
            position,
            Quaternion.identity);
            position += TileOffset;
        }
    }

    private void OnEnable()
    {
        GameObject leaper = Instantiate(_leaperPrefab, _spawnPoint.position, Quaternion.identity);
        _leaper = leaper.GetComponent<Leaper>();
        _leaper.transform.SetParent(_spawnPoint.transform.parent);
        OnLeaperSpawn?.Invoke(_leaper);


        _leaper.OnLeaperDeath += OnLeaperDeath;
    }

    private void OnLeaperDeath()
    {
        _leaper.OnLeaperDeath -= OnLeaperDeath;
        _isGameOver = true;
        
    }

    private void Start()
    {
        SetReminder();
    }

    private void Update()
    {
        ReloadGravityChange();
        RegenerateEnergy();
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown((int)MouseButtons.Left))
            ChangeGravity();
        
        if (Input.GetMouseButtonDown((int)MouseButtons.Right))
            ChangeGravityImmediantely();
        if (Input.GetKeyDown(KeyCode.A))
            Shoot();
    }

    public void ChangeGravity()
    {
        if (_canChangeGraity && !_isGameOver)
        {
            Physics.gravity *= -1;
            _canChangeGraity = false;
            SetReminder();
        }
    }

    public void ChangeGravityImmediantely()
    {
        if (_energy > .5f * _maxEnergy)
        {
            _energy = 0;
            OnEnergyChange?.Invoke(_energy / _maxEnergy);
            Physics.gravity *= -1;
        }
    }

    private void ReloadGravityChange()
    {
        if (!_canChangeGraity && !_isGameOver)
        {
            _changeReminder -= Time.deltaTime;
            if (_changeReminder <= 0)
            {
                _canChangeGraity = true;
                SetReminder();
            }
            else
            {
                OnReminderChange?.Invoke(1f -(_changeReminder / GravityChangeRate));
            }
        }
    }

    private void SetReminder()
    {
        _changeReminder = GravityChangeRate;
        OnReminderChange?.Invoke(1f - (_changeReminder / GravityChangeRate));
    }

    public void Shoot()
    {
        if (!_isGameOver)
        {
            if (_energy > _shootCost)
            {
                _energy -= _shootCost;
                _leaper.Shoot();
            }
        }
    }

    private void RegenerateEnergy()
    {
        if (_energy < _maxEnergy)
        {
            _energy += Time.deltaTime;
            OnEnergyChange?.Invoke(_energy / _maxEnergy);
        }
    }  
}
