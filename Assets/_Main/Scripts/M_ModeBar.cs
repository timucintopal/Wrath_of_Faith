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

    ProphetModes _currentMode = ProphetModes.Human;
    public ProphetModes CurrentMode
    {
        get => _currentMode;
        set
        {
            if(_currentMode == value) return;
            
            _currentMode = value;
            
            HumanBtn.interactable = _currentMode == ProphetModes.God;
            GodBtn.interactable = _currentMode == ProphetModes.Human;
            
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

    private void Start()
    {
        CurrentMode = ProphetModes.Human;
    }
}
