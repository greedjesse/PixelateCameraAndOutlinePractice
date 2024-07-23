using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelatedCamera : MonoBehaviour
{
    public enum PixelScreen { Resize, Scale }
    
    [System.Serializable]
    public struct ScreenSize
    {
        public int width;
        public int height;
    }

    [Header("Screen Scaling Setting")]
    public PixelScreen mode;
    public ScreenSize targetScreenSize = new ScreenSize { width=256, height=144 };
    public uint screenScaleFactor = 1;

    private Camera _renderCamera;
    private RenderTexture _renderTexture;
    private int _screenWidth, _screenHeight;

    [Header("Display")] 
    public RawImage display;
    
    void Start()
    {
        Init();   
    }
    
    void Update()
    {
        Init();
        if (CheckScreenResize()) Init();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Init()
    {
        if (!_renderCamera) _renderCamera = GetComponent<Camera>();
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        
        if (screenScaleFactor < 1) screenScaleFactor = 1;
        if (targetScreenSize.width < 1) targetScreenSize.width = 1;
        if (targetScreenSize.height < 1) targetScreenSize.height = 1;

        int width = mode == PixelScreen.Resize ? (int)targetScreenSize.width : _screenWidth / (int)screenScaleFactor;
        int height = mode == PixelScreen.Resize ? (int)targetScreenSize.height : _screenHeight / (int)screenScaleFactor;

        _renderTexture = new RenderTexture(width, height, 24)
        {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };

        _renderCamera.targetTexture = _renderTexture;
        display.texture = _renderTexture;
    }

    private bool CheckScreenResize()
    {
        return Screen.width != _screenWidth || Screen.height != _screenHeight;
    }
}
