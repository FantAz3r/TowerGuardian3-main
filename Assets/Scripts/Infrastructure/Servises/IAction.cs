public interface IAction
{
    InteractionType GetInteractionType();
    void Execute();
}