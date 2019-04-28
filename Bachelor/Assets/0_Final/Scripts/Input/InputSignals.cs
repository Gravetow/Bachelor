using UnityEngine;

public class HoverSignal { }

public class SelectSignal
{
    public Vector3 position;
    public GameObject selectedGameObject;
}

public class DeselectSignal { }

public class SubmitSignal { }

public class SubmittedSignal
{
    public GameObject submittedGameObject;
}

public class BeginDragSignal
{
    public Vector3 position;
}

public class DragSignal
{
    public Vector3 position;
}

public class EndDragSignal
{
    public Vector3 position;
}