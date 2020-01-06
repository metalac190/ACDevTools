using System;

[Serializable]
public class State {
	public string Label { get; private set; }
	Action _customEnter = delegate { };
	Action _customExit = delegate { };

	public State (Action enter, Action exit = null, string label = "") {
		_customEnter = enter;
		_customExit = exit;
		this.Label = label;
	}

	public void Enter () {
		if (_customEnter != null)
			_customEnter();
	}

	public void Exit () {
		if (_customExit != null)
			_customExit();
	}
}