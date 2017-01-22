/*
 * Authors: Isaiah Mann
 * Description: An interface to handle the audio module
 */

public interface IAudioModule {

	void Play(AudioFile file);
	void Stop(AudioFile file);
	void ToggleFXMute();
	void ToggleMusicMute();

}
