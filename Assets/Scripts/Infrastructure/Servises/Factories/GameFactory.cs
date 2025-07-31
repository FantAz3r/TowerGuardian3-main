using System.Collections.Generic;
using UnityEngine;

public class GameFactory 
{
    private Transform _player;
    private Transform _uiRoot;
    private PlayerExperience _experience;
    private Inventory _inventory;
    private AttackZone _attackZone;
    private WeaponFactory _weaponFactory;
    private PlayerConfigContainer _cardHolder;
    private AllCardConfigs _cards;
    private IInputService _inputService;
    private ITimeService _timeService;  
    private List<CardButton> _buttons = new List<CardButton>();

    public GameFactory(IInputService inputService, ITimeService timeService)
    {
        _inputService = inputService;
        _timeService = timeService;
    }

    public void CreatePlayer()
    {
        Player prefab = Resources.Load<Player>(GameConstants.Player);
        _player = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity).transform;
        _player.GetComponentInChildren<PlayerMover>().Init(_inputService);
        _player.GetComponentInChildren<PlayerAttacker>().Init(_inputService);
        _inventory = _player.GetComponentInChildren<Inventory>();
        _attackZone = _player.GetComponentInChildren<AttackZone>();
        _experience = _player.GetComponentInChildren<PlayerExperience>();
        _cardHolder = _player.GetComponentInChildren<PlayerConfigContainer>();
    }

    public void CreateWeaponFactory()
    {
        _weaponFactory = new WeaponFactory(_attackZone, _player);
    }

    public void CreateCamera()
    {
        CameraFollower prefab = Resources.Load<CameraFollower>(GameConstants.MainCamera);
        CameraFollower camera = Object.Instantiate(prefab);
        camera.Init(_player);
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

    public void CreateCards()
    {
        AllCardConfigs prefab = Resources.Load<AllCardConfigs>(GameConstants.AllCards);
        AllCardConfigs cards = Object.Instantiate(prefab);
        cards.Init(_cardHolder);
        _cards = cards;
    }

    public void CreateCardsSelectionMenu()
    {
        CardSelectionMenu prefab = Resources.Load<CardSelectionMenu>(GameConstants.CardSelectionMenu);
        Transform container = _uiRoot.transform;
        CardSelectionMenu panel = Object.Instantiate(prefab, container);
        panel.Init(_timeService, _experience, new CardSelector(_cards, _cardHolder), _buttons);
    }

    public void CreateCardButtons()
    {
        int cardsCount = 3;
        CardButton prefab = Resources.Load<CardButton>(GameConstants.CardViewer);
        Transform container = _uiRoot.transform;

        for (int i = 0; i < cardsCount; i++)
        {
            CardButton button = Object.Instantiate(prefab, container);
            button.Init(_cards, new List<ICardFactory> { _weaponFactory });
            _buttons.Add(button);
        }
    }

    public void CreateLight(int levelNumber)
    {
        DayCycle prefab = Resources.Load<DayCycle>(GameConstants.DirectionLight);
        DayCycle cycle = Object.Instantiate(prefab);
        cycle.Init(levelNumber);
    }
}