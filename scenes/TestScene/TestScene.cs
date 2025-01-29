using Godot;
using Godot.Collections;

public partial class TestScene : Node2D
{
	[Export] NoiseTexture2D noiseTexture;
	[Export] NoiseTexture2D noiseTexture2;
	
	private TileMapLayer groundMap;
	private TileMapLayer grassMap;
	private TileMapLayer SnowMap;
	private TileMapLayer TreeMap;
	
	public override void _Ready()
	{
		grassMap = GetNode<TileMapLayer>("GrassBase");
		groundMap = GetNode<TileMapLayer>("GroundBase");
		SnowMap = GetNode<TileMapLayer>("SnowBase");
		TreeMap = GetNode<TileMapLayer>("TreeBase");
		
		var noise = noiseTexture.GetNoise() as FastNoiseLite;
		var noise2 = noiseTexture2.GetNoise() as FastNoiseLite;
		
		RandomNumberGenerator rng = new RandomNumberGenerator();
		
		int seed = (int)rng.Randi();
		noise.SetSeed(seed);
		noise2.SetSeed(seed);
		
		
		var arr_ground = new Array<Vector2I>();
		var arr_grass = new Array<Vector2I>();
		var arr_snow = new Array<Vector2I>();
		var arr_tree = new Array<Vector2I>();
		
		for (int i = 0; i <= 80; i++)
		{		
			for (int j = 0; j <= 50; j++)
			{
				float val = noise.GetNoise2D(i, j);
				float val2 = noise2.GetNoise2D(i, j);
				
				if (val <= 0.6)
				{
					arr_ground.Add(new Vector2I(i, j));
				}
				if (val <= 0.4)
				{
					arr_grass.Add(new Vector2I(i, j));
				}

				if (val <= -0.5)
				{
					arr_snow.Add(new Vector2I(i, j));
				}

				if (val2 <= -0.2)
				{
					TreeMap.SetCell(new Vector2I(i, j), 3, new Vector2I(0, 0), 1);
				}
				
			}
		}
		
		groundMap.SetCellsTerrainConnect(arr_ground ,0, 0);
		grassMap.SetCellsTerrainConnect(arr_grass, 0, 1);
		SnowMap.SetCellsTerrainConnect(arr_snow, 0, 2);


	}

	public void GenerateMap()
	{
		this.GetTree().ReloadCurrentScene();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		
		
		
		/*if (Input.IsActionJustPressed("click"))
		{
			Vector2I selectedCell = mapLayer.LocalToMap(mapLayer.GetLocalMousePosition());
			GD.Print(selectedCell.ToString());
			
			mapLayer.SetCell(selectedCell, 0, new Vector2I(1, 1));
		}*/
		
	}

}
