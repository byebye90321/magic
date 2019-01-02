using UnityEngine;
using System.Collections;

public class DungeonCharacterAnimator : MonoBehaviour
{

	public float _initialRandomTime = 0;
	public float _randomSpeed = 0;  //來回速度
	public float xDistacne;  //來回距離
	private Vector2 initialPosition; //初始位置
	public Transform player;
	public MonsterState monsterState;
	bool isChasing;
	public GameObject warningObj;
	public enum MonsterState
	{
		idle,
		chase,
		back,
	}
	public float diatanceToPlayer;
	public float diatanceToInitial;

	void Start()
	{
		//_initialRandomTime = Random.Range(0f, 3f);
		//_randomSpeed = Random.Range(1f, 1.7f);
		initialPosition = transform.position;
		monsterState = MonsterState.idle;
	}

	void Update()
	{
		switch (monsterState)
		{
			case MonsterState.idle:
				float currentPositionTime = Time.time * _randomSpeed + _initialRandomTime;
				transform.position = new Vector3(initialPosition.x + Mathf.Cos(currentPositionTime) * xDistacne, transform.position.y, transform.position.z);
				if (currentPositionTime % (Mathf.PI * 2f) < Mathf.PI)
				{
					transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				}
				else
				{
					transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
				}
				WarningCheck();
				break;

			case MonsterState.chase:
				if (!isChasing)
				{
					isChasing = true;
				}
				StartCoroutine("warningOpen");
				ChaseRadiusCheck();
				break;

			case MonsterState.back:
				transform.position = Vector2.MoveTowards(transform.position, initialPosition, 2f * Time.deltaTime);
				ReturnCheck();
				break;
		}
	
	}

	void WarningCheck()
	{
		diatanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (diatanceToPlayer < 3)
		{
			monsterState = MonsterState.chase;
		}

		if (diatanceToPlayer > 3)
		{
			monsterState = MonsterState.idle;
		}
	}

	void ChaseRadiusCheck()
	{

		diatanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
		if (transform.position.x - player.transform.position.x>0 )
		{
			transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		else
		{
			transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
		}

		if (diatanceToInitial > 7 || diatanceToPlayer > 3)
		{
			monsterState = MonsterState.back;
		}
	}

	void ReturnCheck()
	{
		diatanceToInitial = Vector3.Distance(transform.position, initialPosition);

		if (transform.position.x  - initialPosition.x > 0)
		{
			transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			Debug.Log("0");
		}
		else
		{
			transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
			Debug.Log("1");
		}
		if (diatanceToInitial < 0.1f)
		{
			isChasing = false;
			monsterState = MonsterState.idle;
		}
	}

	IEnumerator warningOpen()
	{
		warningObj.SetActive(true);
		yield return new WaitForSeconds(.3f);
		warningObj.SetActive(false);
		transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);

	}
}
