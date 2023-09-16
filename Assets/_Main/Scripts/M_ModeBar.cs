using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ProphetModes
{
    Human,
    God
}

public class M_ModeBar : Singleton<M_ModeBar>
{
    [SerializeField] Button HumanBtn;
    [SerializeField] Button GodBtn;

    ProphetModes _currentMode;
    public ProphetModes CurrentMode
    {
        get => _currentMode;
        set
        {
            if(_currentMode == value) return;
            
            _currentMode = value;
            
            switch (_currentMode)
            {
                case ProphetModes.Human:
                    break;
                case ProphetModes.God:
                    break;
            }
            
            ModeChanged?.Invoke(_currentMode);
        }
    }

    public static UnityAction<ProphetModes> ModeChanged;
    
    

    private void Awake()
    {
        HumanBtn.onClick.AddListener(()=> CurrentMode = ProphetModes.Human);
        GodBtn.onClick.AddListener(()=> CurrentMode = ProphetModes.God);
    }
    
    
    
}
