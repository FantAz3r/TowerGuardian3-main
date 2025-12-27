using System.Collections.Generic;

public class InteractionObjectFactory
{
    private List<IAction> _actions;

    public InteractionObjectFactory(List<IAction> actions )
    {
        _actions = actions;
    }

    //public void Create(Vector3 buildingPoint, InteractionType interactionType, InteractionMethod interactionObject)
    //{
    //    interactionObject = Object.Instantiate(interactionObject, buildingPoint, Quaternion.identity);
    //
    //    foreach (var action in _actions)
    //    {
    //        if(action.GetInteractionType() == interactionType)
    //        {
    //            interactionObject.Init(action);
    //        }
    //    }
    //}
}
