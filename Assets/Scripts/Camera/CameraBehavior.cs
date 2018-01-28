using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	/**
	* Attributes
	*/
	public static CameraBehavior instance = null;

	Player player;

	[SerializeField] Transform MoveAxis;
	[SerializeField] Transform ShakeAxis;

	// For shaking camera
	private bool _isShaking = false;
	private int _shakeCount;
	private float _shakeIntensity, _shakeSpeed, _baseX, _baseY;
	private Vector3 _nextShakePosition;

	/**
	* Monobehavior methods
	*/
	void Awake() {
		if (!instance)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}

	private void Start() {
		player = Player.instance;

		// Set up base positions, these are used for shaking to determine where to return to after a shake.
		_baseX = ShakeAxis.localPosition.x;
		_baseY = ShakeAxis.localPosition.y;
	}
	
	void Update () {
		MoveAxis.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, MoveAxis.transform.position.z);

		if (_isShaking) {
			// Move toward the previously determined next shake position
			ShakeAxis.localPosition = Vector3.MoveTowards(ShakeAxis.localPosition, _nextShakePosition, Time.deltaTime * _shakeSpeed);

			// Determine if we are there or not
			if (Vector2.Distance(ShakeAxis.localPosition, _nextShakePosition) < _shakeIntensity / 5f) {
				//Decrement shake counter
				_shakeCount--;

				// If we are done shaking, turn this off if we're not longer moving
				if (_shakeCount <= 0) {
					_isShaking = false;
					ShakeAxis.localPosition = new Vector3(_baseX, _baseY, ShakeAxis.localPosition.z);
				}
				// If there is only 1 shake left, return back to base
				else if (_shakeCount <= 1) {
					_nextShakePosition = new Vector3(_baseX, _baseY, ShakeAxis.localPosition.z);
				}
				// If we are not done or nearing done, determine the next position to travel to
				else {
					DetermineNextShakePosition();
				}
			}
		}
	}

	public void Shake(float intensity, int shakes, float speed) {
		enabled = true;
		_isShaking = true;
		_shakeCount = shakes;
		_shakeIntensity = intensity;
		_shakeSpeed = speed;

		DetermineNextShakePosition();
	}


	private void DetermineNextShakePosition() {
		_nextShakePosition = new Vector3(Random.Range(-_shakeIntensity, _shakeIntensity),
				Random.Range(-_shakeIntensity, _shakeIntensity),
				ShakeAxis.localPosition.z);
	}
}
