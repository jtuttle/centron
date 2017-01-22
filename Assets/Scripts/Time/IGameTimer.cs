/*
 * Author: Isaiah Mann
 * Description: Unity independent setup for a timer
 */

public interface IGameTimer
{
	#region Instance Accessors 

	bool TimeIsUp 
	{
		get;
	}

	bool IsRunning 
	{
		get;
	}

	float TimeRemaining 
	{
		get;
	}

	#endregion

	IGameTimer Setup(float timeStep, float maxTime);

	void Teardown();
	void Reset();
	void Begin(bool resetOnStart);
	void Resume();

	float Pause();
	float Stop(bool resetOnStop);

  void SubscribeToTimeChange(Data.DataActionf action);
  void UnsubscribeFromTimeChange(Data.DataActionf action);
  void SubscribeToTimeUp(Data.DataAction action);
  void UnsubscribeFromTimeUp(Data.DataAction action);
	void ClearEventsOnTimeUp();
	void ModifyTimeRemaining(float deltaTime, bool checkForEvents);
	void SetTimeRemaining(float newTime, bool checkForEvents);
	void ZeroOutTimeRemaining(bool shouldCallTimeUpEvent);

}
