using UnityEngine;

public class GameFactory 
{
    private Transform _player;
    private Transform _uiRoot;
    private Inventory _inventory;
    private AttackZone _attackZone;
    private WeaponFactory _factory;
    private IInputService _inputService;
    private Vector3 _defaultPosition = new Vector3(0, 0, 0);

    public GameFactory(IInputService inputService)
    {
        _inputService = inputService;
    }

    public void CreatePlayer()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.Player);
        _player = Object.Instantiate(prefab, _defaultPosition, Quaternion.identity).transform;
        _player.GetComponentInChildren<PlayerMover>().Init(_inputService);
        _player.GetComponentInChildren<PlayerAttacker>().Init(_inputService, _factory);
        _inventory = _player.GetComponentInChildren<Inventory>();
        _attackZone = _player.GetComponentInChildren<AttackZone>();
    }

    public void CreateWeaponFactory()
    {
        _factory = new WeaponFactory(_attackZone, _player);
    }

    public void CreateCamera()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.MainCamera);
        GameObject camera = Object.Instantiate(prefab);
        camera.GetComponent<CameraFollower>().Init(_player);
    }

    public void CreateUI()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.GameCanvas);
        _uiRoot = Object.Instantiate(prefab).transform;
    }

    public void CreateResourceView()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.ResourceViewPanel);
        Transform container = _uiRoot.GetComponentInChildren<UIDummy>().transform;
        GameObject panel = Object.Instantiate(prefab, container);
        panel.GetComponent<ResourceViewer>().Init(_inventory);
    }
}
