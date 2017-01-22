/*
 * Authors: Isaiah Mann
 * Description: An interface to handle event logic
 */

public interface IEventModule {

	void InstanceSubscribe (EventModule.NamedEventAction action);
	void InstanceSubscribe (EventModule.NamedFloatAction action);
	void InstanceSubscribe (EventModule.AudioEventAction action);

	void InstanceUnsubscribe (EventModule.NamedEventAction action);
	void InstanceUnsubscribe (EventModule.NamedFloatAction action);
	void InstanceUnsubscribe (EventModule.AudioEventAction action);

	void InstanceEvent (string eventName);
	void InstanceEvent (string valueKey, float value);
	void InstanceEvent(AudioActionType actionType, AudioType audioType);

}
