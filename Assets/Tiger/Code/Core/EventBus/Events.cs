using System.Collections.Generic;

namespace Tiger {
    public interface IEvent {
    }



    public struct UIButtonPressed : IEvent {
        public UIButtonTypes buttonType;
    }


    
    public struct UISliderChanged : IEvent {
        public UISliders sliderType;
        public float value;
    }
    
    public struct DataChanged : IEvent {}

    public struct FadeRequest : IEvent {
        public bool shouldFade;
    }

    public struct UISetTransitionMsg : IEvent {
        public UITransitionMessageTypes type;
    }

    public struct NoteChanged : IEvent {
        public List<NoteController.NoteItem> notes;
    }

    public struct OnItemClicked : IEvent {
        public ClickableObject item;
        public bool shouldAdd;
    }
    
    public struct OnLivesCountChanged : IEvent {
        public float count;
    }
    
    public struct TisTheEnd : IEvent {
        public bool isVictory;
    }
    
    public enum UISliders {
        SfxVolume,
        MusicVolume,
    }
    
    public enum UIButtonTypes {
        Play,
        Settings,
        Back,
        Pause,
        Resume,
        Restart,
        Exit,
        HowToPlay,
        Next
    }

    public enum UITransitionMessageTypes {
        None,
        Correct,
        Wrong,
        Remember,
        Repeat
    }
    
    // And he uses struct bcs:
    // "Structs are allocated on a stack, not a heap so they put way less pressure on the garbage collector"
    // Pretty cool
    public struct TestEvent : IEvent {
    }

    public struct PlayerEvent : IEvent {
        public int health;
        public int mana;
    }
}