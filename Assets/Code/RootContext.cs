/// In an MVCSContext, like the one we're using, there are three types of
/// available mappings:
/// 1. Dependency Injection - Bind your dependencies to injectionBinder.
/// 2. View/Mediator Binding - Bind MonoBehaviours on your GameObjects to Mediators that speak to the rest of the app
/// 3. Event Binding - Bind Events to any/all of the following:
/// 		- Event/Method Binding -	Firing the event will trigger the method(s).
/// 		- Event/Command Binding -	Firing the event will instantiate the Command(s) and run its Execute() method.
/// 		- Event/Sequence Binding -	Firing the event will instantiate a Command(s), run its Execute() method, and,
/// 									unless the sequence is interrupted, fire each subsequent Command until the
/// 									sequence is complete.

using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;

// Look at StrangeIoC\examples\Assets\scripts\myfirstproject\MyFirstContext.cs for example implementation
public class RootContext : MVCSContext
{
    public RootContext(MonoBehaviour view) : base(view)
    {
    }

    public RootContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
    }
}

