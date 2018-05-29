using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Particlecircle : MonoBehaviour {
	private ParticleSystem my_particlesystem;  // 粒子系统  
	private ParticleSystem.Particle[] particles;  // 粒子数组  
	private ParticlePosition[] particle_data; // 极坐标数组  
	public GameObject mybutton;
	private Button t_button;
									 // Use this for initialization

	public int count = 10000;
	public float minR = 5.0f;  // 最小半径  
	public float maxR = 11.0f; // 最大半径 
	public bool clockwise = true;
	private float midR;
	public Gradient colorGradient;
	private bool a = true;

	private bool ischange = false;
	private int changecount = 0;

	void Start () {
		particles = new ParticleSystem.Particle[count];
		particle_data = new ParticlePosition[count];
		my_particlesystem = this.GetComponent<ParticleSystem>();

		my_particlesystem.startSpeed = 0;            // 粒子位置由程序控制  
		my_particlesystem.startSize = 0.06f;          // 设置粒子大小  
		my_particlesystem.loop = false;
		my_particlesystem.maxParticles = count;      // 设置最大粒子量  
		my_particlesystem.Emit(count);               // 发射粒子  
		my_particlesystem.GetParticles(particles);
		midR = (maxR +minR) / 2;

		// 初始化梯度颜色控制器  
		GradientAlphaKey[] alphaKeys = new GradientAlphaKey[6];
		alphaKeys[0].time = 0.0f; alphaKeys[0].alpha = 1.0f;
		alphaKeys[1].time = 0.25f; alphaKeys[1].alpha = 0.4f;
		alphaKeys[2].time = 0.4f; alphaKeys[2].alpha = 0.8f;
		alphaKeys[3].time = 0.65f; alphaKeys[3].alpha = 0.4f;
		alphaKeys[4].time = 0.9f; alphaKeys[4].alpha = 0.8f;
		alphaKeys[5].time = 1.0f; alphaKeys[5].alpha = 1.0f;
		GradientColorKey[] colorKeys = new GradientColorKey[3];
		colorKeys[0].time = 0.0f; colorKeys[0].color = Color.red;
		colorKeys[1].time = 0.5f; colorKeys[1].color = Color.blue;
		colorKeys[2].time = 1.0f; colorKeys[2].color = Color.red;
		colorGradient.SetKeys(colorKeys, alphaKeys);

		for (int i = 0; i < count; ++i)
		{   // 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近  
			float minRate = Random.Range(1.0f, midR / minR);
			float maxRate = Random.Range(midR / maxR, 1.0f);
			float radius = Random.Range(minR * minRate, maxR * maxRate);

			// 随机每个粒子的角度  
			float angle = Random.Range(0.0f, 360.0f);
			float theta = angle / 180 * Mathf.PI;

			// 随机每个粒子的游离起始时间  
			float time = Random.Range(0.0f, 360.0f);

			particle_data[i] = new ParticlePosition(radius, angle, time);

			particles[i].position = new Vector3(particle_data[i].radius * Mathf.Cos(theta), 0f, particle_data[i].radius * Mathf.Sin(theta));

		}
		my_particlesystem.SetParticles(particles, particles.Length);

		mybutton.GetComponent<Button>().onClick.AddListener(() => {
			if (!ischange)
			{
				for (int i = 0; i < count; ++i)
				{
					if (a)
						particle_data[i].nextdis = midR + 0.25f * (particle_data[i].radius- midR) - particle_data[i].radius;
					else
						particle_data[i].nextdis = midR + 4 * (particle_data[i].radius- midR) - particle_data[i].radius;
				}
				a = !a;
				changecount = 25;
				ischange = true;
			}
		});

	}

	// Update is called once per frame
	void Update() {

		for (int i = 0; i < count; i++)
		{
			if (clockwise)
				particle_data[i].angle -= (i % 10 + 2) * (0.6f / particle_data[i].radius);
			else
				particle_data[i].angle += (i % 10 + 2) * (0.6f / particle_data[i].radius);
			// 保证angle在0~360度  
			particle_data[i].angle = (360.0f + particle_data[i].angle) % 360.0f;
			float theta = particle_data[i].angle / 180 * Mathf.PI;
			float newR = particle_data[i].radius + Mathf.PingPong(particle_data[i].time / minR / maxR, 0.02f) - 0.01f;

			particles[i].position = new Vector3(newR * Mathf.Cos(theta), 0f, newR * Mathf.Sin(theta));

			particles[i].color = colorGradient.Evaluate(particle_data[i].angle / 360.0f);

			particle_data[i].time += Time.deltaTime;
			if (ischange)
			{
				particle_data[i].radius += particle_data[i].nextdis / 25;
			}
		}
		if (ischange) { 
			changecount--;
			if (changecount <= 0)
			{
				ischange = false;
			}
		}

		my_particlesystem.SetParticles(particles, particles.Length);
	}
}
public class ParticlePosition
{
	public float radius = 0f, angle = 0f, time = 0f;
	public float nextdis=0f;
	public ParticlePosition(float radius, float angle, float time)
	{
		this.radius = radius;   // 半径  
		this.angle = angle;     // 角度  
		this.time = time;       // 时间  
		this.nextdis = 0;
	}
}