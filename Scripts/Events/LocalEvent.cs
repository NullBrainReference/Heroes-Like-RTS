using System;


public class LocalEvent: ILocalEvent
{
    private Action eventAction;

	public LocalEvent(Action eventAction)
	{
		this.eventAction = eventAction;
	}

	public void Invoke()
	{
		eventAction.Invoke();
	}
}
