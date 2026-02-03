using System;
using UnityEngine;
using YG;

//public class OrientationSwitcher : MonoBehaviour
//{
//    private IWindowService _windowService;
//    private WindowData _currentWindowData;
//    private WindowData _horizontalData;
//    private WindowData _verticalData;
//    private bool _canChange = true;
//
//    public event Action<GameUI> OrientationChanged;
//
//   //private void Awake()
//   //{
//   //    _windowService = ServiceLocator.Get<IWindowService>();
//   //
//   //    if ( YG2.envir.isDesktop)
//   //    {
//   //        _currentWindowData = 
//   //        _canChange = false;
//   //        _windowService.Reopen(_currentWindowData);
//   //        return;
//   //    }
//   //
//   //    _horizontalData = Resources.Load<WindowData>(GameConstants.HorizontalWindowData);
//   //    _verticalData = Resources.Load<WindowData>(GameConstants.VerticalWindowData);
//   //
//   //}
//
//    private void Update()
//    {
//        if (_canChange == false)
//            return;
//
//        if (Screen.width > Screen.height)
//        {
//            _currentWindowData = _horizontalData;
//        }
//        else
//        {
//            _currentWindowData = _verticalData;
//        }
//
//        _windowService.Reopen(_currentWindowData);
//    }
//}
