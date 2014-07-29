using UnityEngine;
using System.Collections;

public class TestIncrementingObjects : TestMain {
	
	public override void Start () {
		Application.targetFrameRate = 60;
		this.guiText.text = "";
		EventManager.instance.AddListener(this as IEventListener, "BenchmarkEvent");
		
		// Test ilosci obiektow na ekranie
		
		addTest ("Benchmark 50", 50, true, 0);
		addTest ("Benchmark 100", 100, true, 0);
		addTest ("Benchmark 250", 250, true, 0);
		addTest ("Benchmark 500", 500, true, 0);
		addTest ("Benchmark 750", 750, true, 0);
		addTest ("Benchmark 1000", 1000, true, 0);
		addTest ("Benchmark 1250", 1250, true, 0);
		addTest ("Benchmark 1500", 1500, true, 0);
		addTest ("Benchmark 1750", 1750, true, 0);
		addTest ("Benchmark 2000", 2000, true, 0);
	}
}