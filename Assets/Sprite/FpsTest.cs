using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FpsTest : MonoBehaviour
{
	private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;

	private float m_UpdateShowDeltaTime = 0.01f;//更新帧率的时间间隔;

	private int m_FrameUpdate = 0;//帧数;

	public float m_FPS = 0;

	public Text fps;

	void Awake()
	{
		Application.targetFrameRate = 100;
	}

	// Use this for initialization
	void Start()
	{
		m_LastUpdateShowTime = Time.realtimeSinceStartup;
	}

	// Update is called once per frame
	void Update()
	{
		m_FrameUpdate++;
		if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
		{
			m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
			m_FrameUpdate = 0;
			m_LastUpdateShowTime = Time.realtimeSinceStartup;
		}
		//Debug.Log(m_FPS);
		fps.text = m_FPS.ToString("#0.00");
	}

}