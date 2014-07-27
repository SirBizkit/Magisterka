using UnityEngine;
using System.Collections;

public class TestMain3D : TestMain {

	public override void Start () {
		Application.targetFrameRate = 60;
		this.guiText.text = "";
		EventManager.instance.AddListener(this as IEventListener, "BenchmarkEvent");
		
		//addTest ("Benchmark 21", 250, true, 50);
		
		addTest ("Benchmark 1", 10, false, 0);
		addTest ("Benchmark 2", 50, false, 0);
		addTest ("Benchmark 3", 250, false, 0);
		addTest ("Benchmark 4", 500, false, 0);
		
		addTest ("Benchmark 5", 10, true, 0);
		addTest ("Benchmark 6", 50, true, 0);
		addTest ("Benchmark 7", 250, true, 0);
		addTest ("Benchmark 8", 500, true, 0);
		
		addTest ("Benchmark 9", 100, true, 5);
		addTest ("Benchmark 10", 100, true, 10);
		addTest ("Benchmark 11", 100, true, 15);
		addTest ("Benchmark 12", 100, true, 20);
		addTest ("Benchmark 13", 100, true, 25);
		addTest ("Benchmark 14", 100, true, 35);
		addTest ("Benchmark 15", 100, true, 50);
	}
}
